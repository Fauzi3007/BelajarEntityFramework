using BelajarEntityFramework.Models;
using BelajarEntityFramework.ViewModel;
using BelajarEntityFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BelajarEntityFramework.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly EFDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<UserProfileController> _logger;

        // Allowed file extensions and MIME types for profile pictures (images)
        private readonly string[] _allowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly string[] _allowedImageMimeTypes = { "image/jpeg", "image/png", "image/gif" };
        private const long _profilePicSizeLimit = 2 * 1024 * 1024; // 2 MB

        // Allowed file extension and MIME type for identity proofs (PDF)
        private readonly string[] _allowedPdfExtensions = { ".pdf" };
        private readonly string[] _allowedPdfMimeTypes = { "application/pdf" };
        private const long _identityProofSizeLimit = 5 * 1024 * 1024; // 5 MB

        public UserProfileController(EFDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<UserProfileController> logger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        // GET: /UserProfile/Index
        // Lists all user profiles.
        public IActionResult Index()
        {
            // Eager-load IdentityProofs and their related master data.
            var profiles = _context.UserProfiles
                .Select(p => new UserProfile
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    ProfilePictureFileName = p.ProfilePictureFileName,
                    CreatedOn = p.CreatedOn,
                    IdentityProofs = p.IdentityProofs.Select(ip => new IdentityProof
                    {
                        Id = ip.Id,
                        FileName = ip.FileName,
                        FileType = ip.FileType,
                        FileSize = ip.FileSize,
                        UploadedOn = ip.UploadedOn,
                        VerificationStatus = ip.VerificationStatus,
                        ProofType = ip.ProofType
                    }).ToList()
                }).ToList();

            return View(profiles);
        }

        // GET: /UserProfile/Create
        // Renders the form to create a new user profile.
        public IActionResult Create()
        {
            // Load the master list of proof types from the database.
            var proofTypes = _context.ProofTypes.ToList();
            ViewBag.ProofTypes = new SelectList(proofTypes, "Id", "Name");

            // Initialize with one empty identity proof field.
            var model = new UserProfileUploadViewModel();
            model.IdentityProofs.Add(new IdentityProofUploadViewModel());
            return View(model);
        }

        // POST: /UserProfile/Create
        // Processes the submitted form.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserProfileUploadViewModel model)
        {
            // Re-populate the ProofTypes list for the dropdown in case of an error.
            ViewBag.ProofTypes = new SelectList(_context.ProofTypes.ToList(), "Id", "Name");

            // Check if the overall model state is valid.
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // --- Server-Side Duplicate Check for Identity Proof Types ---
            if (model.IdentityProofs != null && model.IdentityProofs.Any())
            {
                // Only consider entries that have a file uploaded.
                var selectedProofTypeIds = model.IdentityProofs
                    .Where(ip => ip.File != null && ip.File.Length > 0)
                    .Select(ip => ip.SelectedProofTypeId)
                    .ToList();

                // If the number of unique proof type IDs is less than the number provided, then duplicates exist.
                if (selectedProofTypeIds.Count != selectedProofTypeIds.Distinct().Count())
                {
                    ModelState.AddModelError("IdentityProofs", "Each identity proof must have a unique proof type.");
                    return View(model);
                }
            }

            // --- Validate Profile Picture ---
            if (model.ProfilePicture == null || model.ProfilePicture.Length == 0)
            {
                ModelState.AddModelError("ProfilePicture", "Profile picture is required.");
                return View(model);
            }
            if (model.ProfilePicture.Length > _profilePicSizeLimit)
            {
                ModelState.AddModelError("ProfilePicture", "Profile picture exceeds the maximum allowed size of 2 MB.");
                return View(model);
            }
            var profilePicExt = Path.GetExtension(model.ProfilePicture.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(profilePicExt) || !_allowedImageExtensions.Contains(profilePicExt))
            {
                ModelState.AddModelError("ProfilePicture", "Invalid file extension for profile picture.");
                return View(model);
            }
            if (!_allowedImageMimeTypes.Contains(model.ProfilePicture.ContentType))
            {
                ModelState.AddModelError("ProfilePicture", "Invalid MIME type for profile picture.");
                return View(model);
            }

            // --- Prepare Folders for Saving Files ---
            var uploadsRoot = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var profilePicFolder = Path.Combine(uploadsRoot, "profile_pictures");
            var identityProofFolder = Path.Combine(uploadsRoot, "identity_proofs");

            if (!Directory.Exists(profilePicFolder))
                Directory.CreateDirectory(profilePicFolder);
            if (!Directory.Exists(identityProofFolder))
                Directory.CreateDirectory(identityProofFolder);

            // --- Save the Profile Picture ---
            var profilePicFileName = Guid.NewGuid().ToString() + profilePicExt;
            var profilePicPath = Path.Combine(profilePicFolder, profilePicFileName);
            using (var stream = new FileStream(profilePicPath, FileMode.Create))
            {
                await model.ProfilePicture.CopyToAsync(stream);
            }

            // --- Create the UserProfile Entity ---
            var userProfile = new UserProfile
            {
                FullName = model.FullName,
                ProfilePictureFileName = profilePicFileName,
                ProfilePictureFileType = model.ProfilePicture.ContentType,
                ProfilePictureFileSize = model.ProfilePicture.Length,
                CreatedOn = DateTime.Now,
                IdentityProofs = new List<IdentityProof>()
            };

            // --- Process Each Identity Proof Entry ---
            if (model.IdentityProofs != null)
            {
                foreach (var proofModel in model.IdentityProofs)
                {
                    // Skip entries where no file is provided.
                    if (proofModel.File == null || proofModel.File.Length == 0)
                        continue;

                    // Validate file size for identity proof.
                    if (proofModel.File.Length > _identityProofSizeLimit)
                    {
                        ModelState.AddModelError("IdentityProofs", "One of the identity proof files exceeds the maximum allowed size of 5 MB.");
                        return View(model);
                    }
                    // Validate file extension.
                    var proofExt = Path.GetExtension(proofModel.File.FileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(proofExt) || !_allowedPdfExtensions.Contains(proofExt))
                    {
                        ModelState.AddModelError("IdentityProofs", "Invalid file extension for identity proof. Only PDF files are allowed.");
                        return View(model);
                    }
                    // Validate MIME type.
                    if (!_allowedPdfMimeTypes.Contains(proofModel.File.ContentType))
                    {
                        ModelState.AddModelError("IdentityProofs", "Invalid MIME type for identity proof. Only PDF files are allowed.");
                        return View(model);
                    }

                    // Save the identity proof file.
                    var proofFileName = Guid.NewGuid().ToString() + proofExt;
                    var proofFilePath = Path.Combine(identityProofFolder, proofFileName);
                    using (var stream = new FileStream(proofFilePath, FileMode.Create))
                    {
                        await proofModel.File.CopyToAsync(stream);
                    }

                    // Create a new IdentityProof entity.
                    var identityProof = new IdentityProof
                    {
                        // Map the selected proof type ID from the view model.
                        ProofTypeId = proofModel.SelectedProofTypeId,
                        FileName = proofFileName,
                        FileType = proofModel.File.ContentType,
                        FileSize = proofModel.File.Length,
                        // Set the initial verification status as Pending (assuming its ID is 1 in your VerificationStatuses master table).
                        VerificationStatusId = 1,
                        UploadedOn = DateTime.Now
                    };

                    // Add this identity proof to the user profile.
                    userProfile.IdentityProofs.Add(identityProof);
                }
            }

            // --- Save the UserProfile (and its IdentityProofs) to the Database ---
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();

            // Redirect to the Index action after successful creation.
            return RedirectToAction("Index");
        }


        // GET: /UserProfile/Verify/{id}
        // Renders a form to verify (approve/reject) a specific identity proof.
        public IActionResult Verify(int id)
        {
            var proof = _context.IdentityProofs
                .Include(ip => ip.ProofType)
                .Include(ip => ip.VerificationStatus)
                .FirstOrDefault(p => p.Id == id);
            if (proof == null)
                return NotFound();

            var model = new IdentityProofVerificationViewModel
            {
                Id = proof.Id,
                ProofTypeName = proof.ProofType.Name,
                FileName = proof.FileName,
                FileType = proof.FileType,
                FileSize = proof.FileSize,
                UploadedOn = proof.UploadedOn,
                VerificationStatusName = proof.VerificationStatus.Name,
                // Default new status as current (you may want to force a new selection)
                NewVerificationStatusId = proof.VerificationStatusId
            };

            // Load the master verification statuses to populate the drop-down.
            ViewBag.VerificationStatuses = new SelectList(_context.VerificationStatuses.ToList(), "Id", "Name");

            return View(model);
        }

        // POST: /UserProfile/Verify
        // Processes the verification update.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Verify(IdentityProofVerificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.VerificationStatuses = new SelectList(_context.VerificationStatuses.ToList(), "Id", "Name");
                return View(model);
            }

            var proof = _context.IdentityProofs.FirstOrDefault(p => p.Id == model.Id);
            if (proof == null)
                return NotFound();

            // Update verification status and admin comments.
            proof.VerificationStatusId = model.NewVerificationStatusId;
            proof.AdminComments = model.AdminComments;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
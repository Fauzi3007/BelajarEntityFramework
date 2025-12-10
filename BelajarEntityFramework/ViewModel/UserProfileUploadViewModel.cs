using System.ComponentModel.DataAnnotations;

namespace BelajarEntityFramework.ViewModel
{
    public class UserProfileUploadViewModel
    {
        [Required(ErrorMessage ="Full name is required")]
        [Display(Name ="Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Please select a profile picture.")]
        [Display(Name ="Profile Picture (Image file)")]
        public IFormFile ProfilePicture { get; set; }

        [Display(Name = "Identiti Proof (PDF Files)")]
        public List<IdentityProofUploadViewModel> IdentityProofs { get; set; } = new List<IdentityProofUploadViewModel>();
    }
}

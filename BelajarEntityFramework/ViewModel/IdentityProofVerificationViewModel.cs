using System.ComponentModel.DataAnnotations;

namespace BelajarEntityFramework.ViewModels
{
    public class IdentityProofVerificationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Proof Type")]
        public string? ProofTypeName { get; set; }

        [Display(Name = "File Name")]
        public string? FileName { get; set; }

        [Display(Name = "File Type")]
        public string? FileType { get; set; }

        [Display(Name = "File Size (bytes)")]
        public long? FileSize { get; set; }

        [Display(Name = "Uploaded On")]
        public DateTime? UploadedOn { get; set; }

        [Display(Name = "Current Verification Status")]
        public string? VerificationStatusName { get; set; }

        [Required(ErrorMessage = "Please select a new status.")]
        [Display(Name = "New Verification Status")]
        public int NewVerificationStatusId { get; set; } 

        [Display(Name = "Admin Comments")]
        public string? AdminComments { get; set; }
    }
}
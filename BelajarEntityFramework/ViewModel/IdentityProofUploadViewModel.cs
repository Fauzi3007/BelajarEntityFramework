using System.ComponentModel.DataAnnotations;

namespace BelajarEntityFramework.ViewModel
{
    public class IdentityProofUploadViewModel
    {
        [Required(ErrorMessage ="Please select a proof type")]
        [Display(Name ="Proof Type")]
        public int SelectedProofTypeId { get; set; }

        [Required(ErrorMessage ="Please select a file for the identity proof")]
        [Display(Name ="Proof FIle (PDF)")]
        public IFormFile File { get; set; }
    }
}

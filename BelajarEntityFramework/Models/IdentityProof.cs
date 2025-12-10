using BelajarEntityFramework.Models;

namespace BelajarEntityFramework.Models
{
    public class IdentityProof
    {
        public int Id { get; set; }
        public int ProofTypeId { get; set; }
        public ProofType ProofType { get; set; } 
        public string FileName { get; set; }
        public string FileType { get; set; } 
        public long FileSize { get; set; } 
        public int VerificationStatusId { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
        public DateTime UploadedOn { get; set; }
        public string? AdminComments { get; set; }
        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
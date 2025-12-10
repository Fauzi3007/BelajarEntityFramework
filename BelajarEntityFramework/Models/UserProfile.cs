namespace BelajarEntityFramework.Models
{
    public class UserProfile
    {
        public int Id { get; set; } 

        public string FullName { get; set; } 

        public string ProfilePictureFileName { get; set; }
        public string ProfilePictureFileType { get; set; } 
        public long ProfilePictureFileSize { get; set; } 

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<IdentityProof> IdentityProofs { get; set; }
    }
}
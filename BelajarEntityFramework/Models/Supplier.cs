namespace BelajarEntityFramework.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }        
        public string Name { get; set; }           
        public string ContactEmail { get; set; }    
        public string PhoneNumber { get; set; }     

        public ICollection<Product> Products { get; set; }
    }
}
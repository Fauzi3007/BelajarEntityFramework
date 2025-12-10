namespace BelajarEntityFramework.Models
{
    public class Category
    {
        public int CategoryId { get; set; }        
        public string Name { get; set; }           
        public string Description { get; set; }      

        public ICollection<ProductExcel> Products { get; set; }
    }
}
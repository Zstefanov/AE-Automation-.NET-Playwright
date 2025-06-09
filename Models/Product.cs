namespace AE_extensive_project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Brand { get; set; }    }

    public class ProductResponse
    {
        public List<Product> Products { get; set; }
    }
}

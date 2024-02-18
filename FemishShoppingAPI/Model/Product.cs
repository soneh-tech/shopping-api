namespace FemishShoppingAPI.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get;set; }
        public decimal CurrentPrice { get; set; }
        public decimal PreviousPrice { get; set; }
        public int SellerID { get;set; }
        public Seller? Seller { get; set; }
        public List<ProductImage>? Images { get; set; }
        public List<OrderItem>? OrderItems { get; set; }

        [NotMapped]
        public List<IFormFile>? Photos { get; set; }
        }
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PreviousPrice { get; set; }
        public int SellerID { get; set; }
        public List<IFormFile>? Photos { get; set; }
    }
}

namespace FemishShoppingAPI.Model
{
    public class ProductImage
    {
        public int ProductImageID { get; set; }
        public string ImageURL { get; set; }
        public int ProductID { get; set; }
        public Product? Product { get; set; }
    }
    public class ProductImageDto
    {
        public int ProductImageID { get; set; }
        public string ImageURL { get; set; }
        public int ProductID { get; set; }
    }
}

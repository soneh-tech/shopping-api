namespace FemishShoppingAPI.Repository
{
    public interface IProductImage
    {
        Task<IEnumerable<ProductImage>> GetProductImages(int product_id);
        Task<IEnumerable<ProductImage>> ModifyProductImage(List<ProductImage> images);
        Task<bool> DeleteProductImage(int id);
    }
}

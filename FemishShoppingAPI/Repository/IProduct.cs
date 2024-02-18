namespace FemishShoppingAPI.Repository
{
    public interface IProduct
    {
        Task<IEnumerable<object>> GetSellerProducts(int seller_id);
        Task<object> GetProduct(int id);
        Task<bool> ModifyProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}

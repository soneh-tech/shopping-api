namespace FemishShoppingAPI.Repository
{
    public interface ICart
    {
        Task<IEnumerable<object>> GetCustomerCart(int customer_id);
        Task<bool> ModifyBuyerCart(Cart cart);
        Task<bool> DeleteBuyerCart(int customer_id);
        Task<bool> DeleteCartItem(int id);
    }
}

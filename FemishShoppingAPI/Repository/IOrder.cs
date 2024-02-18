namespace FemishShoppingAPI.Repository
{
    public interface IOrder
    {
        Task<IEnumerable<object>> GetCustomerOrders(int customer_id);
        Task<IEnumerable<object>> GetFilteredOrder(int customer_id, DateTime start,DateTime end);
        Task<object> ModifyOrder(Order order);
        Task<bool> DeleteOrder(int id);
        Task<IEnumerable<object>> GetSellerOrders(int seller_id);
        Task<IEnumerable<object>> GetSellerPendingOrders(int seller_id);
        Task<IEnumerable<object>> GetSellerOrders(string status);
        Task<object> GetOrder(int id);
    }
}

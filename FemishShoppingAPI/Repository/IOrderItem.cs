namespace FemishShoppingAPI.Repository
{
    public interface IOrderItem
    {
        Task<IEnumerable<object>> GetOrderItems(int order_id);
        Task<object> GetOrderItem(int id);
        Task<bool> ModifyOrderItem(OrderItem orderItem);
        Task<bool> DeleteOrderItem(int id);
    }
}

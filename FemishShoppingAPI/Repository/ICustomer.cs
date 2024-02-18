namespace FemishShoppingAPI.Repository
{
    public interface ICustomer
    {
        Task<object> GetCustomer(int customer_id);
        Task<Customer> GetCustomer(string id);
        Task<bool> ModifyCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
    }
}

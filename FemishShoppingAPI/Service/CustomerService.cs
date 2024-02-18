namespace FemishShoppingAPI.Service
{
    public class CustomerService : ICustomer
    {
        private readonly AppDBContext context;
        public CustomerService(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteCustomer(int id)
        {
            bool status = false;
            var customerToDelete = await context.Customers.FindAsync(id);
            if (customerToDelete != null)
            {
                context.Customers.Remove(customerToDelete);
                await context.SaveChangesAsync();
                status = true;
            }
            return status;
        }
        public async Task<object> GetCustomer(int customer_id)
        {
            var customer = await context.Customers.Where(x => x.CustomerID == customer_id)
             .Select(x => new
             {
                 CustomerID = x.CustomerID,
                 CustomerName = $"{x.FirstName} {x.LastName}",
                 Address=x.Address,
             })
             .FirstOrDefaultAsync();

            return customer;
        }
        public async Task<Customer> GetCustomer(string id)
        {
            return await context.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<bool> ModifyCustomer(Customer customer)
        {
            bool status = true;
            if (customer.CustomerID is not 0)
                context.Customers.Entry(customer).State = EntityState.Modified;
            else
                await context.Customers.AddAsync(customer);

            await context.SaveChangesAsync();

            return status;
        }
    }
}

namespace FemishShoppingAPI.Service
{
    public class OrderService : IOrder
    {
        private readonly AppDBContext context;
        public OrderService(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteOrder(int id)
        {
            bool status = false;
            var orderToDelete = await context.Orders.FindAsync(id);
            if (orderToDelete != null)
            {
                context.Orders.Remove(orderToDelete);
                await context.SaveChangesAsync();
                status = true;
            }
            return status;
        }
        public async Task<IEnumerable<object>> GetCustomerOrders(int customer_id)
        {
            var orders = await context.Orders
                  .Where(buyer => buyer.CustomerID == customer_id)
                  .Include(c => c.Customer)
                  .Select(c => new
                  {
                      OrderID = c.OrderID,
                      OrderNumber = c.OrderNumber,
                      OrderDate = c.OrderDate,
                      OrderItems = c.OrderItems.Count,
                      TotalAmount= c.TotalAmount,
                      Status = c.Status
                  })
                .ToListAsync();
            return orders;
        }
        public async Task<IEnumerable<object>> GetFilteredOrder(int customer_id, DateTime start, DateTime end)
        {
            var orders = await context.Orders
                .Where(x => x.CustomerID == customer_id && x.OrderDate.Date >= start.Date && x.OrderDate.Date <= end.Date)
                .Include(c => c.Customer)
                .Select(c => new
                {
                    OrderID = c.OrderID,
                    OrderNumber = c.OrderNumber,
                    OrderDate = c.OrderDate,
                    OrderItems = c.OrderItems.Count,
                    TotalAmount = c.TotalAmount,
                    Status = c.Status
                })
                .ToListAsync();
            return orders;
        }
        public async Task<object> GetOrder(int id)
        {
            var orders = await context.Orders
                   .Where(order => order.OrderID == id)
                   .Include(c => c.Customer)
                   .Select(c => new
                   {
                       OrderID = c.OrderID,
                       OrderNumber = c.OrderNumber,
                       OrderDate = c.OrderDate,
                       OrderItems = c.OrderItems.Count,
                       TotalAmount = c.TotalAmount,
                       Status = c.Status
                   })
                 .FirstOrDefaultAsync();
            return orders;
        }
        public async Task<IEnumerable<object>> GetSellerOrders(int seller_id)
        {
            var orders = await context.Orders
                  .Where(seller => seller.SellerID == seller_id)
                  .Include(c => c.Customer)
                  .Select(c => new
                  {
                      OrderID = c.OrderID,
                      CustomerID = c.CustomerID,
                      SellerID = c.SellerID,
                      OrderNumber = c.OrderNumber,
                      OrderDate = c.OrderDate,
                      OrderItems = c.OrderItems.Count,
                      TotalAmount = c.TotalAmount,
                      Status = c.Status,
                      CustomerName = $"{c.Customer.FirstName} {c.Customer.LastName}"
                  })
                .ToListAsync();
            return orders;

        }
        public async Task<IEnumerable<object>> GetSellerPendingOrders(int seller_id)
        {
            var orders = await context.Orders
                  .Where(seller => seller.SellerID == seller_id && seller.Status == "pending")
                  .Include(c => c.Customer)
                  .Select(c => new
                  {
                      OrderID = c.OrderID,
                      CustomerID = c.CustomerID,
                      SellerID = c.SellerID,
                      OrderNumber = c.OrderNumber,
                      OrderDate = c.OrderDate,
                      OrderItems = c.OrderItems.Count,
                      TotalAmount = c.TotalAmount,
                      Status = c.Status,
                      CustomerName = $"{c.Customer.FirstName} {c.Customer.LastName}"
                  })
                .ToListAsync();
            return orders;

        }

        public async Task<IEnumerable<object>> GetSellerOrders(string status)
        {
            var orders = await context.Orders
                  .Where(order => order.Status == status)
                  .Include(c => c.Customer)
                  .Select(c => new
                  {
                      OrderID = c.OrderID,
                      OrderNumber = c.OrderNumber,
                      OrderDate = c.OrderDate,
                      OrderItems = c.OrderItems.Count,
                      TotalAmount = c.TotalAmount,
                      Status = c.Status
                  })
                .ToListAsync();
            return orders;

        }
        public async Task<object> ModifyOrder(Order order)
        {
            if (order.OrderID is not 0)
                context.Orders.Entry(order).State = EntityState.Modified;
            else
                await context.Orders.AddAsync(order);

            await context.SaveChangesAsync();

            var last_order = await context.Orders.
                OrderByDescending(x => x.OrderID)
                .Select(x => new
                {
                    OrderID = x.OrderID
                })
                .FirstOrDefaultAsync();

            return last_order;
        }
    }
}

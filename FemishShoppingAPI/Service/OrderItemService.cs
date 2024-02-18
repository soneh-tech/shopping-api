namespace FemishShoppingAPI.Service
{
    public class OrderItemService : IOrderItem
    {
        private readonly AppDBContext context;
        public OrderItemService(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteOrderItem(int id)
        {
            bool status = false;
            var orderItemToDelete = await context.OrderItems.FindAsync(id);
            if (orderItemToDelete != null)
            {
                context.OrderItems.Remove(orderItemToDelete);
                await context.SaveChangesAsync();
                status= true;
            }
            return status;
        }
        public async Task<object> GetOrderItem(int id)
        {
            var orderItem = await context.OrderItems
                  .Where(o => o.OrderItemID == id)
                  .Include(o => o.Order)
                  .Select(p => new
                  {
                      OrderItemID = p.OrderItemID,
                      ProductName = p.Product.ProductName,
                      Price = p.Product.CurrentPrice,
                      Quantity = p.Quantity,
                      Total = p.ItemPrice,
                      Thumbnail = p.Product.Images.Select(i => new
                      { ImageURL = i.ImageURL }).FirstOrDefault(),
                  })
                  .FirstOrDefaultAsync();
            return orderItem;
        }
        public async Task<IEnumerable<object>> GetOrderItems(int order_id)
        {
            var orderItems = await context.OrderItems
                  .Where(p =>p.OrderID == order_id)
                  .Include(o => o.Order)
                  .Select(p => new
                  {
                      OrderItemID = p.OrderItemID,
                      ProductName = p.Product.ProductName,
                      Price = p.Product.CurrentPrice,
                      Quantity=p.Quantity,
                      Total =p.ItemPrice,
                      Thumbnail = p.Product.Images.Select(i => new
                      { ImageURL = i.ImageURL }).FirstOrDefault(),
                  })
                  .ToListAsync();
            return orderItems;
        }
        public async Task<bool> ModifyOrderItem(OrderItem orderItem)
        {
            bool status = true;
            if (orderItem.OrderItemID is not 0)
                context.OrderItems.Entry(orderItem).State = EntityState.Modified;
            else
                await context.OrderItems.AddAsync(orderItem);

            await context.SaveChangesAsync();

            return status;
        }
    }
}

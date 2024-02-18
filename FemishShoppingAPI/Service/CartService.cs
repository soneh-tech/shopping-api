namespace FemishShoppingAPI.Service
{
    public class CartService : ICart
    {
        private readonly AppDBContext context;
        public CartService(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteBuyerCart(int customer_id)
        {
            bool status = false;
            var cartToDelete = await context.Carts.Where(x => x.CustomerID == customer_id)
                .ToListAsync();
            if (cartToDelete != null)
            {
                context.Carts.RemoveRange(cartToDelete);
                await context.SaveChangesAsync();
                status = true;
            }
            return status;
        }
        public async Task<bool> DeleteCartItem(int id)
        {
            bool status = false;
            var cartToDelete = await context.Carts.FindAsync(id);
            if (cartToDelete != null)
            {
                context.Carts.Remove(cartToDelete);
                await context.SaveChangesAsync();
                status = true;
            }
            return status;
        }
        public async Task<IEnumerable<object>> GetCustomerCart(int customer_id)
        {
            var cartItems = await context.Carts
                 .Where(customer => customer.CustomerID == customer_id)
                 .Include(p => p.Product)
                 .Select(p => new
                 {
                     ProductID = p.Product.ProductID,
                     ProductName = p.Product.ProductName,
                     CartID = p.CartID,
                     Price = p.Product.CurrentPrice,
                     Quantity = p.Quantity,
                     Thumbnail = p.Product.Images.Select(i => new
                     { ImageURL = i.ImageURL }).FirstOrDefault(),
                 })
                 .ToListAsync();

            return cartItems;
        }
        public async Task<bool> ModifyBuyerCart(Cart cart)
        {
            bool status = true;
            if (cart.CartID is not 0)
                context.Carts.Entry(cart).State = EntityState.Modified;
            else
                await context.Carts.AddAsync(cart);

            await context.SaveChangesAsync();

            return status;
        }
    }
}

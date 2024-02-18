namespace FemishShoppingAPI.Service
{
    public class SellerService : ISeller
    {
        private readonly AppDBContext context;
        public SellerService(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteSeller(int id)
        {
            bool status = false;
            var sellerToDelete = await context.Sellers.FindAsync(id);
            if (sellerToDelete != null)
            {
                context.Sellers.Remove(sellerToDelete);
                await context.SaveChangesAsync();
                status = true;
            }
            return status;
        }
        public async Task<object> GetSeller(int seller_id)
        {
            var seller = await context.Sellers.Where(x => x.SellerID == seller_id)
                        .Select(x => new
                        {
                            SellerID = x.SellerID,
                            SellerName = $"{x.FirstName} {x.LastName}",
                            ShopName = x.ShopName,
                            ShopAddress = x.ShopAddress,
                            Location = x.Location,
                            ClientID = x.ClientID,
                        })
                        .FirstOrDefaultAsync();

            return seller;
        }
        public async Task<Seller> GetSeller(string id)
        {
            return await context.Sellers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<object>> GetSellerCustomers(int seller_id)
        {
            var customers = await context.Customers.Where(x => x.SellerID == seller_id)
                         .Select(x => new
                         {
                             CustomerID = x.CustomerID,
                             CustomerName = $"{x.FirstName} {x.LastName}",
                             Address = x.Address,
                             PhoneNumber = x.User.PhoneNumber,
                             Orders = x.Orders.Count(),
                         })
                         .ToListAsync();

            return customers;
        }

        public async Task<int> GetSellerIDByClientID(string? sellerID)
        {
            var result = await context.Sellers.Where(x => x.ClientID == sellerID).FirstOrDefaultAsync();
            if (result is not null)
                return result.SellerID;
            return 0;
        }

        public async Task<bool> ModifySeller(Seller seller)
        {
            bool status = true;
            if (seller.SellerID is not 0)
                context.Sellers.Entry(seller).State = EntityState.Modified;
            else
                await context.Sellers.AddAsync(seller);

            await context.SaveChangesAsync();

            return status;
        }
    }
}

namespace FemishShoppingAPI.Repository
{
    public interface ISeller
    {
        Task<object> GetSeller(int seller_id);
        Task<Seller> GetSeller(string  id);
        Task<bool> ModifySeller(Seller seller);
        Task<bool> DeleteSeller(int id);
        Task<IEnumerable<object>> GetSellerCustomers(int seller_id);
        Task<int> GetSellerIDByClientID(string? sellerID);
    }
}

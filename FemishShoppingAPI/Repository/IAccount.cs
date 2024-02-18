namespace FemishShoppingAPI.Repository
{
    public interface IAccount
    {
        public Task<StatusResponse> AuthenticateUser(UserDto user);
        public Task<StatusResponse> RegisterUser(UserDto user);
        public Task<StatusResponse> RegisterSeller(UserDto user);
    }
}

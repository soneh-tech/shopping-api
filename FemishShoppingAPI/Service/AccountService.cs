namespace FemishShoppingAPI.Service
{
    public class AccountService : IAccount
    {
        private readonly ICustomer _customer;
        private readonly ISeller _seller;
        // private readonly IAdmin _admin;
        private readonly IDevice _device;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountService(ICustomer customer,
                                ISeller seller,
                                IDevice device,
                                // IAdmin admin,
                                UserManager<IdentityUser> userManager,
                                IConfiguration configuration
                              )
        {
            _userManager = userManager;
            _configuration = configuration;
            _customer = customer;
            _seller = seller;
            _device = device;
            // _admin = admin
        }
        public async Task<StatusResponse> AuthenticateUser(UserDto user)
        {
            var response = new StatusResponse();
            var login_user = await _userManager.FindByEmailAsync(user.Email);
            if (login_user is not null)
            {
                if (await _userManager.CheckPasswordAsync(login_user, user.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(login_user);
                    var authClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,login_user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        if (userRole is "Buyer")
                        {
                            var customer = await _customer.GetCustomer(login_user.Id);
                            response.Status += $"{userRole}:{customer.CustomerID}:{customer.SellerID}";
                        }
                        else if (userRole is "Seller")
                        {
                            var seller = await _seller.GetSeller(login_user.Id);
                            response.Status += $"{userRole}:{0}:{seller.SellerID}";
                        }
                        else
                        {
                            //var admin = await _admin.GetAdmin(login_user.Id);
                            //response.Status += $"{userRole}:{0}:{admin.AdminID}";
                        }
                    }
                    var authSignInKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var token = new JwtSecurityToken
                        (
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.UtcNow.AddMonths(6),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
                        );

                    response.Message = new JwtSecurityTokenHandler().WriteToken(token);


                }
                else
                {
                    response.Status = Convert.ToString(StatusCodes.Status401Unauthorized);
                    response.Message = "invalid login attempt";
                }
            }
            return response;

        }
        public async Task<StatusResponse> RegisterUser(UserDto user)
        {
            var response = new StatusResponse();
            var new_user = new IdentityUser
            {
                Email = user.Email,
                UserName = user.Email
            };
            int seller_id = await _seller.GetSellerIDByClientID(user.SellerID);
            if (seller_id == 0)
            {
                response.Status = Convert.ToString(StatusCodes.Status404NotFound);
                response.Message = "The Seller with this ID was not found";

                return response;
            }
            var result = await _userManager.CreateAsync(new_user, user.Password);
            if (result.Succeeded)
            {
                var created_user = await _userManager.FindByEmailAsync(user.Email);
                var registered_device = await _device.RegisterDevice(user.DeviceToken);
                var customer_info = new Customer { FirstName = user.FirstName, LastName = user.LastName, SellerID = seller_id, Id = created_user.Id, DeviceID = registered_device.DeviceID };
                await _customer.ModifyCustomer(customer_info);
                await _userManager.AddToRoleAsync(new_user, "Buyer");
                response.Status = Convert.ToString(StatusCodes.Status200OK);
                response.Message = "User Account Was Created Successfully";

                return response;
            }
            else
            {
                foreach (var error in result.Errors)
                {

                    response.Status = error.Code;
                    response.Message = error.Description;

                }
                return response;
            }

        }
        public async Task<StatusResponse> RegisterSeller(UserDto user)
        {
            var response = new StatusResponse();
            var new_user = new IdentityUser
            {
                Email = user.Email,
                UserName = user.Email
            };
            var result = await _userManager.CreateAsync(new_user, user.Password);
            if (result.Succeeded)
            {
                var created_user = await _userManager.FindByEmailAsync(user.Email);
                var seller_info = new Seller { Id = created_user.Id };
                await _seller.ModifySeller(seller_info);
                await _userManager.AddToRoleAsync(new_user, "Seller");
                response.Status = Convert.ToString(StatusCodes.Status200OK);
                response.Message = "User Account Was Created Successfully";

                return response;
            }
            else
            {
                foreach (var error in result.Errors)
                {

                    response.Status = error.Code;
                    response.Message = error.Description;

                }
                return response;
            }

        }
    }
}

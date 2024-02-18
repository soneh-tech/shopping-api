namespace FemishShoppingAPI.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount account;
        public AccountController(IAccount account)
        {
            this.account = account;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> RegisterBuyer(UserDto user)
        {
            var result = await account.RegisterUser(user);
            return result is not null ? Ok(result) : BadRequest();
        }
        [Authorize]
        [HttpPost]
        [Route("api/onboardSeller")]
        public async Task<IActionResult> RegisterSeller(UserDto user)
        {
            var result = await account.RegisterSeller(user);
            return result is not null ? Ok(result) : BadRequest();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login(UserDto user)
        {
            var result = await account.AuthenticateUser(user);
            return result is not null ? Ok(result) : BadRequest();
        }
    }
}

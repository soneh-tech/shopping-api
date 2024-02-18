namespace FemishShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart cart;
        public CartController(ICart cart)
        {
            this.cart = cart;
        }

        [HttpGet]
        [Route("api/getBuyerCart/{buyer_id}")]
        public async Task<IActionResult> GetCarts(int buyer_id)
        {
            var result = await cart.GetCustomerCart(buyer_id);
            return result is not null ? Ok(result) : NoContent();
        }
        [HttpDelete]
        [Route("api/deleteCartItem/{cart_id}")]
        public async Task<IActionResult> DeleteCartItem(int cart_id)
        {
            var result = await cart.DeleteCartItem(cart_id);
            return result is not false ? Ok($"Success {result}") : BadRequest();
        }
        [HttpDelete]
        [Route("api/deleteBuyerCart/{buyer_id}")]
        public async Task<IActionResult> DeleteBuyerCart(int buyer_id)
        {
            var result = await cart.DeleteBuyerCart(buyer_id);
            return result is not false ? Ok($"Success {result}") : BadRequest();
        }
        [HttpPost]
        [Route("api/modifyCart")]
        public async Task<IActionResult> ModifyCart(CartDto cart_dto)
        {
            var my_cart = new Cart
            {
                CartID = cart_dto.CartID,
                CustomerID = cart_dto.CustomerID,
                ProductID = cart_dto.ProductID,
                Quantity = cart_dto.Quantity,
            };
            var result = await cart.ModifyBuyerCart(my_cart);
            return result is not false ? Ok($"Success {result}") : BadRequest();
        }
    }
}

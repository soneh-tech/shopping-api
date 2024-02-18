namespace FemishShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItem orderItem;
        public OrderItemController(IOrderItem orderItem)
        {
            this.orderItem = orderItem;
        }

        [HttpGet]
        [Route("api/getOrderItems/{order_id}")]
        public async Task<IActionResult> OrderItems(int order_id)
        {
            var result = await orderItem.GetOrderItems(order_id);
            return result is not null ? Ok(result) : NoContent();
        }

        [HttpGet]
        [Route("api/getOrderItem/{id}")]
        public async Task<IActionResult> OrderItem(int id)
        {
            var result = await orderItem.GetOrderItem(id);
            return result is not null ? Ok(result) : NoContent();
        }

        [HttpDelete]
        [Route("api/deleteOrderItem/{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await orderItem.DeleteOrderItem(id);
            return result is not false ? Ok($"Success {result}") : BadRequest();
        }

        [HttpPost]
        [Route("api/modifyOrderItem")]
        public async Task<IActionResult> ModifyOrderItem(OrderItemDto order_dto)
        {
            var my_order = new OrderItem
            {
                OrderID = order_dto.OrderID,
                OrderItemID = order_dto.OrderItemID,
                ItemPrice = order_dto.ItemPrice,
                ProductID = order_dto.ProductID,
                Quantity = order_dto.Quantity,
            };
            var result = await orderItem.ModifyOrderItem(my_order);
            return result is not false ? Ok($"Success {result}") : BadRequest();
        }
    }
}

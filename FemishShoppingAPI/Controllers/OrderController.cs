namespace FemishShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder order;
        public OrderController(IOrder order)
        {
            this.order = order;
        }

        [HttpGet]
        [Route("api/getBuyerOrders/{buyer_id}")]
        public async Task<IActionResult> GetOrders(int buyer_id)
        {
            var result = await order.GetCustomerOrders(buyer_id);
            return result is not null ? Ok(result) : NoContent();
        }
        [HttpGet]
        [Route("api/getSellerOrders/{seller_id}")]
        public async Task<IActionResult> GetSellerOrders(int seller_id)
        {
            var result = await order.GetSellerOrders(seller_id);
            return result is not null ? Ok(result) : NoContent();
        }
        [HttpGet]
        [Route("api/getSellerPendingOrders/{seller_id}")]
        public async Task<IActionResult> GetSellerPendingOrders(int seller_id)
        {
            var result = await order.GetSellerPendingOrders(seller_id);
            return result is not null ? Ok(result) : NoContent();
        }
        [HttpGet]
        [Route("api/getOrdersByStatus/{status}")]
        public async Task<IActionResult> GetSellerOrders(string status)
        {
            var result = await order.GetSellerOrders(status);
            return result is not null ? Ok(result) : NoContent();
        }

        [HttpPost]
        [Route("api/getBuyerFilteredOrders")]
        public async Task<IActionResult> GetOrder(OrderFilterDto filter)
        {
            var result = await order.GetFilteredOrder(filter.UserID, filter.Start, filter.End);
            return result is not null ? Ok(result) : NoContent();
        }

        [HttpDelete]
        [Route("api/deleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await order.DeleteOrder(id);
            return result is not false ? Ok($"Success {result}") : NoContent();
        }

        [HttpPost]
        [Route("api/modifyOrder")]
        public async Task<IActionResult> ModifyOrder(OrderDto order_dto)
        {
            var my_order = new Order
            {
                OrderID = order_dto.OrderID,
                SellerID = order_dto.SellerID,
                CustomerID = order_dto.CustomerID,
                OrderDate = order_dto.OrderDate,
                OrderNumber = order_dto.OrderNumber,
                TotalAmount = order_dto.TotalAmount,
                Status = order_dto.Status,
            };
            var result = await order.ModifyOrder(my_order);
            return result is not null ? Ok(result) : NoContent();
        }
        [HttpPost]
        [Route("api/getOrder{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await order.GetOrder(id);
            return result is not null ? Ok(result) : NoContent();
        }
    }
}

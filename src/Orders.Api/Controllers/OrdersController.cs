using Microsoft.AspNetCore.Mvc;
using Orders.Api.DTOs;
using Orders.Api.Entities;
using Orders.Api.Interfaces;

namespace Orders.Api.Controllers
{
    public class OrdersController(IOrderRepository orderRepository, ICatalogService catalogService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrders()
        {
            return Ok(await orderRepository.GetOrdersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {
            var order = await orderRepository.GetOrderByIdAsync(id);

            if(order == null) return NotFound();
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto createOrderDto, CancellationToken cancellationToken)
        {
            if(createOrderDto.Quantity <= 0) return BadRequest("Quantity must be positive");

            var product = await catalogService.GetProductAsync(createOrderDto.ProductId, cancellationToken);
            if(product == null) return NotFound($"Product with id {createOrderDto.ProductId} not found");

            if(product.Stock < createOrderDto.Quantity) return Conflict("Stock less than quantity");

            var order = new Order
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = createOrderDto.Quantity,
                Total = product.Price * createOrderDto.Quantity
            };

            orderRepository.AddOrder(order);

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }
    }
}

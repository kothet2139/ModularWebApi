using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModularWebApi.Modules.Orders.Application.Commands;
using ModularWebApi.Modules.Orders.Domain.Entities;
namespace ModularWebApi.Modules.Orders.API
{
    [Authorize]
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> OrderById(Guid orderId)
        {
            GetOrderCommand getOrderCommand = new GetOrderCommand(orderId);
            var order = await _mediator.Send(getOrderCommand);

            return Ok(order);
        }

        [HttpGet("confirm/{orderId}")]
        public async Task<IActionResult> ConfirmOrder(Guid orderId)
        {
            ConfirmOrderCommand confirmOrderCommand = new ConfirmOrderCommand(orderId);
            var isConfirmed = await _mediator.Send(confirmOrderCommand);

            if(isConfirmed)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(List<OrderItemDto> items)
        {
            var lstItems = items.Select(i => new OrderItem(i.ProductName, i.Quantity, i.Price)).ToList();
            var userId = HttpContext!.Items!["UserId"]!.ToString();

            var order = new Order(Guid.Parse(userId!), lstItems);

            CreateOrderCommand createOrderCommand = new CreateOrderCommand(order);
            var id = await _mediator.Send(createOrderCommand);

            return Ok(id);
        }
    }
}

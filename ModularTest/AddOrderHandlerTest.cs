using FluentAssertions;
using ModularWebApi.Modules.Orders.Application;
using ModularWebApi.Modules.Orders.Application.Commands;
using ModularWebApi.Modules.Orders.Application.Handlers;
using ModularWebApi.Modules.Orders.Domain.Entities;
using ModularWebApi.Modules.Orders.Domain.Repositories;
using Moq;

namespace ModularTest
{
    public class AddOrderHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldAddOrder()
        {
            var mockRepository = new Mock<IOrderRepository>();
            var order = new Order(Guid.NewGuid(), new List<OrderItem>
            {
                new OrderItem("Tablet", 2, 2000)
            });

            var Handler = new CreateOrderHandler(mockRepository.Object);
            var command = new CreateOrderCommand(order);

            var orderId = await Handler.Handle(command, CancellationToken.None);

            orderId.Should().NotBe(Guid.Empty);
            mockRepository.Verify(repo => repo.AddAsync(order), Times.Once);
        }
    }
}

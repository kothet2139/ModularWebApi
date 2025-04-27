using FluentAssertions;
using ModularWebApi.Modules.Orders.Application;
using ModularWebApi.Modules.Orders.Application.Commands;
using ModularWebApi.Modules.Orders.Application.Handlers;
using ModularWebApi.Modules.Orders.Domain.Entities;
using ModularWebApi.Modules.Orders.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularTest
{
    public class GetOrderHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldGetOrder()
        {
            var order = new Order(Guid.NewGuid(), new List<OrderItem>
            {
                new OrderItem("Xiaomi Tablet", 4, 4000)
            });

            var mockRepository = new Mock<IOrderRepository>();

            var Handler = new GetOrderHandler(mockRepository.Object);
            var getOrderCommand = new GetOrderCommand(order.Id);

            mockRepository.Setup(repo => repo.GetByIdAsync(order.Id))
                .ReturnsAsync(order);

            var result = await Handler.Handle(getOrderCommand, CancellationToken.None);

            result.Should().NotBeNull();
            result.Equals(order);

            mockRepository.Verify(repo => repo.GetByIdAsync(order.Id), Times.Once);
        }
    }
}

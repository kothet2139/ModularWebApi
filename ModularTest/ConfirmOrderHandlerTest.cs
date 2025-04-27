using FluentAssertions;
using MediatR;
using ModularWebApi.Modules.Orders.Application.Commands;
using ModularWebApi.Modules.Orders.Application.Handlers;
using ModularWebApi.Modules.Orders.Domain.Entities;
using ModularWebApi.Modules.Orders.Domain.Enum;
using ModularWebApi.Modules.Orders.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularTest
{
    public class ConfirmOrderHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldConfirmOrder()
        {
            var order = new Order(Guid.NewGuid(), new List<OrderItem>
            {
                new OrderItem("Phone", 1, 200)
            });

            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetByIdAsync(order.Id))
                .ReturnsAsync(order);

            var mockMediator = new Mock<IMediator>();
            var handler = new ConfirmOrderHandler(orderRepositoryMock.Object, mockMediator.Object);
            var command = new ConfirmOrderCommand(order.Id);

            await handler.Handle(command, CancellationToken.None);

            order.Status.Should().Be(OrderStatus.Confirmed);
            orderRepositoryMock.Verify(repo => repo.UpdateAsync(order), Times.Once);
        }
    }
}

using MediatR;
using ModularWebApi.Modules.Orders.Domain.Entities;

namespace ModularWebApi.Modules.Orders.Application.Commands
{
    public record GetOrderCommand(Guid id) : IRequest<Order>;
}

using MediatR;
using ModularWebApi.Modules.Orders.Domain.Entities;

namespace ModularWebApi.Modules.Orders.Application.Commands
{
    public record CreateOrderCommand(Order order) : IRequest<Guid>;
}

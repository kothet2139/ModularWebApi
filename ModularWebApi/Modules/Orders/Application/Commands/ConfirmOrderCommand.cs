using MediatR;

namespace ModularWebApi.Modules.Orders.Application.Commands
{
    public record ConfirmOrderCommand(Guid id) : IRequest<bool>;
}

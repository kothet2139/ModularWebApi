using MediatR;

namespace ModularWebApi.Modules.User.Application.Commands
{
    public record DisableUserCommand(Guid userId) : IRequest<bool>;
}

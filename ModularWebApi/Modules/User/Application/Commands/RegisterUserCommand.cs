using MediatR;

namespace ModularWebApi.Modules.User.Application.Commands
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<Guid>;
}

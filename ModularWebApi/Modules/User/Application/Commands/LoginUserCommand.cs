using MediatR;

namespace ModularWebApi.Modules.User.Application.Commands
{
    public record LoginUserCommand(string Email, string Password ) : IRequest<string>;
}

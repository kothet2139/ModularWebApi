using MediatR;
using ModularWebApi.Modules.User.Application.Commands;
using ModularWebApi.Modules.User.Domain.Repository;

namespace ModularWebApi.Modules.User.Application.Handlers
{
    public class DisableUserHandler : IRequestHandler<DisableUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public DisableUserHandler(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId);

            if (user == null)
                throw new InvalidOperationException("User not found.");

            user.Disabled();

            await _userRepository.UpdateAsync(user);

            return true;
        }
    }
}

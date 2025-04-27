using MediatR;
using ModularWebApi.Modules.User.Application.Commands;
using ModularWebApi.Modules.User.Domain.Repository;

namespace ModularWebApi.Modules.User.Application.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        public RegisterUserHandler(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.User(request.Email, request.Password, "Admin");
            await _userRepository.AddAsync(user);

            return user.Id;
        }
    }
}

using MediatR;
using ModularWebApi.Modules.User.Application.Commands;
using ModularWebApi.Modules.User.Domain;
using ModularWebApi.Modules.User.Domain.Repository;

namespace ModularWebApi.Modules.User.Application.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IJWTProvider _jWTProvider;
        private readonly IUserRepository _userRepository;
        public LoginUserHandler(IJWTProvider jWTProvider, IUserRepository userRepository) 
        {
            _jWTProvider = jWTProvider;
            _userRepository = userRepository;
        }
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                throw new InvalidOperationException("User not found.");

            return _jWTProvider.GenerateToken(user.Id, user.Role!);
        }
    }
}

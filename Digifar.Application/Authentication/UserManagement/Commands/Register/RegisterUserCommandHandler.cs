using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using MapsterMapper;
using MediatR;
using UserRoles = Digifar.Application.Authentication.Common.UserRoles;

namespace Digifar.Application.Authentication.UserManagement.Commands.Register
{
    public class RegisterUserCommandHandler(IUserRepository userRepository,
        IMapper mapper) : IRequestHandler<RegisterUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request);

            return await userRepository.RegisterAsync(user, UserRoles.USER);
        }
    }
}

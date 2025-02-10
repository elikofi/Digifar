using Digifar.Application.Authentication.Common;
using Digifar.Application.Common.Interfaces.Authentication;
using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Mapster;
using MediatR;

namespace Digifar.Application.Authentication.UserManagement.Queries.Login
{
    public class LoginUserQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginUserQuery, Result<AuthenticationResult>>
    {
        public async Task<Result<AuthenticationResult>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var signIn = await userRepository.Login(request.PhoneNumber, request.Otp);

            if(signIn.Success is false)
                return Result<AuthenticationResult>.ErrorResult(signIn.ErrorMessage!);

            var newSignIn = signIn.Data.Adapt<UserDTO>();

            var token = jwtTokenGenerator.GenerateToken(newSignIn);

            var authResult = new AuthenticationResult(newSignIn, token);

            return Result<AuthenticationResult>.SuccessResult(authResult);
        }
    }
}

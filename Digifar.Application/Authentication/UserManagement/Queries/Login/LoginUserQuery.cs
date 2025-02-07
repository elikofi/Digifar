using Digifar.Application.Authentication.Common;
using Digifar.Application.Common.Results;
using MediatR;

namespace Digifar.Application.Authentication.UserManagement.Queries.Login
{
    public record LoginUserQuery(string PhoneNumber, string Otp) : IRequest<Result<AuthenticationResult>>;
}

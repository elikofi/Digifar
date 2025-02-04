using Digifar.Application.Common.Results;
using MediatR;

namespace Digifar.Application.Authentication.UserManagement.Commands.Register
{
    public record RegisterUserCommand
        (
            string FirstName,
            string LastName,
            string UserName,
            string PasswordHash,
            string PhoneNumber
        ) : IRequest<Result<string>>;
}

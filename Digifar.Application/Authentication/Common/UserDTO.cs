namespace Digifar.Application.Authentication.Common
{
    public record UserDTO
        (
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string PhoneNumber,
        string Password
        );
}

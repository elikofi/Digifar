namespace Digifar.Application.Authentication.Common
{
    public record UserDTO
        (
        string Id,
        string FirstName,
        string LastName,
        string UserName,
        string PhoneNumber
        );
}

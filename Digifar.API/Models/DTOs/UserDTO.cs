namespace Digifar.API.Models.DTOs
{
    public record UserDTO
        (
        string Id,
        string FirstName,
        string LastName,
        string UserName,
        string Email
        );
}

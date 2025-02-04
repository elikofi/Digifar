namespace Digifar.Contracts.Authentication
{
    public record RegisterUserRequest(
        string FirstName,
        string LastName,
        string UserName,
        string PasswordHash,
        string PhoneNumber
        );
}
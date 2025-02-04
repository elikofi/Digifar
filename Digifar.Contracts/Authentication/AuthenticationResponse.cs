namespace Digifar.Contracts.Authentication
{
    public record AuthenticationResponse
        (
            string Id,
            string Username,
            string PhoneNumber,
            string Token
        );
}

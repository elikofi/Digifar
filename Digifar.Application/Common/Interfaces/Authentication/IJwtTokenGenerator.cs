
namespace Digifar.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string phoneNumber);
    }
}

using Digifar.API.Models.DTOs;

namespace Digifar.API.Repositories.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string phoneNumber);   
    }
}

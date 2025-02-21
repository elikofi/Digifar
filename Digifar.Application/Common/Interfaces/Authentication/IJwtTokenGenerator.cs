
using Digifar.Application.Authentication.Common;

namespace Digifar.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserDTO user);
    }
}

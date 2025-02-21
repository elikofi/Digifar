using Digifar.Application.Authentication.Common;
using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;

namespace Digifar.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<Result<string>> RegisterAsync(User user, string role);
        Task<Result<UserDTO>> Login(string phoneNumber, string otp);
        Task<string> SeedRoles();

    }
}

using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;

namespace Digifar.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<Result<string>> RegisterAsync(User user, string role);
        Task<Result<string>> Login(string phoneNumber);
        Task<string> SeedRoles();

    }
}

using Digifar.API.Common.Result;
using Digifar.API.Models.Entities;

namespace Digifar.API.Repositories.Interfaces.UserManagement
{
    public interface IUserRepository
    {
        Task<Result<string>> RegisterAsync(User user, string role);

    }
}

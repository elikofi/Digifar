using Digifar.API.Common.Result;
using Digifar.API.Data;
using Digifar.API.Models.DTOs;
using Digifar.API.Models.Entities;
using Digifar.API.Repositories.Interfaces.UserManagement;
using Microsoft.AspNetCore.Identity;

namespace Digifar.API.Repositories.Implementation.UserManagement
{
    public class UserRepository(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        DigifarDbContext context,
        SignInManager<User> signInManager,
        ILogger<UserDTO> logger) : IUserRepository
    {
        public Task<Result<string>> RegisterAsync(User user, string role)
        {
            throw new NotImplementedException();
        }
    }
}

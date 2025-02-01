using Digifar.API.Common.ConstantResponses;
using Digifar.API.Common.Errors;
using Digifar.API.Common.Result;
using Digifar.API.Data;
using Digifar.API.Models.DTOs;
using Digifar.API.Models.Entities;
using Digifar.API.Repositories.Interfaces.UserManagement;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Digifar.API.Repositories.Implementation.UserManagement
{
    public class UserRepository(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        DigifarDbContext context,
        SignInManager<User> signInManager,
        ILogger<UserDTO> logger) : IUserRepository
    {
        public async Task<Result<string>> RegisterAsync(User user, string role)
        {
            try
            {
                var userExists = await userManager.FindByEmailAsync(user.Email!);
                if (userExists == null)
                {

                    User newUser = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        UserName = user.UserName,
                        EmailConfirmed = true,
                        TwoFactorEnabled = false,
                        PhoneNumber = user.PhoneNumber,
                        IsOtpVerified = true,
                        SecurityStamp = Guid.NewGuid().ToString()

                    };

                    var registeredUser = await userManager.CreateAsync(newUser, user.PasswordHash!);

                    if (registeredUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, role);
                        return Result<string>.SuccessResult(ConstantResponses.RegisteredSuccessfully);
                    }
                    return Result<string>.ErrorResult($"{ConstantResponses.FailedRegistration} {registeredUser.Errors.FirstOrDefault()?.Description}");
                }
                return Result<string>.ErrorResult(Errors.DuplicateEmail);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

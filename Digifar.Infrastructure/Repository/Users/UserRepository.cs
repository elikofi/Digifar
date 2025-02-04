using Digifar.Application.Authentication.Common;
using Digifar.Application.Common.Constants;
using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Domain.Common.Errors;
using Digifar.Domain.Entities;
using Digifar.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Digifar.Infrastructure.Repository.Users
{
    public class UserRepository(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        DigifarDbContext context,
        SignInManager<User> signInManager,
        ILogger<UserDTO> logger) : IUserRepository
    {
        public Task<Result<string>> Login(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        //USER REGISTRATION
        public async Task<Result<string>> RegisterAsync(User user, string role)
        {
            try
            {
                var userExists = await userManager.FindByNameAsync(user.UserName!);
                if (userExists == null)
                {

                    User newUser = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        TwoFactorEnabled = false,
                        PhoneNumber = user.PhoneNumber,
                        PasswordHash = user.PasswordHash,
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
                return Result<string>.ErrorResult(Errors.DuplicateUsername);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> SeedRoles()
        {
            try
            {
                bool isSuperAdminRoleExists = await roleManager.RoleExistsAsync(UserRoles.SUPERADMIN);
                bool isAdminRoleExists = await roleManager.RoleExistsAsync(UserRoles.ADMIN);
                bool isUserRoleExists = await roleManager.RoleExistsAsync(UserRoles.USER);
                bool isSuperUserRoleExists = await roleManager.RoleExistsAsync(UserRoles.MERCHANT);

                if (isSuperAdminRoleExists && isAdminRoleExists && isUserRoleExists && isSuperUserRoleExists)
                {
                    return "Roles seeding already done.";
                }


                await roleManager.CreateAsync(new IdentityRole(UserRoles.SUPERADMIN));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.USER));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.MERCHANT));

                return "seeded roles.!";
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

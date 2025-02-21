using Digifar.Application.Authentication.Common;
using Digifar.Application.Common.Constants;
using Digifar.Application.Common.Interfaces.Authentication;
using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Domain.Common.Errors;
using Digifar.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Digifar.Infrastructure.Repository.Users
{
    public class UserRepository(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<User> signInManager,
        ILogger<UserDTO> logger, IOtpService otpService) : IUserRepository
    {

        //LOGIN USER
        public async Task<Result<UserDTO>> Login(string phoneNumber, string otp)
        {
            try
            {
                var user = await userManager.Users
                    .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

                if (user == null)
                    return Result<UserDTO>.ErrorResult(Errors.IncorrectPhonenumber);

                var otpVerificationResult = await otpService.VerifyOTP(phoneNumber, otp);

                if (otpVerificationResult.Success is false)
                    return Result<UserDTO>.ErrorResult(otpVerificationResult.ErrorMessage!);

                await signInManager.SignInAsync(user, isPersistent: false);

                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                        {
                            new(ClaimTypes.Name, user.UserName!),
                            new(ClaimTypes.NameIdentifier, user.Id),
                        };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                return Result<UserDTO>.SuccessResult(new UserDTO
                (
                    Id: user.Id,
                    FirstName: user.FirstName,
                    LastName: user.LastName,
                    UserName: user.UserName!,
                    PhoneNumber: user.PhoneNumber!
                ));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Errors.SignInFailure);
                throw;
            }
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
                        //Wallets =
                        //        [
                        //            new Wallet
                        //            {
                        //                Balance = 0m
                        //            }
                        //        ]

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

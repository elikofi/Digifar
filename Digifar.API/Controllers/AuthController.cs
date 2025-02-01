using Digifar.API.Common.Roles;
using Digifar.API.Models.DTOs;
using Digifar.API.Models.Entities;
using Digifar.API.Repositories.Interfaces.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace Digifar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserRepository _userRepository) : ControllerBase
    {
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                PasswordHash = user.Password
            };


            var result = await _userRepository.RegisterAsync(newUser, UserRoles.MERCHANT);

            if (result.Success is true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

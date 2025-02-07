using Digifar.Application.Authentication.Common;
using Digifar.Application.Authentication.UserManagement.Commands.OTP;
using Digifar.Application.Authentication.UserManagement.Commands.Register;
using Digifar.Application.Authentication.UserManagement.Queries.Login;
using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Digifar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender mediator, IMapper mapper, ILogger<UserDTO> logger, IUserRepository userRepository) : ControllerBase
    {
        //SEEDING ROLES.
        [HttpPost("SeedRoles")]
        public async Task<IActionResult> SeedRoles()
        {
            var seedRoles = await userRepository.SeedRoles();
            return Ok(seedRoles);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var user = mapper.Map<RegisterUserCommand>(request);

            var authResult = await mediator.Send(user);

            if (authResult.Success is false)
            {
                return BadRequest(authResult.ErrorMessage);
            }
            return Ok(authResult);
        }

        [HttpPost]
        [Route("ReqOtp")]
        public async Task<IActionResult> RequestOTP([FromBody] RequestOtpRequest request)
        {
            var mappedRequest = mapper.Map<RequestOtpCommand>(request);

            var otpResult = await mediator.Send(mappedRequest);

            if (otpResult.Success is false)
            {
                return BadRequest(otpResult.ErrorMessage);
            }
            Console.WriteLine(otpResult);
            return Ok(otpResult);
        }
        
        [HttpPost]
        [Route("VerOtp")]
        public async Task<IActionResult> VerifyOTP([FromBody] VerifyOtpRequest request)
        {
            var mappedRequest = mapper.Map<VerifyOtpCommand>(request);

            var verificationResult = await mediator.Send(mappedRequest);

            if (verificationResult.Success is false)
            {
                return BadRequest(verificationResult.ErrorMessage);
            }

            return Ok(verificationResult);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var mappedRequest = mapper.Map<LoginUserQuery>(request);

            var loginResult = await mediator.Send(mappedRequest);

            if (loginResult.Success is false)
            {
                return BadRequest(loginResult.ErrorMessage);
            }

            return Ok(loginResult);
        }
    }
}

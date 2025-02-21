using Digifar.Application.Authentication.Common;
using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Dashboard.WalletManagement.Commands;
using Digifar.Contracts.Wallets;
using Digifar.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Digifar.API.Controllers.Wallets
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController(ISender mediator, IMapper mapper, ILogger<Wallet> logger, IWalletRepository walletRepository) : ControllerBase
    {
        [HttpPost("AddWallet")]
        public async Task<IActionResult> CreateWallet([FromBody]CreateUserWalletRequest request)
        {
            var walletMap = mapper.Map<CreateUserWalletCommand>(request);

            var mapResult = await mediator.Send(walletMap);

            if(mapResult.Success is false)
                return BadRequest(mapResult.ErrorMessage);

            return Ok(mapResult);
        }

    }
}

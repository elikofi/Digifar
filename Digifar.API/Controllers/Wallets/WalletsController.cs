using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Dashboard.WalletManagement.Commands;
using Digifar.Application.Dashboard.WalletManagement.Queries;
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
        //CREATE
        [HttpPost("AddWallet")]
        public async Task<IActionResult> CreateWallet([FromBody]CreateUserWalletRequest request)
        {
            var walletMap = mapper.Map<CreateUserWalletCommand>(request);

            var mapResult = await mediator.Send(walletMap);

            if(mapResult.Success is false)
                return BadRequest(mapResult.ErrorMessage);

            return Ok(mapResult);
        }

        //GET ALL WALLETS OF A USER
        [HttpGet("GetWalletsOfaUser")]
        public async Task<IActionResult> GetUserWallets(string UserId)
        {
            var userId = new GetAllWalletsOfAUserRequest(UserId);

            var mappedUser = mapper.Map<GetAllWalletsOfAUserQuery>(userId);

            var mapResult = await mediator.Send(mappedUser);

            if (mapResult.Success is false)
                return BadRequest(mapResult.ErrorMessage);

            return Ok(mapResult);
        }

        //DELETE A WALLET
        [HttpDelete("DeleteUserWallet")]
        public async Task<IActionResult> DeleteUserWallets(Guid WalletId)       
        {
            var userId = new DeleteUserWalletRequest(WalletId);

            var mappedUser = mapper.Map<DeleteUserWalletCommand>(userId);

            var mapResult = await mediator.Send(mappedUser);

            if (mapResult.Success is false)
                return BadRequest(mapResult.ErrorMessage);

            return Ok(mapResult);
        }
    }
}

using System;
using System.Collections.Generic;
using Digifar.Application.Dashboard.WalletManagement.Commands;
using Digifar.Application.Dashboard.WalletManagement.Queries;
using Digifar.Contracts.Wallets;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;

namespace Digifar.API.Endpoints;

public static partial class EndpointGroups
{
    public static RouteGroupBuilder AddWalletEndpoints(this RouteGroupBuilder group)
    {
        // CREATE WALLET
        group.MapPost("AddWallet", async (IMediator mediator, IMapper mapper, [FromBody] CreateUserWalletRequest request) =>
            {
                var walletMap = mapper.Map<CreateUserWalletCommand>(request);
                var mapResult = await mediator.Send(walletMap);

                return mapResult.Success == true ? Results.Ok(mapResult) : Results.BadRequest(mapResult.ErrorMessage);
            })
            .WithName("CreateWallet")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Create a new wallet for a user";
                return operation;
            });

        // GET ALL WALLETS OF A USER
        group.MapGet("GetWalletsOfaUser", async (IMediator mediator, IMapper mapper, string UserId) =>
            {
                var userId = new GetAllWalletsOfAUserRequest(UserId);
                var mappedUser = mapper.Map<GetAllWalletsOfAUserQuery>(userId);
                var mapResult = await mediator.Send(mappedUser);

                return mapResult.Success == true ? Results.Ok(mapResult) : Results.BadRequest(mapResult.ErrorMessage);
            })
            .WithName("GetUserWallets")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Get all wallets of a user";
                operation.Parameters = new List<OpenApiParameter>
                {
                    new()
                    {
                        In = ParameterLocation.Query,
                        Name = "UserId",
                        Required = true,
                        Schema = new OpenApiSchema { Type = "string" }
                    }
                };
                return operation;
            });

        // DELETE A WALLET
        group.MapDelete("DeleteUserWallet", async (IMediator mediator, IMapper mapper, Guid WalletId) =>
            {
                var userId = new DeleteUserWalletRequest(WalletId);
                var mappedUser = mapper.Map<DeleteUserWalletCommand>(userId);
                var mapResult = await mediator.Send(mappedUser);

                return mapResult.Success == true ? Results.Ok(mapResult) : Results.BadRequest(mapResult.ErrorMessage);
            })
            .WithName("DeleteUserWallet")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Delete a wallet by ID";
                operation.Parameters = new List<OpenApiParameter>
                {
                    new()
                    {
                        In = ParameterLocation.Query,
                        Name = "WalletId",
                        Required = true,
                        Schema = new OpenApiSchema { Type = "string", Format = "uuid" }
                    }
                };
                return operation;
            });

        return group; 
    }
}
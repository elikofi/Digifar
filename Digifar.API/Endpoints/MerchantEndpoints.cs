using System;
using System.Collections.Generic;
using Digifar.Application.Dashboard.MerchantManagement.Commands;
using Digifar.Application.Dashboard.MerchantManagement.Queries;
using Digifar.Contracts.Merchants;
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
    public static RouteGroupBuilder AddMerchantEndpoints(this RouteGroupBuilder group)
    {
        // CREATE MERCHANT
        group.MapPost("CreateMerchant", async (IMediator mediator, IMapper mapper, [FromBody] CreateMerchantRequest request) =>
            {
                var merchantMap = mapper.Map<CreateMerchantCommand>(request);
                var mapResult = await mediator.Send(merchantMap);

                return mapResult.Success == true ? Results.Ok(mapResult) : Results.BadRequest(mapResult.ErrorMessage);
            })
            .WithName("CreateMerchant")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Create a new merchant account";
                return operation;
            });

        // GET MERCHANT BY ID
        group.MapGet("GetMerchantById", async (IMediator mediator, IMapper mapper, Guid MerchantId) =>
            {
                var merchantId = new GetMerchantByIdRequest(MerchantId);
                var mappedMerchant = mapper.Map<GetMerchantByIdQuery>(merchantId);
                var mapResult = await mediator.Send(mappedMerchant);

                return mapResult.Success == true ? Results.Ok(mapResult) : Results.BadRequest(mapResult.ErrorMessage);
            })
            .WithName("GetMerchantById")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Get merchant details by ID";
                operation.Parameters = new List<OpenApiParameter>
                {
                    new()
                    {
                        In = ParameterLocation.Query,
                        Name = "MerchantId",
                        Required = true,
                        Schema = new OpenApiSchema { Type = "string", Format = "uuid" }
                    }
                };
                return operation;
            });

        // DELETE MERCHANT BY ID
        group.MapDelete("DeleteMerchant", async (IMediator mediator, IMapper mapper, Guid MerchantId) =>
            {
                var merchantId = new DeleteMerchantRequest(MerchantId);
                var mappedMerchant = mapper.Map<DeleteMerchantCommand>(merchantId);
                var mapResult = await mediator.Send(mappedMerchant);

                return mapResult.Success == true ? Results.Ok(mapResult) : Results.BadRequest(mapResult.ErrorMessage);
            })
            .WithName("DeleteMerchant")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Delete a merchant by ID";
                operation.Parameters = new List<OpenApiParameter>
                {
                    new()
                    {
                        In = ParameterLocation.Query,
                        Name = "MerchantId",
                        Required = true,
                        Schema = new OpenApiSchema { Type = "string", Format = "uuid" }
                    }
                };
                return operation;
            });

        // UPDATE MERCHANT BY ID
        group.MapPut("UpdateMerchant", async (IMediator mediator, IMapper mapper, [FromBody] UpdateMerchantRequest request) =>
            {
                var merchantMap = mapper.Map<UpdateMerchantCommand>(request);
                var mapResult = await mediator.Send(merchantMap);

                return mapResult.Success == true ? Results.Ok(mapResult) : Results.BadRequest(mapResult.ErrorMessage);
            })
            .WithName("UpdateMerchant")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Update a merchant by ID";
                return operation;
            });

        // GET ALL MERCHANTS
        group.MapGet("GetAllMerchants", async (IMediator mediator, IMapper mapper) =>
            {
                var query = new GetAllMerchantsQuery();
                var mapResult = await mediator.Send(query);

                return mapResult.Success == true ? Results.Ok(mapResult) : Results.BadRequest(mapResult.ErrorMessage);
            })
            .WithName("GetAllMerchants")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Get all merchants";
                return operation;
            });

        return group;
    }
}
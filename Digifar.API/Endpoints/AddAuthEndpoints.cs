using System.Collections.Generic;
using Digifar.Application.Authentication.UserManagement.Commands.OTP;
using Digifar.Application.Authentication.UserManagement.Commands.Register;
using Digifar.Application.Authentication.UserManagement.Queries.Login;
using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Contracts.Authentication;
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
    public static RouteGroupBuilder AddAuthEndpoints(this RouteGroupBuilder group)
    {
        // SEEDING ROLES
        group.MapPost("SeedRoles", async (IUserRepository userRepository) =>
        {
            var seedRoles = await userRepository.SeedRoles();
            return Results.Ok(seedRoles);
        })
        .WithName("SeedRoles")
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Seed roles into the database";
            operation.Description = "This endpoint seeds predefined roles into the database.";
            return operation;
        });

        // REGISTER USER
        group.MapPost("Register", async (IMediator mediator, IMapper mapper, [FromBody] RegisterUserRequest request) =>
        {
            var command = mapper.Map<RegisterUserCommand>(request);
            var result = await mediator.Send(command);

            return result.Success == true ? Results.Ok(result) : Results.BadRequest(result.ErrorMessage);
        })
        .WithName("RegisterUser")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Register a new user";
            operation.Description = "This endpoint registers a new user with the provided details.";
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "RegisterUserRequest" }
                        }
                    }
                }
            };
            return operation;
        });

        // REQUEST OTP
        group.MapPost("ReqOtp", async (IMediator mediator, IMapper mapper, [FromBody] RequestOtpRequest request) =>
        {
            var command = mapper.Map<RequestOtpCommand>(request);
            var result = await mediator.Send(command);

            return result.Success == true ? Results.Ok(result) : Results.BadRequest(result.ErrorMessage);
        })
        .WithName("RequestOTP")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Request an OTP for user verification";
            operation.Description = "This endpoint requests an OTP for user verification.";
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "RequestOtpRequest" }
                        }
                    }
                }
            };
            return operation;
        });

        // VERIFY OTP
        group.MapPost("VerOtp", async (IMediator mediator, IMapper mapper, [FromBody] VerifyOtpRequest request) =>
        {
            var command = mapper.Map<VerifyOtpCommand>(request);
            var result = await mediator.Send(command);

            return result.Success == true ? Results.Ok(result) : Results.BadRequest(result.ErrorMessage);
        })
        .WithName("VerifyOTP")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Verify an OTP for user authentication";
            operation.Description = "This endpoint verifies an OTP for user authentication.";
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "VerifyOtpRequest" }
                        }
                    }
                }
            };
            return operation;
        });

        // LOGIN
        group.MapPost("Login", async (IMediator mediator, IMapper mapper, [FromBody] LoginRequest request) =>
        {
            var query = mapper.Map<LoginUserQuery>(request);
            var result = await mediator.Send(query);

            return result.Success == true ? Results.Ok(result) : Results.BadRequest(result.ErrorMessage);
        })
        .WithName("Login")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Login a user";
            operation.Description = "This endpoint logs in a user with the provided credentials.";
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new()
                    {
                        Schema = new OpenApiSchema
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "LoginRequest" }
                        }
                    }
                }
            };
            return operation;
        });

        return group;
    }
}
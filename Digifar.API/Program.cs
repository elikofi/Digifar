using Digifar.API.Endpoints;
using Digifar.Application;
using Digifar.Application.Common.Exceptions;
using Digifar.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Scalar.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger/OpenAPI generation (required for Scalar)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Digifar API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuring Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger (OpenAPI specification) - Required for Scalar
    app.UseSwagger();

    // Enable Scalar API Reference
    app.MapScalarApiReference(options =>
    {
        options.OpenApiRoutePattern = "/swagger/v1/swagger.json"; // Path to your OpenAPI specification
        options.EndpointPathPrefix = "/scalar"; // Customize the path where Scalar UI will be served
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });

    // Redirect root URL to Scalar UI
    app.MapGet("/", async context =>
    {
        context.Response.Redirect("/scalar/v1", false);
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

// Exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

// Enable CORS
app.UseCors("AllowAllOrigins");

// Wallet API
app.MapGroup("api/v1/wallets")
    .WithTags("Wallets")
    .WithOpenApi(operation =>
    {
        operation.Tags = new[] { new OpenApiTag() { Name = "Wallets" } };
        return operation;
    })
    .AddWalletEndpoints();

// Add Auth Endpoints
app.MapGroup("api/v1/auth")
    .WithTags("Authentication")
    .WithOpenApi(operation =>
    {
        operation.Tags = new[] { new OpenApiTag() { Name = "Authentication" } };
        return operation;
    })
    .AddAuthEndpoints();

// Merchant Endpoints
app.MapGroup("api/v1/merchants")
    .WithTags("Merchants")
    .WithOpenApi(operation =>
    {
        operation.Tags = new[] { new OpenApiTag() { Name = "Merchants" } };
        return operation;
    })
    .AddMerchantEndpoints();

// Fallback for SPAs
app.MapFallbackToFile("index.html");

app.Run();
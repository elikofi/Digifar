using Digifar.API.Models.JWT;
using Digifar.API.Repositories.Implementation.Authentication;
using Digifar.Application.Common.Interfaces.Authentication;
using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Interfaces.Services;
using Digifar.Domain.Entities;
using Digifar.Infrastructure.Authentication;
using Digifar.Infrastructure.Data;
using Digifar.Infrastructure.Repository.Users;
using Digifar.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Digifar.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuth(configuration);

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOtpService, OtpService>();



            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DigifarDbContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<DigifarDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Default"));
            });
            return services;
        }


        public static IServiceCollection AddAuth(
        this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    });

            return services;
        }
    }
}

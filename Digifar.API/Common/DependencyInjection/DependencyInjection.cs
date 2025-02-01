using MapsterMapper;
using System.Reflection;
using MediatR;
using Digifar.API.Common.Behaviours;
using FluentValidation;
using Digifar.API.Models.JWT;
using Digifar.API.Repositories.Implementation.Authentication;
using Digifar.API.Repositories.Interfaces.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Digifar.API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Digifar.API.Data;
using Digifar.API.Repositories.Interfaces.UserManagement;
using Digifar.API.Repositories.Implementation.UserManagement;



namespace Digifar.API.Common.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

    }
}

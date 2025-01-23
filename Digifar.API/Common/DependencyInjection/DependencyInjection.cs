using MapsterMapper;
using System.Reflection;
using MediatR;
using Digifar.API.Common.Behaviours;
using FluentValidation;



namespace Digifar.API.Common.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDI(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}

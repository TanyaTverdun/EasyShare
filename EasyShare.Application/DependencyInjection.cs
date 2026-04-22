using EasyShare.Application.Common.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyShare.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(DependencyInjection).Assembly);
            });

            return services;
        }
    }
}

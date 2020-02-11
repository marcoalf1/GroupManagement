using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlayBall.GroupManagement.Business.Impl.Services;
using PlayBall.GroupManagement.Business.Services;
using System;

namespace Playball.GroupManagement.Web.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBussiness(this IServiceCollection services)
        {

            services.AddScoped<IGroupsService, GroupsService>();

            return services;

        }

        public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }


    }
}

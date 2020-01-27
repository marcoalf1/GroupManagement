using Microsoft.Extensions.DependencyInjection;
using PlayBall.GroupManagement.Business.Impl.Services;
using PlayBall.GroupManagement.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playball.GroupManagement.Web.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBussiness(this IServiceCollection services)
        { 

            services.AddSingleton<IGroupsService, InMemoryGroupsService>();

            return services;

        }
    }
}

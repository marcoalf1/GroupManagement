using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playball.GroupManagement.Web.Demo;
using Playball.GroupManagement.Web.IoC;
using System;
using System.Threading.Tasks;

namespace Playball.GroupManagement.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
            //var value = _config.GetValue<int>("SomeRoot:SomeSubRoot:SomeKey");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Using IOptions
            //services.Configure<SomeRootConfiguration>(_config.GetSection("SomeRoot"));

            // Injecting POCO
            //var someRootConfiguration = new SomeRootConfiguration();
            //_config.GetSection("SomeRoot").Bind(someRootConfiguration);
            //services.AddSingleton(someRootConfiguration);

            // Injecting POCO, but prettier 
            services.ConfigurePOCO<SomeRootConfiguration>(_config.GetSection("SomeRoot"));

            services.ConfigurePOCO<DemoSecretsConfiguration>(_config.GetSection("DemoSecrets"));

            //if using default DI container, uncoment
            //services.AddBussiness();

            // Add Autofac
            var containerBluider = new ContainerBuilder();
            containerBluider.RegisterModule<AutofacModule>();
            containerBluider.Populate(services);

            var container = containerBluider.Build();
            return new AutofacServiceProvider(container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("X-Powered-By","ASP.NET Core: From 0 to overkill");
                    return Task.CompletedTask;
                });
                
                await next.Invoke();
            });

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}

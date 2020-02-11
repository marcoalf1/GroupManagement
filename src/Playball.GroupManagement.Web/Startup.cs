using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playball.GroupManagement.Web.IoC;
using PlayBall.GroupManagement.Data;
using System.Threading.Tasks;

[assembly: ApiController]
namespace Playball.GroupManagement.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRequiredMvcComponentes();

            services.AddDbContext<GroupManagementDbContext>(options => 
            {
                options.UseSqlServer(_config.GetConnectionString("GroupManagementDbContext"));
                options.EnableSensitiveDataLogging();
                
            });

            //if using default DI container, uncoment
            services.AddBussiness();

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

            app.Run(async (context) => { await context.Response.WriteAsync("No middlewares could handle the request"); });

        }
    }
}

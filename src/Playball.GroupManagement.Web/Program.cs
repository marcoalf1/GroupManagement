using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Playball.GroupManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("############################# STARTING APPLICATION #############################");
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(builder => { builder.ClearProviders(); })
            .UseNLog()
            .UseStartup<Startup>();
    }
}

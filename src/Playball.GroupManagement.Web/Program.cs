using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using LogLevel = NLog.LogLevel;

namespace Playball.GroupManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("############################# STARTING APPLICATION #############################");
            ConfigueNLog();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(builder => 
            {
                builder.ClearProviders(); // Clear default providers, NLog will handle it
                builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); // Set minimun level to trace, NLog rules will kick in afterwards
            })
            .UseNLog()
            .UseStartup<Startup>();
        


    }
}

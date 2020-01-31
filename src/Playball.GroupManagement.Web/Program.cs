using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;

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
            .ConfigureLogging(builder => { builder.ClearProviders(); })
            .UseNLog()
            .UseStartup<Startup>();
        
        // TODO: replace with nlog.config
        private static void ConfigueNLog()
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget("coloredConsole")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);

            var fileTarget = new FileTarget("file")
            {
                FileName = "${basedir}/file.log",
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception} ${ndlc}"
            };
            config.AddTarget(fileTarget);

            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, consoleTarget);
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, consoleTarget, "PlayBall.GroupManagement.Web.IoC.*");
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, consoleTarget);
            config.AddRule(NLog.LogLevel.Warn, NLog.LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;

        }


    }
}

// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Azure.Identity;

namespace CoronaTracker.Core
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.ConfigureAppConfiguration(config =>
                   {
                       var connectionString = config.Build().GetConnectionString("AppConfiguration");
                       config.AddAzureAppConfiguration(connectionString);
                   });
                   webBuilder.UseStartup<Startup>();
               });
        }
    } 
}


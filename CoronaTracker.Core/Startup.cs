// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using CoronaTracker.Core.Brokers.Apis;
using CoronaTracker.Core.Brokers.Configurations;
using CoronaTracker.Core.Brokers.DateTimes;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Brokers.Queues;
using CoronaTracker.Core.Brokers.Storages;
using CoronaTracker.Core.Services.Foundations.Countries;
using CoronaTracker.Core.Services.Foundations.ExternalCountries;
using CoronaTracker.Core.Services.Foundations.ExternalCountryEvents;
using CoronaTracker.Core.Services.Orchestrations.Countries;
using CoronaTracker.Core.Services.Processings.Countries;
using CoronaTracker.Core.Services.Processings.CountryEvents;
using CoronaTracker.Core.Services.Processings.ExternalCountries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CoronaTracker.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddControllers();
            services.AddHttpClient();
            services.AddDbContext<StorageBroker>();
            AddBrokers(services);
            AddServices(services);

            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = "CoronaTracker.Core",
                    Version = "v1"
                };

                options.SwaggerDoc(
                    name: "v1",
                    info: openApiInfo);
            });
            services.AddAzureAppConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json",
                    name: "CoronaTracker.Core v1");
            });

            app.UseHttpsRedirection();
            app.UseAzureAppConfiguration();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddTransient<IApiBroker, ApiBroker>();
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<IQueueBroker, QueueBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
            services.AddTransient<IDateTimeBroker, DateTimeBroker>();
            services.AddTransient<IConfigurationBroker, ConfigurationBroker>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IExternalCountryService, ExternalCountryService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IExternalCountryProcessingService, ExternalCountryProcessingService>();
            services.AddTransient<ICountryProcessingService, CountryProcessingService>();
            services.AddTransient<IExternalCountryProcessingService, ExternalCountryProcessingService>();
            services.AddTransient<IExternalCountryEventService, ExternalCountryEventService>();
            services.AddTransient<IExternalCountryEventProcessingService, ExternalCountryEventProcessingService>();
            services.AddTransient<ICountryOrchestrationService, CountryOrchestrationService>();
        }
    }
}

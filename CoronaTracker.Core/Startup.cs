using CoronaTracker.Core.Brokers.APIs;
using CoronaTracker.Core.Brokers.Loggings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CoronaTracker.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddControllers();
            services.AddHttpClient();

            services.AddTransient<IApiBroker, ApiBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();


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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                 {
                     options.SwaggerEndpoint(
                        url: "/swagger/v1/swagger.json",
                        name: "CoronaTracker.Core v1");
                 });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}

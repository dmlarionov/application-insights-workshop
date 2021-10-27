using dog.Services;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace dog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(ITelemetryChannel), new InMemoryChannel() { DeveloperMode = true });
            services.AddSingleton<ITelemetryInitializer>(new CustomTelemetryInitializer());
            services.AddApplicationInsightsTelemetry();

            services.AddSingleton<DogService>();
            services.AddSingleton<VaccinationService>();

            services.AddHttpClient("grooming", c => c.BaseAddress = new Uri(Configuration["Endpoints:grooming"])).AddPolicyHandler(GetRetryPolicy());
            services.AddHttpClient("vaccination", c => c.BaseAddress = new Uri(Configuration["Endpoints:vaccination"])).AddPolicyHandler(GetRetryPolicy());
            services.AddHttpClient("sterilization", c => c.BaseAddress = new Uri(Configuration["Endpoints:sterilization"])).AddPolicyHandler(GetRetryPolicy());

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}

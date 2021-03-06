using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apigw
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
            //var appInsightsOptions = new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions();
            //appInsightsOptions.EnableAdaptiveSampling = false;
            services.AddSingleton(typeof(ITelemetryChannel), new InMemoryChannel() { DeveloperMode = true });
            services.AddSingleton<ITelemetryInitializer>(new CustomTelemetryInitializer());
            services.AddApplicationInsightsTelemetry();

            services.AddHttpClient("pet", c => c.BaseAddress = new Uri(Configuration["Endpoints:pet"]));
            services.AddHttpClient("foo", c => c.BaseAddress = new Uri(Configuration["Endpoints:foo"]));
            services.AddHttpClient("bar", c => c.BaseAddress = new Uri(Configuration["Endpoints:bar"]));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
            //var builder = configuration.DefaultTelemetrySink.TelemetryProcessorChainBuilder;

            //// Using fixed rate sampling
            //double fixedSamplingPercentage = 100;
            //builder.UseSampling(fixedSamplingPercentage);
            //builder.Build();

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
    }
}

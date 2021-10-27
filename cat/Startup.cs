using cat.Models;
using cat.Services;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cat
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

            services.AddSingleton<CatService>();

            services.AddHttpClient("grooming", c => c.BaseAddress = new Uri(Configuration["Endpoints:grooming"]));
            services.AddHttpClient("vaccination", c => c.BaseAddress = new Uri(Configuration["Endpoints:vaccination"]));
            services.AddHttpClient("sterilization", c => c.BaseAddress = new Uri(Configuration["Endpoints:sterilization"]));

            services.AddRouting();

            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(e =>
            {
                var service = e.ServiceProvider.GetRequiredService<CatService>();
                e.MapGet("/api/cat", async c => await c.Response.WriteAsJsonAsync(await service.GetAll()));
                //e.MapGet("/api/cat/{id:string}", async c => await c.Response.WriteAsJsonAsync(await service.Get(Guid.Parse((string)c.Request.RouteValues["id"]))));
                e.MapPost("/api/cat",
                    async c =>
                    {
                        service.Add(await c.Request.ReadFromJsonAsync<Cat>());
                        c.Response.StatusCode = 201;
                    });
                //e.MapDelete("/api/cat/{id:string}",
                //    async c =>
                //    {
                //        await service.Delete(Guid.Parse((string)c.Request.RouteValues["id"]));
                //        c.Response.StatusCode = 204;
                //    });
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}

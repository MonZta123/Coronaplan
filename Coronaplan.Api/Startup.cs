using System.IO;
using BeastSources.Coronaplan.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace BeastSources.Coronaplan.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(n => n.Filters.Add(typeof(ErrorExceptionFilter)));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Coronplan Api",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coronaplan Api"));
            }
            else
                app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.Use(async (httpContext, next) =>
            {
                await next();
                if (httpContext.Response.StatusCode == 404 &&
                    !Path.HasExtension(httpContext.Request.Path.Value) &&
                    !httpContext.Request.Path.Value.StartsWith("/api/") &&
                    !httpContext.Request.Path.Value.StartsWith("/swagger/"))
                {
                    httpContext.Request.Path = "/";
                    await next();
                }
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
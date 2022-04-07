using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Server.Extensions;
using System.IO;
namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(
                string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            services.AddMemoryCache();
            services.AddOptions();
            services.ConfigureRateLimitingOptions();
            services.AddHttpContextAccessor();

            services.ConfigureVersioning();
            services.ConfigureSwagger();

            services.AddAutoMapper(typeof(Startup));
            services.ConfigureSqlContext(Configuration);
            services.ConfigureLoggerService();
            services.ConfigureRepositoryManager();
            services.CoonfigureAuthenticationManager();
            services.ValidationAttributes();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Fridges API v1");
                });
            }
            else
            {
                app.UseHsts();
            }


            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseIpRateLimiting();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

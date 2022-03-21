using Contracts;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Repository;
using Server.ActionFilters;
namespace Server.Extensions
{
    public static class ServiceExtensions
    {
        public static void ValidationAttributes(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidateProductExistsAttribute>();
            services.AddScoped<ValidateFridgeModelExistsAttribute>();
            services.AddScoped<ValidateFridgeExistsAttribute>();
            services.AddScoped<ValidateFridgeProductExistsAttribute>();
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
                     b.MigrationsAssembly("Server")));
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
    }
}

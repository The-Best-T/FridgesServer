using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Threading.Tasks;
namespace Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await DbInitialize(host);
            await AddMigrations(host);

            host.Run();
        }

        private static async Task AddMigrations(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var db = services.GetRequiredService<RepositoryContext>();
                await db.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILoggerManager>();
                logger.LogError($"An error occurred while seeding the database: {ex}.");
            }
        }

        private static async Task DbInitialize(IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await AdminInitializer.InitializeAsync(userManager, rolesManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILoggerManager>();
                logger.LogError($"An error occurred while seeding the database: {ex}.");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineClinic.Models.AuthorizationAuthentication.Context;
using OnlineClinic.Models.AuthorizationAuthentication.Model;
using OnlineClinic.Models.Db;
using System;


namespace OnlineClinic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ClinicContext>();

                    var accountContext = services.GetRequiredService<AccountDbContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    Models.AuthorizationAuthentication.InitializerAcaunt.DbInitializer.InitializeAsync(accountContext, userManager, rolesManager).Wait();
                    Models.Db.Inittializer.DbInitializer.InitializeAsync(context).Wait();

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ошибка при инициализации базы данных");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

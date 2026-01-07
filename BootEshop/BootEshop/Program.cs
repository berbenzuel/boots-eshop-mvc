using System.Security.Claims;
using BootEshop.Models;

using BootEshop.Models.Services;
using BootEshop.Services;
using Database;
using Database.Entities;
using DatabaseManager.Services.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EshopContext = Database.EshopContext;

namespace BootEshop
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Configuration.AddYamlFile("appsettings.yaml", optional: false, reloadOnChange: true);
            
            

            var dbCfg= builder.Configuration.GetSection("appConfig:database").Get<DatabaseConfig>()
                ?? throw new ArgumentNullException("appsettings.yaml", "Database configuration not found");

            // adding context to services
            object value = builder.Services.AddDbContext<EshopContext>(o =>
            {
                o.UseMySQL(dbCfg.ConnectionString);
            });
            
            builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<EshopContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            
            
            builder.Services.Configure<AppConfig>(
                builder.Configuration.GetSection("appConfig"));

            builder.Services.AddEshopServices();
            
            
            
           
  
            var app = builder.Build();
            
            
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
                }

                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>("User"));
                }
                
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<User>>();

                var adminEmail = "admin@admin";
                var adminPassword = "123456Ab";

                var admin = await userManager.FindByEmailAsync(adminEmail);

                if (admin == null)
                {
                    admin = new User
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(admin, adminPassword);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "Admin");
                    }
                }
            }
            
            
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

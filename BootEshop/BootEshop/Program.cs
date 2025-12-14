using BootEshop.Models;
using Microsoft.EntityFrameworkCore;

namespace BootEshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Configuration.AddYamlFile("./appsettings.yaml", optional: false, reloadOnChange: true);
            
            

            var dbCfg= builder.Configuration.GetSection("appConfig:database").Get<DatabaseConfig>()
                ?? throw new ArgumentNullException("appsettings.yaml", "Database configuration not found");
            
            // adding context to services
            builder.Services.AddDbContext<EshopContext>(o =>
            {
                o.UseMySQL(dbCfg.ConnectionString);
            });
            
            builder.Services.Configure<AppConfig>(
                builder.Configuration.GetSection("appConfig"));
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

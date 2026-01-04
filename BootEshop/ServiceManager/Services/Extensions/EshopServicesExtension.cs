using BootEshop.Models.Services;
using BootEshop.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseManager.Services.Extensions;



public static class EshopServicesExtension
{
    public static IServiceCollection AddEshopServices(this IServiceCollection services)
    {
        services.AddScoped<ProductService>();
        services.AddScoped<OrderService>();
        services.AddScoped<UserService>();
        services.AddScoped<ColorService>();
        services.AddScoped<SizeService>();
        services.AddScoped<ManufacturerService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<SourceService>();
        
        
        return services;
    }
    
}
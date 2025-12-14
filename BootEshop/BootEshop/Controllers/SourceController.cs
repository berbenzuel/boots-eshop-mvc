using BootEshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BootEshop.Controllers;


public class SourceController : Controller
{
    private SourceConfig _config;
    private readonly IWebHostEnvironment _env;
    
    public SourceController(IOptions<AppConfig> config, IWebHostEnvironment env)
    {
        _config = config.Value.Source;
        _env = env;
    }
    
    public IActionResult ProductImage(Guid id)
    {
        var path = Path.Combine(_env.ContentRootPath ,_config.ProductImage, $"{id}.png");
        
        if (!System.IO.File.Exists(path))
             return NotFound();
            
        Response.Headers.CacheControl = "public,max-age=604800";
        return PhysicalFile(path, "image/png");
    }
}
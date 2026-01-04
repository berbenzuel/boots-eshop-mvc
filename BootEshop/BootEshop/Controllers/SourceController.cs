using BootEshop.Models;
using DatabaseManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BootEshop.Controllers;


public class SourceController : Controller
{
    private SourceService _sourceService;
    
    
    public SourceController( SourceService sourceService)
    {
        _sourceService = sourceService;
    }
    
    public IActionResult CatalogProductImage(Guid id)
    {
        var path = _sourceService.GetProductMainImagePath(id);
        return path is not null ? PhysicalFile(path, "image/webp") : NotFound(path);
    }
    

    public IActionResult ProductImage(string filename)
    {
        var path = _sourceService.GetProductImagePath(filename);
        Response.Headers.CacheControl = "public,max-age=604800";
        return path is not null ? PhysicalFile(path, "image/webp") : NotFound(path);
    }

    public IActionResult CarrouselImage(int id)
    {
        var path = _sourceService.GetCarouselImagePath(id);
        Response.Headers.CacheControl = "public,max-age=604800";
        return PhysicalFile(path, "image/png");
    }

    // public IActionResult CategoryImage(int id)
    // {
    //     var path = Path.Combine(_env.ContentRootPath, _config.CategoryImage, $"{id}");
    //     if (!System.IO.File.Exists(path))
    //         return NotFound();
    //         
    //     Response.Headers.CacheControl = "public,max-age=604800";
    //     return PhysicalFile(path, "image/png");
    // }
}
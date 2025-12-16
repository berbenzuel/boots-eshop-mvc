using BootEshop.Models.Services;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.Controllers;

public class ProductController : Controller
{
    private readonly EshopContext  _context;
    
    public ProductController(EshopContext context)
    {
        _context = context;
    }

    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    
}
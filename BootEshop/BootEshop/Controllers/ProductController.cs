using System.Runtime.CompilerServices;
using BootEshop.Models.Services;
using BootEshop.Services;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.Controllers;

public class ProductController : Controller
{
    
    ProductService _productService;
    
    public ProductController(ProductService service)
    {
        _productService = service;
    }

    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    
}
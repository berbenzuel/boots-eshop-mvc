using BootEshop.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.Controllers;

public class ProductController : Controller
{
    private readonly ProductService _productService;
    
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
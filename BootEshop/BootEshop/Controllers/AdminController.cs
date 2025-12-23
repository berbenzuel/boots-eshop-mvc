using BootEshop.Services;
using BootEshop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.Controllers;

public class AdminController : Controller
{
    private readonly ProductService _productService;

    public AdminController(ProductService productService)
    {
        _productService = productService;
    }
    
    // GET
    public IActionResult Index()
    {
        return View(new AdminViewModel()
        {
            OrderCountToday = 10,
            OrderTotal = 100
        });
    }

    public IActionResult ProductOverview()
    {
        var products = _productService.GetEntities();
        return View(products.AsEnumerable());
    }
    
}
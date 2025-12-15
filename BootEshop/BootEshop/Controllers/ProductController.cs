using Microsoft.AspNetCore.Mvc;

namespace BootEshop.Controllers;

public class ProductController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
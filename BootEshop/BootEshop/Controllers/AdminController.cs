using BootEshop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(new AdminViewModel()
        {
            OrderCountToday = 10,
            OrderTotal = 100
        });
    }
    
}
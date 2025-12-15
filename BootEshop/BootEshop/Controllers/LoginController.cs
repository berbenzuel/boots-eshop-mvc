using BootEshop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.Features.Login;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        return View();
    }
}
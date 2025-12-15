using Microsoft.AspNetCore.Mvc;

namespace BootEshop.Features.Login;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
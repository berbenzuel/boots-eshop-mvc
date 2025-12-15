using System.Diagnostics;
using BootEshop.Controllers.Services;
using BootEshop.Models;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly CatalogService _catalogService;

        public HomeController(CatalogService service)
        {
            _catalogService = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using BootEshop.Services;
using BootEshop.ViewArgs;
using BootEshop.ViewModels;
using Database.Entities;
using DatabaseManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.Controllers;

public class CatalogController : Controller
{

    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    public CatalogController(ProductService productService,  CategoryService categoryService)
    {
        this._productService = productService;
        this._categoryService = categoryService;
    }

    public IActionResult BoyCatalog()
    {
        return RedirectToAction(
            nameof(Index),
            new { productSelection = nameof(ProductSelection.ManProduct) }
        );
    }

    public IActionResult GirlCatalog()
    {
        return RedirectToAction(
            nameof(Index),
            new { productSelection = nameof(ProductSelection.WomanProduct) }
        );
    }


    public ActionResult Index(string productSelection)
    {
        Enum.TryParse<ProductSelection>(productSelection, out var productSelectionEnum);
        var filter = new ProductFilterModelDto();
        filter.CategoryId = _categoryService.GetCategory(productSelectionEnum).Id;

        var model = new CatalogViewModel()
        {
            FilterModel = filter,
        };
        
        return View(model);
    }
    
    
    
    [HttpPost]
    public IActionResult CatalogGrid([FromBody] ProductFilterModelDto filter)
    {
        return ViewComponent("CatalogGrid", new { filter });
    }
}
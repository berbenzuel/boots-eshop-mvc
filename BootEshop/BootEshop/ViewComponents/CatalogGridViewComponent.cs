using BootEshop.Models.Services;
using BootEshop.Services;
using BootEshop.ViewArgs;
using BootEshop.ViewModels;
using Database.Entities;
using DatabaseManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.ViewComponents;

public class CatalogGridViewComponent : ViewComponent
{

    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    
    
    public CatalogGridViewComponent(ProductService service, CategoryService categoryService)
    {
        _productService = service;
        _categoryService = categoryService;
    }
    
    public IViewComponentResult Invoke(ProductFilterModelDto filter)
    {
        var categories = _categoryService.GetEntities().ToList();
        var query = _productService.GetProducts(filter);
        
        
        var sizes = query.SelectMany(p => p.ProductSizes).Distinct();
        var manufacturers  = query.Select(p => p.Manufacturer).Distinct().ToList();
        var colors = query.SelectMany(p => p.ProductColors).Distinct().ToList();
        var products = query.ToList();
        
        var viewModel = new CatalogGridViewModel(filter, manufacturers, categories, sizes, colors, products);
        
        return View(viewModel); 
    }
}
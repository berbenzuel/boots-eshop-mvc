using BootEshop.Models;

using BootEshop.Models.Services;
using BootEshop.Services;
using BootEshop.ViewArgs;
using BootEshop.ViewModels;
using Database.Entities;

using Microsoft.AspNetCore.Mvc;

namespace BootEshop.ViewComponents;

public class ProductsSelectionViewComponent : ViewComponent
{
    private readonly ProductService _service;

    public ProductsSelectionViewComponent(ProductService service)
    {
        _service = service;
    }
    
    public async Task<IViewComponentResult> InvokeAsync(ProductsSelectionArgs args)
    {
        //replace with fetch
        
        //this._service.GetEntity()

        var products = _service.GetProducts(args.FilterModel);
            

        return View(new ProductSelectionViewModel
        {
            Products = products,
            Header = args.Header,
            HeaderBold = args.HeaderBold,
        });

    }
    
}
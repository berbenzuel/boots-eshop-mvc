using BootEshop.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.ViewComponents;

public class ProductGridViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Product> products)
    {
       
        return View(products);

    }

}
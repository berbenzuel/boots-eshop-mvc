
using Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.ViewComponents;

public class ProductItemViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Product product)
    {
        return View(product);
    }
}
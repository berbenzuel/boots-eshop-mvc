using BootEshop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.ViewComponents;

public class ImageCardViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ImageCardViewModel model)
    {
        return View(model);
    }
}
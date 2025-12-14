using BootEshop.Models;
using BootEshop.Models.Entities;
using BootEshop.ViewArgs;
using BootEshop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BootEshop.ViewComponents;

public class ProductsSelectionViewComponent : ViewComponent
{
    private readonly EshopContext _context;

    public ProductsSelectionViewComponent(EshopContext context)
    {
        _context = context;
    }
    
    public async Task<IViewComponentResult> InvokeAsync(ProductsSelectionArgs args)
    {
        //replace with fetch
        var products = new List<Product>();
        products.Add(new Product()
        {
            Id = Guid.Parse("04908700-5d50-454e-bb0d-04faf2d37b6c"),
            Price = 100,
            SalePrice = 50,
            Name = "Test Product",
        });
        
        
        for (var i = 0; i < args.Count; i++)
        {
            if (i % 2 == 0)
            {
                products.Add(new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = $"Rock {i}",
                    Price = 500
                });    
            }
            else
            {
                products.Add(new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = $"Metal {i}",
                    SalePrice = 60,
                    Price = 10,

                });
            }
            
        }
            

        return View(new ProductSelectionViewModel
        {
            Products = products,
            Header = args.Header,
            HeaderBold = args.HeaderBold,
        });

    }
    
}
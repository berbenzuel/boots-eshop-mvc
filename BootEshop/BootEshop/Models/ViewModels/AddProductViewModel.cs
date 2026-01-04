using System.Drawing;
using BootEshop.ViewArgs;
using Database.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BootEshop.ViewModels;

public class AddProductViewModel
{
    public string Name { get; set; } = "boot";
    public string ShortDescription { get; set; } = "some short description";
    public string LongDescription { get; set; } = "some long description";

    public double StockPrice { get; set; }
    public double Price { get; set; }
    public double? SalePrice { get; set; }

    public Guid CategoryId { get; set; }
    public Guid ManufacturerId { get; set; }
    public IEnumerable<Guid> ProductColorIds { get; set; } = new List<Guid>();
    public IEnumerable<Guid> ProductSizeIds { get; set; } = new List<Guid>();
    
    public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();
    public IEnumerable<SelectListItem> Categories { get; set; }  = new List<SelectListItem>();
    public IEnumerable<SelectListItem> Manufacturers { get; set; }  = new List<SelectListItem>();
    public IEnumerable<ProductColor> Colors { get; set; }   = new List<ProductColor>();
    public IEnumerable<ProductSize> Sizes { get; set; }   = new List<ProductSize>();
    
}
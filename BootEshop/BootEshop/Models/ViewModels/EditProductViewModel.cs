using Database.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BootEshop.ViewModels;

public class EditProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; } 
    public string LongDescription { get; set; }

    public double StockPrice { get; set; }
    public double Price { get; set; }
    public double? SalePrice { get; set; }

    public Guid CategoryId { get; set; }
    public Guid ManufacturerId { get; set; }
    public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();
    public IEnumerable<string> ExistingImages { get; set; } = new List<string>();
    public IEnumerable<string> ImageOrder {get; set;} = new List<string>();
    public IEnumerable<SelectListItem> Categories { get; set; }  = new List<SelectListItem>();
    public IEnumerable<SelectListItem> Manufacturers { get; set; }  = new List<SelectListItem>();

    public List<SelectListItem> Colors { get; set; } = new();
    public List<SelectListItem> Sizes { get; set; } = new();
    
    public List<StockRowViewModel> Rows { get; set; } = new List<StockRowViewModel>();
    
}
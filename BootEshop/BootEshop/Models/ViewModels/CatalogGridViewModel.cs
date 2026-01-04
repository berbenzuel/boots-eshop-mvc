using BootEshop.ViewArgs;
using Database.Entities;

namespace BootEshop.ViewModels;

public class CatalogGridViewModel(
    ProductFilterModelDto filterModel,
    IEnumerable<Manufacturer> manufacturers,
    IEnumerable<ProductCategory> categories,
    IEnumerable<ProductSize> sizes,
    IEnumerable<ProductColor> colors,
    IEnumerable<Product> products
    )
{
    public ProductFilterModelDto FilterModel { get; set; } = filterModel;
    public IEnumerable<Manufacturer> Manufacturers { get; set; } = manufacturers;
    public IEnumerable<ProductCategory> Categories { get; set; } = categories;
    public IEnumerable<ProductSize> Sizes { get; set; } = sizes;
    public IEnumerable<ProductColor> Colors { get; set; } = colors;
    
    public IEnumerable<Product> Products { get; set; } = products;
}
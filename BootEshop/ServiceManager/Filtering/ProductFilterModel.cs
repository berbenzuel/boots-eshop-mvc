using Database.Entities;

namespace BootEshop.ViewArgs;

public class ProductFilterModel
{
    public ProductCategory ProductCategory { get; set; } = new();
    public List<ProductSize> Sizes { get; set; } = new();
    public List<ProductColor> Colors { get; set; } = new();
    public List<Manufacturer> Manufacturers { get; set; } = new();
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public ProductSort Sort { get; set; } = ProductSort.Recommended;

}
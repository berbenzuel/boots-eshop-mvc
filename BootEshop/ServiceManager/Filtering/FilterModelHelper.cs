namespace BootEshop.ViewArgs;

public static class FilterModelHelper
{
    public static ProductFilterModelDto Convert(this ProductFilterModel filter)
    {
        return new ProductFilterModelDto
        {
            CategoryId = filter.ProductCategory.Id,
            SizeIds = filter.Sizes.Select(s => s.Id).ToList(),
            ColorIds = filter.Colors.Select(s => s.Id).ToList(),
            ManufacturerIds = filter.Manufacturers.Select(s => s.Id).ToList(),
            MinPrice = filter.MinPrice,
            MaxPrice = filter.MaxPrice,
            SortEnum = (int)filter.Sort,
        };
    }
}
using System.ComponentModel.DataAnnotations;

namespace BootEshop.ViewArgs;

public class ProductFilterModelDto
{
    public Guid? CategoryId { get; set; }
    public List<Guid> SizeIds { get; set; } = new();
    public List<Guid> ColorIds { get; set; } = new();
    public List<Guid> ManufacturerIds { get; set; } = new();
    public double? MinPrice { get; set; } = null;
    public double? MaxPrice { get; set; } = null;
    public int SortEnum { get; set; } = (int)ProductSort.Recommended;
}
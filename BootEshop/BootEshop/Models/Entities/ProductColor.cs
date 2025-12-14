namespace BootEshop.Models.Entities;

public class ProductColor
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string HexColor { get; set; }
    
    public ICollection<Product> Products { get; set; }
}
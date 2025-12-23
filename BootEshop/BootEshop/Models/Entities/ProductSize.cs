namespace BootEshop.Models.Entities;

public class ProductSize
{
    public Guid Id { get; set; }
    public int Size { get; set; }
    
    public ICollection<Product> Products { get; set; }
}
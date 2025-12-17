namespace Database.Entities;

public class ProductCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    
    public ICollection<Product> Products { get; set; }
    
}
namespace Database.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public double StockPrice { get; set; }
    public double Price { get; set; }
    public double? SalePrice { get; set; }
    public DateTime Created { get; set; }
    
    public Guid CategoryId { get; set; }
    public Guid ProductColorId { get; set; }
    public Guid ProductSizeId { get; set; }
    public Guid ManufacturerId { get; set; }
    
    public ProductCategory ProductCategory { get; set; }
    public ProductColor ProductColor { get; set; }
    public ProductSize ProductSize { get; set; }
    public Manufacturer Manufacturer { get; set; }
    
    
}
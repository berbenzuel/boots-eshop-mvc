using Database.Entities;


namespace Database.Entities;
public class Stock
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    
    public Guid ProductId { get; set; }
    public Guid ProductColorId {get; set;}
    public Guid ProductSizeId {get; set;}
    
    public Product Product { get; set; }
    public ProductColor ProductColor { get; set; }
    public ProductSize ProductSize { get; set; }
    public ICollection<OrderStock> OrderStocks { get; set; }
}
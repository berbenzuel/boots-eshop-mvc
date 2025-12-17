using Database.Entities;


namespace Database.Entities;
public class Stock
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    
    public Guid ProductId { get; set; }
    public Guid BootColorId {get; set;}
    public Guid BootSizeId {get; set;}
    
    public Product Product { get; set; }
    public ProductColor ProductColor { get; set; }
    public ProductSize Size { get; set; }
    public ICollection<OrderStock> OrderStocks { get; set; }
}
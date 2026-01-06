namespace Database.Entities;

public class ProductSize
{
    public Guid Id { get; set; }
    public int Size { get; set; }
    
    public ICollection<Stock> Stocks { get; set; }
}
namespace BootEshop.Models.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid UserId { get; set; }
    
    public ICollection<Product> Products { get; set; }    
    public User User { get; set; }
}
using Database.Entities;

namespace BootEshop.Models.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid UserId { get; set; }
    
    public ICollection<OrderStock> OrderStocks { get; set; }    
    public User User { get; set; }
}
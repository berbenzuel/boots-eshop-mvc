using Database;
using Database.Entities;

namespace Database.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool Completed { get; set; }
    public double Price { get; set; }
    public Guid UserId { get; set; }
    
    public ICollection<OrderStock> OrderStocks { get; set; }    
    public User User { get; set; }
}
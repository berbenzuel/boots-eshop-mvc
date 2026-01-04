
using BootEshop.Models.Abstractions;
using BootEshop.ViewArgs;
using Database.Entities;
using Database;


namespace BootEshop.Models.Services;

public class OrderService(EshopContext context) : ContextService<Order>(context)
{
    public IEnumerable<Order> GetOrders(DateTime startDate, DateTime endDate)
    {
        var query = this._context.Set<Order>();
        query.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate);
        return query.ToList();
    }
}
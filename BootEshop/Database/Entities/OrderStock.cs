

namespace Database.Entities
{
    public class OrderStock
    {
        public Guid Id { get; set; }
        public Guid StockId { get; set; }
        public Guid OrderId { get; set; }
        public int Count { get; set; }


        public Stock Stock {get; set;}
        public Order Order {get; set;}
    }
}

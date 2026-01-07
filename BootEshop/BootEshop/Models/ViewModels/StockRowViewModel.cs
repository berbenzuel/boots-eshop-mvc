namespace BootEshop.ViewModels;

public class StockRowViewModel
{
    public Guid? Id { get; set; }   // null = new row

    public Guid ProductColorId { get; set; }
    public Guid ProductSizeId { get; set; }

    public int Quantity { get; set; }
    
}
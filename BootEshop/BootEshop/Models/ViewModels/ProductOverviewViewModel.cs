using Database.Entities;

namespace BootEshop.ViewModels;

public class ProductOverviewViewModel
{
    public Product Product { get; set; }
    public int Stock { get; set; }
}
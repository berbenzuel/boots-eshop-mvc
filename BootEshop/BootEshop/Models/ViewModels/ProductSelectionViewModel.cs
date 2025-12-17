using Database.Entities;

namespace BootEshop.ViewModels;

public class ProductSelectionViewModel
{
    public IEnumerable<Product> Products { get; set; }
    public string Header { get; set; }
    public string HeaderBold { get; set; }
}
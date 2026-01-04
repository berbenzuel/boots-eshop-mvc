namespace BootEshop.ViewArgs;

public record ProductsSelectionArgs(string header, string headerBold, ProductFilterModelDto FilterModel)
{
    public ProductFilterModelDto FilterModel { get; set; } = FilterModel;
    public string Header { get; set; } = header;
    public string HeaderBold { get; set; } = headerBold;
}
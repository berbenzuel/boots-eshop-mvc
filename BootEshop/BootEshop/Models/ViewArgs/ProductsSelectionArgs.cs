namespace BootEshop.ViewArgs;

public record ProductsSelectionArgs(string header, string headerBold, CatalogFilter filter)
{
    public CatalogFilter Filter { get; set; } = filter;
    public string Header { get; set; } = header;
    public string HeaderBold { get; set; } = headerBold;
}
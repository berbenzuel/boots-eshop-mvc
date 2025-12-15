namespace BootEshop.ViewArgs;

public record ProductsSelectionArgs(int count, string header, string headerBold, StockFilter filter)
{
    public StockFilter Filter { get; set; } = filter;
    public string Header { get; set; } = header;
    public string HeaderBold { get; set; } = headerBold;
}
namespace BootEshop.ViewArgs;

public record ProductsSelectionArgs(int count, string header, string headerBold)
{
    public int Count { get; set; } = count;
    public string Header { get; set; } = header;
    public string HeaderBold { get; set; } = headerBold;
}
namespace BootEshop.ViewModels;

public class ImageCardViewModel
{
    public int Index { get; set; }
    public string Url { get; set; }
    public IFormFile? File { get; set; }
}
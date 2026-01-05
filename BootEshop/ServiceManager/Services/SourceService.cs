using BootEshop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;



namespace DatabaseManager.Services;

//images have convention: foobarbaz_1.[typeof], foobarbaz_2 etc

public class SourceService(IOptions<AppConfig> config, IWebHostEnvironment env)
{
    private SourceConfig _config = config.Value.Source;
    private readonly IWebHostEnvironment _env = env;

    public string? GetProductMainImagePath(Guid productId)
    {
        var path = Path.Combine(_config.ProductImage, $"{productId}_1");
        return System.IO.Path.Exists(path) ?  path : null;
    }

    public IEnumerable<string>? GetProductImagePaths(Guid productId)
    {
        if (!Directory.Exists(_config.ProductImage))
            return null;
        
        return GetOrderedFiles(_config.ProductImage,
            productId);
        
    }

    public string? GetProductImagePath(string fileName)
    {
        var path = Path.Combine(_config.ProductImage, fileName);
        return System.IO.Path.Exists(path) ?  path : null;
    }
    public bool UploadProductImages(Guid productId, IEnumerable<IFormFile> files)
    {

        try
        {
            int count = 1;
            foreach (var image in files)
            {
                if (image.Length > 0)
                {
                    var filePath = Path.Combine(_config.ProductImage, $"{productId}_{count}");
                    
                    if (!System.IO.File.Exists(filePath))
                        File.Create(filePath).Close();
                    using var stream = new FileStream(filePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.None);
                    image.CopyTo(stream);
                    count++;
                }
            }    
        }
        catch (Exception ex)
        {
            //logging
            return false;
        }
         
        return true;
    }

    public IEnumerable<IFormFile> GetProductImages(Guid productId)
    {
        foreach (var path in GetProductImagePaths(productId))
        {
            using var stream = new FileStream(path, FileMode.Open);

            
            yield return new FormFile(stream, 0, stream.Length, null, Path.GetFileName(path))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/webp"
            };
        }
    }
    
    
    public string? GetCarouselImagePath(int id)
    {
        return Directory.EnumerateFiles(_config.CarrouselImage, $"*").FirstOrDefault();
    } 
    

    private IEnumerable<string> GetOrderedFiles(string folderPath, Guid id)
    {
        return Directory.EnumerateFiles(folderPath,
                $"{id}_*",
            SearchOption.AllDirectories).AsEnumerable()
            .OrderBy(f => f);

    }
    
    
}
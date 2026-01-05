using System.Text;
using BootEshop.Services;
using BootEshop.ViewModels;
using Database;
using Database.Entities;
using DatabaseManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace BootEshop.Controllers;

public class AdminController : Controller
{
    private readonly ProductService _productService;
    private readonly ColorService _colorService;
    private readonly ManufacturerService _manufacturerService;
    private readonly SizeService _sizeService;
    private readonly CategoryService _categoryService;
    private readonly SourceService _sourceService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;


    public AdminController(ProductService productService,  ColorService colorService, ManufacturerService manufacturerService, SizeService sizeService, CategoryService categoryService, SourceService sourceService, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager )
    {
        _productService = productService;
        _colorService = colorService;
        _manufacturerService = manufacturerService;
        _sizeService = sizeService;
        _categoryService = categoryService;
        _sourceService = sourceService;
        _userManager = userManager;
        _roleManager = roleManager;
        
    }
    
    // GET
    public IActionResult Index()
    {
        return View(new AdminViewModel()
        {
            OrderCountToday = 10,
            OrderTotal = 100
        });
    }

    public IActionResult ProductOverview()
    {
        var products = _productService.GetEntities();
        return View(products.AsEnumerable());
    }

    public IActionResult DeleteProduct(Guid productId)
    {
        var product = _productService.GetEntity(productId);
        _productService.DeleteEntity(product);
        
        TempData["message"] = $"Product {product.Name} has been deleted";
        return RedirectToAction(nameof(ProductOverview));
    }
    public IActionResult AddProduct()
    {
        var model = new AddProductViewModel
        {
            Categories = _categoryService.GetEntities()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }),

            Manufacturers = _manufacturerService.GetEntities()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }),

            Colors = _colorService.GetEntities(),

            Sizes = _sizeService.GetEntities()
        };

        return View(model);
    }

    [OutputCache(NoStore = true, Duration = 0)]
    public IActionResult EditProduct(Guid productId)
    {
        var product = _productService.GetEntities()
            .Include(x => x.ProductColors)
            .Include(x => x.ProductSizes)
            .First(p => p.Id == productId);
        
 
        var model = new EditProductViewModel()
        {
            Id = product.Id,
            Name = product.Name,
            ShortDescription = product.ShortDescription,
            LongDescription = product.LongDescription,
            StockPrice = product.StockPrice,
            Price = product.Price,
            SalePrice = product.SalePrice,
            CategoryId = product.ProductCategoryId,
            ManufacturerId = product.ManufacturerId,
            ProductColorIds = product.ProductColors.Select(c => c.Id).ToList(),
            ProductSizeIds = product.ProductSizes.Select(c => c.Id).ToList(),
            ExistingImages = _sourceService.GetProductImagePaths(product.Id)
                .Select(f => Path.GetFileName(f))
                .ToList(),
            
            Categories = _categoryService.GetEntities()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }),

            Manufacturers = _manufacturerService.GetEntities()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }),

            Colors = _colorService.GetEntities(),

            Sizes = _sizeService.GetEntities()
        };

        return View(model);
    }
    
    
    [HttpPost]
    public IActionResult EditProduct(EditProductViewModel model)
    {
        var product = _productService.GetEntities()
            .Include(p => p.ProductColors)
            .Include(p => p.ProductSizes)
            .Include(p => p.Manufacturer)
            .Include(p => p.ProductCategory)
            .First(p => p.Id == model.Id);





        product.Name = model.Name;
        product.ShortDescription = model.ShortDescription;
        product.LongDescription = model.LongDescription;
        product.StockPrice = model.StockPrice;
        product.Price = model.Price;
        product.SalePrice = model.SalePrice;
        product.Created = DateTime.UtcNow;

        product.ProductCategory = _categoryService.GetEntity(model.CategoryId);
        product.Manufacturer = _manufacturerService.GetEntity(model.ManufacturerId);
        product.ProductSizes = _sizeService.GetEntities().Where(s => model.ProductSizeIds.Contains(s.Id)).ToList();
        product.ProductColors = _colorService.GetEntities().Where(s => model.ProductColorIds.Contains(s.Id)).ToList();
    
        
        _productService.UpdateEntity(product);
        var files = new List<IFormFile>();
        foreach (var item in model.ImageOrder)
        {
            if (item.StartsWith("NEW:"))
            {
                var index = int.Parse(item.Replace("NEW:", ""));
                var file = model.Images.ElementAt(index);

                files.Add(file);
            }
            else
            {
                var fileStream = new FileStream(_sourceService.GetProductImagePath(item), FileMode.Open, FileAccess.Read);
                var formFile = new FormFile(fileStream, 0, fileStream.Length, "file", Path.GetFileName(item))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/webp"
                };
                files.Add(formFile);
            }
        }
        _sourceService.UploadProductImages(product.Id, files);

        return RedirectToAction("ProductOverview");
        return RedirectToAction("ProductOverview");
        _sourceService.UploadProductImages(product.Id, model.Images);

        TempData["SuccessMessage"] = "Maminka je na tebe pyšná ❤️";
        return RedirectToAction(nameof(ProductOverview));
    }
    

    [HttpPost]
    public IActionResult AddProduct(AddProductViewModel model)
    {
        // if (!ModelState.IsValid)
        // {
        //     TempData["ErrorMessage"] = "Maminka je zklamaná 💔";
        //     return View(model);
        // }

        //var errormessage = new StringBuilder(); check? 
        
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            ShortDescription = model.ShortDescription,
            LongDescription = model.LongDescription,
            StockPrice = model.StockPrice,
            Price = model.Price,
            SalePrice = model.SalePrice,
            Created = DateTime.UtcNow,

            ProductCategoryId = model.CategoryId,
            ManufacturerId = model.ManufacturerId,
            ProductSizes = _sizeService.GetEntities().Where(s => model.ProductSizeIds.Contains(s.Id)).ToArray(),
            ProductColors = _colorService.GetEntities().Where(s => model.ProductColorIds.Contains(s.Id)).ToArray(),
        };

        _productService.AddEntity(product);
        
        //image saving
        _sourceService.UploadProductImages(product.Id, model.Images);
        
        TempData["SuccessMessage"] = "Maminka je na tebe pyšná ❤️";

        
        return RedirectToAction(nameof(ProductOverview));
    }
    
    [HttpGet]
    public IActionResult CreateUser()
    {
        ViewData["Roles"] = _roleManager.Roles; // For a dropdown
        return View();
    }

    [HttpPost]
  
    public async Task<IActionResult> CreateUser(CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Roles"] = _roleManager.Roles;
            return View(model);
        }

        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            ViewData["Roles"] = _roleManager.Roles;
            return View(model);
        }

        if (!string.IsNullOrEmpty(model.Role))
        {
            if (!await _roleManager.RoleExistsAsync(model.Role))
                await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = model.Role });

            await _userManager.AddToRoleAsync(user, model.Role);
        }

        return RedirectToAction("UserList"); // Or wherever you want
    }


    
}
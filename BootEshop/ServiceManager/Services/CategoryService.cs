using BootEshop.Models.Abstractions;
using BootEshop.ViewArgs;
using Database;
using Database.Entities;

namespace DatabaseManager.Services;

public class CategoryService(EshopContext dbContext) : ContextService<ProductCategory>(dbContext)
{
    public ProductCategory GetCategory(ProductSelection selection)
    {
        var compare = selection switch
        {
            ProductSelection.ManProduct => "man",
            ProductSelection.WomanProduct => "woman",
            ProductSelection.OtherProduct => "other",
            ProductSelection.AccessoriesProduct => "accessories",
        };
        
        return GetEntities().First(c => c.Name.ToLower() == compare);
    }
}
using BootEshop.Models.Abstractions;

using BootEshop.ViewArgs;
using Database;
using Database.Entities;


namespace BootEshop.Services;

public class ProductService(EshopContext dbContext) : ContextService<Product>(dbContext)
{
 public IEnumerable<Product> GetProducts(CatalogFilter filter)
     {
         var query = this._context.Set<Product>();
         
         switch (filter.Category)
         {
             case Category.NewProduct:
                 query.OrderByDescending(p => p.Created) 
                     .Take(filter.Count)
                     .ToList();
                 break;
             case Category.RecommendedProduct:
                 query.Take(filter.Count).ToList();
                 break;
             case Category.BoyProduct:
                 query.Where(p => p.ProductCategory.Name == "boy");
                 break;
             case Category.GirlProduct:
                 query.Where(p => p.ProductCategory.Name == "girl");
                 break;
         }
         return query;
     }
}
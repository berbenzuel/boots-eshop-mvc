using BootEshop.Models.Abstractions;

using BootEshop.ViewArgs;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;


namespace BootEshop.Services;

public class ProductService(EshopContext dbContext) : ContextService<Product>(dbContext)
{
     
     public IQueryable<Product> GetProducts(ProductFilterModelDto filterModel)
     {

         var query = GetEntities();
             
         if (filterModel.CategoryId is not null)
             query = query.Where(p => p.ProductCategoryId == filterModel.CategoryId);

         if (filterModel.SizeIds.Any())
             query = query
                 .Where(p => p.ProductSizes.Any(ps => filterModel.SizeIds.Contains(ps.Id)));
        if(filterModel.ManufacturerIds.Any())
            query = query
             .Where(p => filterModel.ManufacturerIds.Contains(p.ManufacturerId));
        if (filterModel.ColorIds.Any())
            query = query
                .Where(p => p.ProductColors.Any(ps => filterModel.ColorIds.Contains(ps.Id)));
        if(filterModel.MaxPrice is double maxPrice)
            query = query
             .Where(p => p.Price <= maxPrice || p.SalePrice >= maxPrice);
        if(filterModel.MinPrice is double minPrice)
            query = query
             .Where(p => p.Price >= minPrice || p.SalePrice >= minPrice);

         query = ((ProductSort)filterModel.SortEnum) switch
         {
             ProductSort.Recommended => query, //fix with some statistic ?
             ProductSort.Alphabetic => query.OrderBy(p => p.Name),
             ProductSort.AlphabeticDesc => query.OrderByDescending(p => p.Name),
             ProductSort.PriceAsc => query.OrderBy(p => p.Price),
             ProductSort.PriceDesc => query.OrderByDescending(p => p.Price),
             ProductSort.Newest => query.OrderBy(p => p.Created),
             ProductSort.NewestDesc => query.OrderByDescending(p => p.Created),
         };
         
         return query;
     }
     
     
}
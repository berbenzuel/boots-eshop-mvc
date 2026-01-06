using BootEshop.Models.Abstractions;
using BootEshop.ViewArgs;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseManager.Services;

public class StockService(EshopContext dbcontext) : ContextService<Stock>(dbcontext)
{
    private readonly EshopContext _context = dbcontext;
    public IQueryable<Stock> GetStock(Guid productId)
    {
        var query = _context.Stock.AsQueryable();
        return query.Where(p => p.ProductId == productId);
    }

    public IQueryable<Stock> GetStock(ProductFilterModelDto filterModel)
    {
        var query = _context.Stock
            .Include(s => s.Product)
            .Include(s => s.ProductColor)
            .Include(s => s.ProductSize)
            .AsQueryable();
        
        if (filterModel.CategoryId is not null)
            query = query
                .Where(s => s.Product.ProductCategoryId == filterModel.CategoryId);

        if (filterModel.SizeIds.Any())
            query = query
                .Where(p => filterModel.SizeIds.Contains(p.ProductSizeId));
        if(filterModel.ManufacturerIds.Any())
            query = query
                .Where(p => filterModel.ManufacturerIds.Contains(p.Product.ManufacturerId));
        if (filterModel.ColorIds.Any())
            query = query
                .Where(p => filterModel.ColorIds.Contains(p.ProductColorId));
        if(filterModel.MaxPrice is double maxPrice)
            query = query
                .Where(p => p.Product.Price <= maxPrice || p.Product.SalePrice >= maxPrice);
        if(filterModel.MinPrice is double minPrice)
            query = query
                .Where(p => p.Product.Price >= minPrice || p.Product.SalePrice >= minPrice);

        // query = ((ProductSort)filterModel.SortEnum) switch
        // {
        //     ProductSort.Recommended => query, //fix with some statistic ?
        //     ProductSort.Alphabetic => query.OrderBy(p => p.Name),
        //     ProductSort.AlphabeticDesc => query.OrderByDescending(p => p.Name),
        //     ProductSort.PriceAsc => query.OrderBy(p => p.Price),
        //     ProductSort.PriceDesc => query.OrderByDescending(p => p.Price),
        //     ProductSort.Newest => query.OrderBy(p => p.Created),
        //     ProductSort.NewestDesc => query.OrderByDescending(p => p.Created),
        // };
        //
        return query;
    }
}
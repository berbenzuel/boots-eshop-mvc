using BootEshop.Models.Abstractions;
using Database;
using Database.Entities;

namespace DatabaseManager.Services;

public class SizeService(EshopContext dbContext) : ContextService<ProductSize>(dbContext)
{
    
}
using BootEshop.Models.Abstractions;
using Database;
using Database.Entities;

namespace DatabaseManager.Services;

public class ColorService(EshopContext dbContext) : ContextService<ProductColor>(dbContext)
{
    
}
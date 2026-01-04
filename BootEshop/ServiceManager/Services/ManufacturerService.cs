using BootEshop.Models.Abstractions;
using Database;
using Database.Entities;

namespace DatabaseManager.Services;

public class ManufacturerService(EshopContext dbContext) : ContextService<Manufacturer>(dbContext)
{
    
}
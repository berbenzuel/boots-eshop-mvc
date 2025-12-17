using Database.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class EshopContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Product> Product { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Manufacturer> Manufacturer { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<ProductColor> ProductColor { get; set; }
    public DbSet<ProductSize> ProductSize { get; set; }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<OrderStock> OrderStock { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //product
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Product>()
            .HasOne<ProductColor>(p => p.ProductColor)
            .WithMany(pc => pc.Products);
        modelBuilder.Entity<Product>()
            .HasOne<ProductSize>(p => p.ProductSize)
            .WithMany(ps => ps.Products);
        modelBuilder.Entity<Product>()
            .HasOne<ProductCategory>(p => p.ProductCategory)
            .WithMany(pc => pc.Products);
        modelBuilder.Entity<Product>()
            .HasOne<Manufacturer>(p => p.Manufacturer)
            .WithMany(m => m.Products);
        
        
        //order
        modelBuilder.Entity<Order>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<OrderStock>()
            .HasOne<Stock>(s => s.Stock)
            .WithMany(s => s.OrderStocks);
        modelBuilder.Entity<OrderStock>()
            .HasOne<Order>(o => o.Order)
            .WithMany(o => o.OrderStocks);
        
    }
    
    public EshopContext(DbContextOptions<EshopContext> options): base(options)
    {
         this.Database.EnsureDeleted();
        this.Database.EnsureCreated();
        //this.Database.Migrate(); <-- i need to fix migrations


    }
}
using BootEshop.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BootEshop.Models;

public class EshopContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }


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
        
        modelBuilder.Entity<Order>()
            .HasMany<Product>().WithMany();
        
    }
    
    public EshopContext(DbContextOptions<EshopContext> options): base(options)
    {
        // this.Database.EnsureDeleted();
        // this.Database.EnsureCreated();
        //this.Database.Migrate(); <-- i need to fix migrations
    }
}
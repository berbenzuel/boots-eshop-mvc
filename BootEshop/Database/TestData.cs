using Database.Entities;

namespace Database;

public static class TestData
{
    public static void InsertTestData(this EshopContext db)
    {
        db.ProductCategory.Add(new ProductCategory()
        {
            Name = "man"
        });
        db.ProductCategory.Add(new ProductCategory()
        {
            Name = "woman"
        });
        db.ProductColor.Add(new ProductColor()
        {
            Name = "blue",
            HexColor = "#0000ff"
        });
        db.ProductColor.Add(new ProductColor()
        {
            Name = "red",
            HexColor = "#ff0000"
        });
        db.ProductColor.Add(new ProductColor()
        {
            Name = "green",
            HexColor = "#00ff00"
        });

        db.ProductSize.Add(new ProductSize()
        {
            Size = 40
        });
        db.ProductSize.Add(new ProductSize()
        {
            Size = 42
        });
        db.ProductSize.Add(new ProductSize()
        {
            Size = 67
        });
        db.Manufacturer.Add(new Manufacturer()
        {
            Name = "bauhaus"
        });
        db.Manufacturer.Add(new Manufacturer()
        {
            Name = "foobarbaz"
        });
        db.Manufacturer.Add(new Manufacturer()
        {
            Name = "unga bunga"
        });

        db.SaveChanges();
        
        db.Product.Add(new Product()
        {
            Created = DateTime.Now,
            ProductCategory = db.ProductCategory.First(),
            Manufacturer = db.Manufacturer.First(),
            
            Name = "botickyyy",
            Price = 100,
            StockPrice = 30,
            SalePrice = 60,
            LongDescription = "foobar baz unga bunga",
            ShortDescription = "foobarbaz",
            
            
        });
        db.Product.Add(new Product()
        {
            Created = DateTime.Now,
            ProductCategory = db.ProductCategory.Order().Last(),
            Manufacturer = db.Manufacturer.Order().Last(),
            Name = "bomboklad",
            Price = 100,
            StockPrice = 30,
            SalePrice = 60,
            
            LongDescription = "foobar baz unga bunga",
            ShortDescription = "foobarbaz",
        });
        db.SaveChanges();

        db.Stock.Add(new Stock()
        {
            Product = db.Product.First(),
            ProductColor = db.ProductColor.First(),
            ProductSize = db.ProductSize.First(),
        });

        db.SaveChanges();
    }
}
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
ETicaretDbContext dbContext = new();
//Tablo içerisindeki kolonları entity classların içerisindeki propertylerler değil fieldlar ile eşleştirmemizi sağlar. Ekstradan sürece müdehale etmemizi sağlar. Esneklik olarak default davranıştan daha iyidir.

var products = await dbContext.Products.ToListAsync();

Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=ECommerceDb;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading


    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(p => p.ProductName).HasField("name").UsePropertyAccessMode(PropertyAccessMode.Field);
        });
        //Field : Veri erişim sürecinde sadece field kullanılır.
        //FieldDuringConstruction : Nesne oluşturulma sürecinde field kullanılır. Sonrasında property kullanılır.
        //Property : Veri erişim sürecinde sadece property kullanılır.

        //Field-Only Properties kullanımı
        modelBuilder.Entity<Product>().Property(nameof(Product.ProductName));

    }
}

#region Backing Fields
//Default olarak kullanım
//public class Product
//{
//    public int Id { get; set; }
//    public string name;
//    public string ProductName { get => name.Substring(0, 3); set => name = value; }
//    public string Description { get; set; }

//}
#endregion
#region Backing Fields Attribute
//Backing Field Attribute ile kullanım
//public class Product
//{
//    public int Id { get; set; }
//    public string name;
//    [BackingField(nameof(name))]
//    public string ProductName { get; set; }
//    public string Description { get; set; }

//}

#endregion
#region Backing Fields Fluent API
//public class Product
//{
//    public int Id { get; set; }
//    public string ProductName { get; set; }
//    public string Description { get; set; }

//}
#endregion
#region Field-Only Properties
//Propertiler yerine methodların kullanıldığı veya belirli alanların hiç gösterilmediği durumlarda kullanılabilir.
public class Product
{
    public int Id { get; set; }
    public string name;
    public string ProductName { get; set; }
    public string Description { get; set; }

    public string GetProductName() =>
        ProductName;
    public string SetProductName(string name) =>
        this.name = name;

}
#endregion

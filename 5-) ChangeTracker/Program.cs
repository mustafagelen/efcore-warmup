using Microsoft.EntityFrameworkCore;
ETicaretDbContext dbContext = new();
//Change Tracker. Context nesnesi üzerinden gelen tüm veriler otomatik olarak bir takip mekanizması tarafından izlenir. Değişiklikler veya işlemler takip edilerek bu işlemlerin fıtratına uygun sql sorguları generate edilir.

//Change Tracker Propertysi takip edilen nesnelere erişebilmemizi sağlar. Nesnelerin durumlarını görebiliriz.
var products = await dbContext.Products.ToListAsync();
var datas = dbContext.ChangeTracker.Entries();

#region DetectChanges
//DetectChanges metodu context nesnesi tarafından takip edilen nesnelerdeki değişiklikleri algılar. Normalde bu metodun manuel olarak çağrılmasına gerek yoktur. SaveChanges metodu çağrıldığında otomatik olarak çağrılır.(SnapChant)
var product = await dbContext.Products.FirstAsync();
product.ProductName = "Updated Name";
dbContext.ChangeTracker.DetectChanges();
#endregion
#region Entries Methodu
//Entries metodu context nesnesi tarafından takip edilen tüm nesnelere erişmemizi sağlar.
var entries = dbContext.ChangeTracker.Entries();
foreach (var entry in entries)
{
    Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
}
#endregion
#region AcceptAllChanges
//AcceptAllChanges metodu context nesnesi tarafından takip edilen tüm nesnelerin durumlarını Unchanged olarak işaretler. Normalde bu metodun manuel olarak çağrılmasına gerek yoktur. SaveChanges metodu çağrıldığında otomatik olarak çağrılır.
var newProduct = new Product { ProductName = "New Product", Description = "New Description" };
dbContext.Products.Add(newProduct);
dbContext.SaveChanges(); // Burada AcceptAllChanges otomatik olarak çağrılır.
#endregion
#region Interceptor Kullanımı
dbContext.SaveChanges();

#endregion


Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Piece> Pieces { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=ECommerceDb;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken=default)
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if(entry.State == EntityState.Added)
            {
                Console.WriteLine($"Eklenecek Entity: {entry.Entity.GetType().Name}");
            }
            else if(entry.State == EntityState.Modified)
            {
                Console.WriteLine($"Güncellenecek Entity: {entry.Entity.GetType().Name}");
            }
            else if(entry.State == EntityState.Deleted)
            {
                Console.WriteLine($"Silinecek Entity: {entry.Entity.GetType().Name}");
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

}
public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }



    public ICollection<Piece> Pieces { get; set; }
}

public class Piece
{
    public int Id { get; set; }
    public string PieceName { get; set; }
}
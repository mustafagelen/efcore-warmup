using Microsoft.EntityFrameworkCore;
Console.WriteLine("");

ETicaretDbContext context = new();
Product product = new Product() { ProductName = "Laptop", Price = 15000 };

//Product tablosuna yeni bir kayıt ekliyoruz.
await context.Products.AddAsync(product);

//SaveChanges; inserti update ve delete sorgularını oluşturup bir transaction eşliğinde veritabanına gönderip execute eden fonksiyondur.
await context.SaveChangesAsync();

public class ETicaretDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=ECommerceDb;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading 
        //Caching
    }

}

//Her Entity primary key'e sahip olmalıdır.
public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
}
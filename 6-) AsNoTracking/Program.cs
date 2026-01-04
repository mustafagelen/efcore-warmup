using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
ETicaretDbContext dbContext = new();

#region AsNoTracking
//AsNoTracking metodu context nesnesi tarafından takip edilmeyecek sorgular oluşturur. Performans optimizasyonu sağlar. Sadece okuma işlemlerinde kullanılmalıdır. Değişiklik yapılacak verilerde kullanılmamalıdır.
//Change Tracker sayesinde yinelenen datalar aynı instanceleri kullanır.
var products = await dbContext.Products.AsNoTracking().ToListAsync();

#endregion
#region AsNoTrackingWithIdentityResolution
// AsNoTrackingWithIdentityResolution metodu context nesnesi tarafından takip edilmeyecek sorgular oluşturur. Ancak aynı nesneye referans veren veriler için tek bir nesne örneği kullanılır. Bu sayede bellek kullanımı optimize edilir ve performans artışı sağlanır. Özellikle büyük veri setlerinde ve ilişkili verilerde faydalıdır. Mesela roller için kullanıcının rolleri tekrar ve tekrar oluşrulmaz.

//Change Tracker sayesinde yinelenen datalar aynı instanceleri kullanır. Ancak bu mekanizmayı koparırsak bu methodu kullanmalıyız. Aynı intancelerin kullanılması için.
var productsWithIdentityResolution = await dbContext.Products.AsNoTrackingWithIdentityResolution().ToListAsync();
#endregion
#region AsTracking
//AsTracking metodu context nesnesi tarafından takip edilecek sorgular oluşturur. Default olarak gelen davranıştır. Değişiklik yapılacak verilerde kullanılmalıdır.
var trackedProducts = await dbContext.Products.AsTracking().ToListAsync();
#endregion
#region UseQueryTrackingBehavior
//UseQueryTrackingBehavior metodu context nesnesi için varsayılan takip davranışını belirler. Bu metod ile context nesnesi oluşturulduğunda tüm sorgular için takip davranışı belirlenebilir. Örneğin, tüm sorguların takip edilmemesi isteniyorsa UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) olarak ayarlanabilir.
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

        //Default olarak tüm sorguların takip edilmemesi isteniyorsa
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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
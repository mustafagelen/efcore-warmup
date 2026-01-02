using Microsoft.EntityFrameworkCore;
ETicaretDbContext dbContext = new();
//Change Tracker. Context nesnesi üzerinden gelen tüm veriler otomatik olarak bir takip mekanizması tarafından izlenir. Değişiklikler veya işlemler takip edilerek bu işlemlerin fıtratına uygun sql sorguları generate edilir.

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
using Microsoft.EntityFrameworkCore;
ETicaretDbContext dbContext = new();

#region Generated Values
// Generated Values, EF Core tarafından otomatik olarak oluşturulan değerlerdir. Bu değerler
// genellikle birincil anahtarlar (primary keys) veya zaman damgaları (timestamps) gibi alanlar için kullanılır.

#region Default Values
//Herhangi bir tablonun herhangi bir colonuna varsayılan bir değer atamak istiyorsak eğer Default Values kullanabiliriz.
#region HasDefaultValue
//Static veri
#endregion
#region HasDefaultValueSql
//Sql tabanlı dinamik veri
#endregion
#endregion
#endregion


Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=Config;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading

    }


}

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Post> Posts { get; set; }

}

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }

    public Blog Blog { get; set; }
}

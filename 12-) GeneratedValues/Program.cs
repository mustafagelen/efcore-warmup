using Microsoft.EntityFrameworkCore;
ETicaretDbContext dbContext = new();

#region Generated Values
// Generated Values, EF Core tarafından otomatik olarak oluşturulan değerlerdir. Bu değerler
// genellikle birincil anahtarlar (primary keys) veya zaman damgaları (timestamps) gibi alanlar için kullanılır.
#endregion
#region Default Values
//Herhangi bir tablonun herhangi bir colonuna varsayılan bir değer atamak istiyorsak eğer Default Values kullanabiliriz.
#region HasDefaultValue
//Static veri
#endregion
#region HasDefaultValueSql
//Sql tabanlı dinamik veri
#endregion
#endregion
#region Computed Columns
//Computed Columns, bir tablodaki bir sütunun (column) değerinin, diğer sütunların (columns) değerlerine bağlı olarak otomatik
//olarak hesaplandığı durumlardır. Bu sütunlar, veritabanı tarafından dinamik olarak hesaplanır ve genellikle sorgulama sırasında 
//kullanılırlar.
#region HasComputedColumnSql
//Sql tabanlı dinamik veri
dbContext.Database.EnsureCreated();
var blog = new Blog()
{
    Title = "Title 1"
};
dbContext.Blogs.Add(blog);
dbContext.SaveChanges();
#endregion
#endregion
#region Value Generation Strategies
//Primary Key her bir satırı kimlik olarak tanımlayan sütun veya sütunlar kümesidir.
//Identity yalnızca otomatik artan tamsayı türündeki birincil anahtarlar (primary keys) için kullanılır.
//Bu ikisi default olarak birlikte kullanılır. Ancak bu davranışı değiştirmek mümkündür.
#endregion

Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=GeneratedValues;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading

    }
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd(); // Generated Values
            entity.Property(e => e.Title)
            .HasDefaultValue("Varsayılan Başlık"); // Default Values
            // Computed Columns
            entity.Property<string>("TitleUpper")
            .HasComputedColumnSql("UPPER(Title)");
        });
        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd(); // Generated Values
        });
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

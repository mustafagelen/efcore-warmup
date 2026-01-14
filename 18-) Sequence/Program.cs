using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
ETicaretDbContext dbContext = new();

#region Sequence Nedir?
//Veritabanında benzersiz ve ardışık sayılar üreten bir veritabanı nesnesidir. Herhangi bir tablonun özelliği değildir. Veritabanı nesnesidir. Birden fazla tablo için kullanılabilir.
//Hangi veritabanı provider üzerinden çalışıyorsanız ona göre bir configuration yapmalısınız.
#endregion

#region Sequence ile Identiy Farkı
//Sequence veritabanına özel bir nesne iken identity tabloya özeldir. Sequence daha hızlı ve performanslıdır.
#endregion



Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=Sequence;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading

    }
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Sequence Üretimi
        modelBuilder.HasSequence("BP_Sequence").StartsAt(100).IncrementsBy(4);
        modelBuilder.Entity<Blog>().Property(e => e.Id).HasDefaultValueSql("NEXT VALUE FOR BP_Sequence");
        modelBuilder.Entity<Post>().Property(e => e.Id).HasDefaultValueSql("NEXT VALUE FOR BP_Sequence");

      

    }


}


public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public int A { get; set; }
    public int B { get; set; }
    public ICollection<Post> Posts { get; set; }

}

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}

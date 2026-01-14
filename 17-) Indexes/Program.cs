using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
ETicaretDbContext dbContext = new();

#region Index Nedir?
//Index bir sütuna dayalı sorgulamaları daha verimli ver performanslı hale getirmek için kullanılan yapıdır.
#endregion
#region Index Nasıl Yapılır? 
//PK, FK, AK olan kolonlar default olarak indexlenir.
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
        modelBuilder.Entity<Blog>().HasIndex(b => b.Url);

        modelBuilder.Entity<Blog>().ToTable(tb => tb.HasCheckConstraint("a_b_compare", "[A] > [B]"));

        //Composit Index
        modelBuilder.Entity<Post>().HasIndex(nameof(Blog.Url), nameof(Blog.Posts));
        //Index Name
        modelBuilder.Entity<Post>().HasIndex(x => x.Content).HasDatabaseName("index_name");
        //Sıralama
        modelBuilder.Entity<Post>().HasIndex(x => new { x.Content, x.Blog }).IsDescending(false, false);
        //Filtre Ekleme
        modelBuilder.Entity<Post>().HasIndex(x => x.Content).HasFilter("[NAEM] IS NOT NULL");
        //Include 
        modelBuilder.Entity<Post>().HasIndex(x => x.Content).IncludeProperties(x => x.BlogId);

    }


}

//Birden fazla index
[Index(nameof(Blog.Url), IsUnique = true)]
[Index(nameof(Blog.Title), IsUnique = true)]

//Composit Index
[Index(nameof(Blog.Url), nameof(Blog.Posts))]
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
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Content { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PostId { get; set; }

    public int BlogId { get; set; }

    public Blog Blog { get; set; }
}

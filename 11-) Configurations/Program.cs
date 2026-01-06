using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
ETicaretDbContext dbContext = new();
Console.WriteLine(nameof(Blog));


#region OnModelCreating
//İlk akla gelen metot OnModelCreating metodudur. Bir modelin yaratılışı ile ilgili tüm configleri burada gerçekleştiriyoruz.

#region GetEntityTypes
//Kullanılan entityleri elde etmek pragmatik olarak öğrenmek istiyorsak eğer GetEntityTypes kullanabiliriz.
#endregion
#endregion
#region Configurations => Data Annotaitons || Fluent API
#region Table - ToTable
//Generate edilecek tablonun ismini belirttiğimiz 
#endregion
#region Column - HasColumnName, HasColumnType, HasColumnOrder
//Generate edilecek kolonların ismini belirttiğimiz configuration burasıdır.
#endregion
#region NotMapped | | Ignore
//Bir property'nin tabloya yansıtılmaması için kullanılır. Bazen böyle bir property tanımlamak zorunda kalabiliriz. Mesela hesaplama yapılan property'ler gibi. İşte bu tip property'lerin tabloya yansıtılmaması için NotMapped attribute'u veya Ignore metodu kullanılır.
#endregion
#region Key - HasKey
//Bir entity'nin primary key'ini belirlemek için kullanılır. İsimledirme dinamiği sağlamak için işimizi görür.
#endregion
#region TimeStamp - IsRowVersion
//Versiyon kontrolü için kullanılır. Genellikle concurrency işlemlerinde kullanılır.
//Veriye özel versiyon mantığı sağlar.
#endregion


#endregion



Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=Shadow;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //var entities = modelBuilder.Model.GetEntityTypes();
        #region ToTable
        //modelBuilder.Entity<Blog>().ToTable("Blog");
        #endregion
        #region Column
        //modelBuilder.Entity<Blog>().Property(p=>p.Title).HasColumnName("Title").HasColumnType("test");
        #endregion
        #region ForeignKey
        modelBuilder.Entity<Blog>().HasMany(b => b.Posts).WithOne(p => p.Blog).HasForeignKey(p => p.BlogId);
        #endregion
        #region Ignore
        modelBuilder.Entity<Blog>().Ignore(b => b.NotMappedProperty);
        #endregion
        #region HasKey
        modelBuilder.Entity<Blog>().HasKey(b => b.KeyKolonu);
        #endregion

        #region Ignore
        modelBuilder.Entity<Blog>().Ignore(b => b.NotMappedProperty);
        #endregion

        #region IsRowVersion
        modelBuilder.Entity<Blog>().Property(b => b.RowVersion).IsRowVersion();
        #endregion

        #region IsRowVersion
        modelBuilder.Entity<Blog>().Property(b => b.RowVersion).IsRowVersion();
        #endregion

    }

}

[Table("Test")]
public class Blog
{
    [Key]
    public int KeyKolonu { get; set; }
    public string Title { get; set; }
    public ICollection<Post> Posts { get; set; }
    [NotMapped]
    public string NotMappedProperty { get; set; }
    [Timestamp]
    public  byte[] RowVersion { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }

    public  int BlogId { get; set; }

    public Blog Blog { get; set; }
}

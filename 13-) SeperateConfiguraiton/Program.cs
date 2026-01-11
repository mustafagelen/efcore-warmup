using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
ETicaretDbContext dbContext = new();

#region OnModelCreating
//Genel anlamda veritabanı ile ilgili konfigürasyonel opersaylaron dışında Entityler üzerinde konfigürasyonel çalışmalar yapmamızı sağlayan bir fonksiyondur.
#endregion

#region IEntityTypeConfiguration<T>
//Entity bazlı konfigürasyonel işlemlerimizi ayrı sınıflar içerisinde yapmamızı sağlayan arayüzdür. Harici bir dosyada konfigürasyonlarımızın yürütülmesi, merkezi bir nokta oluşturmamızı sağlar.
//Harici bir dosyada konfiglerin yürütülmesi entity sayısının fazla olduğu senaryolarda yönetilebilirliği arrıtacak ve yapılandırma ile ilgili geliştiricinin yükünü azaltacaktır.
#endregion

#region ApplyConfiguration Methodu
//ApplyConfiguration ilgili config için kullanılabilir.
#endregion

#region ApplyConfigurationsFromAssembly Methodu
//ApplyConfigurationsFromAssembly ise birden fazla config dosyasının olduğu senaryolarda kullanılabilir. Tek tek bildirmek yerine ilgili Assably bildirerek dinamik bir yapı sunar.
#endregion


Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=SeperatedConfiguration;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading

    }
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        //ApplyConfiguration Method ilgili config için kullanılabilir.
        modelBuilder.ApplyConfiguration(new BlogConfiguration());

        //ApplyConfigurationsFromAssembly Method ise birden fazla config dosyasının olduğu senaryolarda kullanılabilir. Dinamik bir yapı sunar.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ETicaretDbContext).Assembly);
    }


}

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Post> Posts { get; set; }

}

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Blog> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).HasColumnName("BlogTitle").HasColumnType("nvarchar(200)").IsRequired();
    }
}

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}

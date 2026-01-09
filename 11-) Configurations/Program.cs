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
#region Required - IsRequired
//Bir property'nin null olamayacağını belirtmek için kullanılır.
#endregion
#region Precision - HasPrecision
//Küsüratlı sayılar için toplam basamak sayısı ve ondalık basamak sayısını belirlemek için kullanılır.
#endregion
#region Comment - HasComment
//Tablo veya kolon için açıklama eklemek için kullanılır
#endregion
#region Concurrency - IsConcurrencyToken
//Concurrency kontrolü için kullanılır. Bir property'nin concurrency token olarak işaretlenmesini sağlar. Böylece bu property'nin değeri değiştiğinde EF Core, veritabanındaki değeri kontrol eder ve çakışma durumunda bir istisna fırlatır.
#endregion
#region InverseProperty
//Entityler arasından birden fazla ilişki varsa eğer bu ilişkilerin hangi navigation property'leri üzerinden kurulacağını belirtmek için kullanılır.
#endregion
#endregion
#region Composite Key
//Birden fazla kolondan oluşan primary key tanımlamak için kullanılır.
#endregion



Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tester> Testers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=Config;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
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
        #region IsRequired
        modelBuilder.Entity<Blog>().Property(b => b.Description).IsRequired(false);
        #endregion
        #region HasMaxLength
        modelBuilder.Entity<Blog>().Property(b => b.Title).HasMaxLength(200);
        #endregion
        #region HasPrecision
        //Bu metod, ondalıklı sayılar için toplam basamak sayısını ve ondalık basamak sayısını belirlemenizi sağlar. Örneğin, bir fiyat alanı için kullanılabilir.
        modelBuilder.Entity<Blog>().Property(b => b.Title).HasPrecision(18, 2);
        #endregion
        #region HasComment
        modelBuilder.Entity<Blog>().Property(b => b.Title).HasComment("Blog başlığı");
        #endregion
        #region ConcurrencyToken
        modelBuilder.Entity<Blog>().Property(b => b.Concurency).IsConcurrencyToken();
        #endregion
        #region Composite Key
        //modelBuilder.Entity<Blog>().HasKey(b => new { b.KeyKolonu, b.Title });
        #endregion
        #region HasDefaultValue
        //Default değer atamak için kullanılır.
        modelBuilder.Entity<Blog>().Property(b => b.Concurency).HasDefaultValue(1);
        #endregion
        #region HasDefaultValueSql
        //HasDefaultValueSql metodu, bir kolon için varsayılan değeri SQL ifadesi olarak belirlemenizi sağlar. Bu, özellikle tarih/saat gibi dinamik değerler veya veritabanı tarafından hesaplanan değerler için kullanışlıdır.
        modelBuilder.Entity<Blog>().Property<DateTime>("CreatedDate").HasDefaultValueSql("GETDATE()").IsRequired();
        #endregion
        #region HasComputedColumnSql
        //HasComputedColumnSql metodu, bir kolonun değerinin veritabanı tarafından hesaplanacağını belirtmek için kullanılır. Bu, genellikle diğer kolonların değerlerine dayalı olarak hesaplanan kolonlar için kullanılır.
        modelBuilder.Entity<Tester>().Property(t => t.Y).HasComputedColumnSql("[X] * 2");
        #endregion
        #region HasConstraintName
        //HasConstraintName metodu, bir ilişki için oluşturulan yabancı anahtar kısıtlamasının adını belirlemenizi sağlar. Bu, veritabanı şemasında daha anlamlı ve yönetilebilir kısıtlama adları kullanmak istediğinizde faydalıdır.
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.BlogId)
            .HasConstraintName("FK_Post_Blog_CustomName");
        #endregion
        #region HasData
        //HasData metodu, başlangıç verilerini (seed data) veritabanına eklemek için kullanılır. Bu, uygulama başlatıldığında veya veritabanı oluşturulduğunda belirli verilerin otomatik olarak eklenmesini sağlar.
        modelBuilder.Entity<Blog>().HasData(
            new Blog { KeyKolonu = 1, Title = "First Blog", Description = "This is the first blog", Concurency = 1 },
            new Blog { KeyKolonu = 2, Title = "Second Blog", Description = "This is the second blog", Concurency = 1 }
        );
        #endregion
        #region HasDiscriminator
        //HasDiscriminator metodu, tablo başına sınıf hiyerarşisi (Table-per-Hierarchy, TPH) stratejisini kullanarak kalıtım yapılan entity'ler için ayrımcı (discriminator) kolonunu yapılandırmak için kullanılır. Bu kolon, her bir türe özgü verilerin aynı tabloda saklanmasını sağlar ve hangi türün hangi satıra ait olduğunu belirler.
        #endregion
        #region HasNoKey
        //Normal şartlarda tüm entitylerin bir PK olması gerekir. Ancak bazı durumlarda key'siz entity'ler tanımlamak isteyebiliriz. Örneğin, raporlama amaçlı veya view'lar üzerinden veri çekmek istediğimizde key'siz entity'ler kullanabiliriz. İşte bu tür durumlarda HasNoKey metodu kullanılır.
        modelBuilder.Entity<Tester>().HasNoKey();
        #endregion
        #region HasIndex
        //Eğerki bir tabloda index eklemek istiyorsak HasIndex metodunu kullanabiliriz. Index'ler, veritabanı sorgularının performansını artırmak için kullanılır. Sıkça sorgulanan kolonlar üzerinde index oluşturmak, arama işlemlerini hızlandırır.
        modelBuilder.Entity<Tester>().HasIndex(b => b.X).HasDatabaseName("IX_Blog_Title");
        #endregion
        #region HasQueryFilter
        //Globel Query Filter tanımlamak için kullanılır. Belirli bir entity için tüm sorgularda otomatik olarak uygulanacak filtreler tanımlamamıza olanak sağlar. Örneğin, soft delete (yumuşak silme) senaryolarında sıkça kullanılır.
        modelBuilder.Entity<Blog>().HasQueryFilter(b => !EF.Property<bool>(b, "IsDeleted"));
        #endregion


    }

}

[Table("Test")]
public class Blog
{
    [Key]
    public int KeyKolonu { get; set; }
    public string Title { get; set; }
    //[InverseProperty(nameof(Post.Id))]
    public ICollection<Post> Posts { get; set; }
    [NotMapped]
    public string NotMappedProperty { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
    [Comment("Açıklama alanı")]
    public string? Description { get; set; }
    [ConcurrencyCheck]
    public int Concurency { get; set; }

}

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }

    public Blog Blog { get; set; }
}


public class Tester
{
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

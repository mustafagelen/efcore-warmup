using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
ETicaretDbContext dbContext = new();


#region Shadow Property
//Tablo gösterilmesini istemediğimiz görmediğimiz işlem yapılmasını istemediğimiz kolonlar için kullanılır. Entity classların içerisinde property olarak tanımlanmazlar. Sadece model oluşturulma sürecinde fluent api ile tanımlanırlar. Genellikle audit işlemlerinde kullanılırlar. CreatedDate, UpdatedDate, CreatedBy, UpdatedBy gibi kolonlar için idealdir.
//Shadow Propertylerin değerleri ve stateleri Change Tracker üzerinden erişilir ve yönetilir.
#endregion
#region Foreign Key Shadow Property
//İlişkisel senaryolarda foreign key propertysini tanımlamadığımız halde Ef core tarafından dependent entity içine eklenmektedir. İşte bu shadow propertydir.
var blogs = await dbContext.Blogs.Include(x => x.Posts).ToListAsync();
#endregion
#region Shadow Property Oluşturma
//Bir entity üzerinde shadow property oluşturmak için model oluşturulma sürecinde fluent api kullanılır.
//OnModelCreating methodu içerisinde fluent api kuralları iler tanımlanır.
#endregion
#region Shadow Property Erişimi
#region Change Tracker Üzerinden Erişim
//Shadow property ile erişim sağlayabilmek için Change Tracker API'si kullanılır.
var CTentries = await dbContext.Blogs.FirstAsync();
var createdDate = dbContext.Entry(CTentries).Property("CreatedDate");
createdDate.CurrentValue = DateTime.Now;
await dbContext.SaveChangesAsync();
#endregion
#region Ef Core API Üzerinden Erişim
//Özellikle LINQ sorgularında shadow propertylerine erişim için EF.Property yapılamasını kullanabiliriz.
var createdDateOrderBy = await dbContext.Blogs.OrderBy(x=> EF.Property<DateTime>(x,"CreatedDate")).ToListAsync();
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
        //Shadow Property Oluşturma
        modelBuilder.Entity<Blog>()
            .Property<DateTime>("CreatedDate");
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

    public Blog Blog { get; set; }
}

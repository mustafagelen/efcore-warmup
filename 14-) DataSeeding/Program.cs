using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
ETicaretDbContext dbContext = new();

#region Seed Data
//Ef core ile veritabanı nesneleri oluşturulabildiği gibi migrate sürecinde verilerin de oluşturulması mümkündür.
//Seed Datalar yazılım kısmında tutulmalıdır. Veriler üzerinde veritabanı seviyesinde istenilen manipülasyonlar gerçekleştirilebilmektedir.

//Data Seeding; Test için, Identity yapılanmasındaki roller için, yazılım için temel konfig değerler için kullanılabilir.
#endregion
#region İlişkisel Tablolar İçin Seed Data
//İlişkisel senaryolarda dependet table'a veri eklerken FK varsa eğer ona değerini vererek ekleme işlemini yapıyoruz.
//Yani BlogId olmalı ki seed datayı sorunsuz oluşturabilelim.
#endregion

#region Seed Datanın Pk Değerini Değiştirme
//Önceden oluşturulan Postların BlogIdleri 1 id değerine sahip blogları gösteriyorkan, ilgili id 11 ile değiştirilirse bir hata almayız çünkü davranış Cascade olarak default kullanılıyor.
#endregion
Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=SeedData;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading

    }
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>().HasData(
            new Blog() { Id = 11, Title = "Test1" },
            new Blog() { Id = 2, Title = "Test2" }
        );

        modelBuilder.Entity<Post>().HasData(
            new Post() { Id = 4, BlogId = 1, Content = "Test Content" },
            new Post() { Id = 5, BlogId = 1, Content = "Test Content" }
        );
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

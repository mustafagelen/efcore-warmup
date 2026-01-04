using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
ETicaretDbContext dbContext = new();

//One to One İlişki Ekleme- Principal entity üzerinden ekleme yapılacaksa dependent entity zorunlu değildir. Ama dependent entity üzerinden ekleme yapılacaksa principal entity zorunludur.
//1-) Principal entity üzerinden dependent entity ekleme
Person person = new() { Name = "Fk", Surname = "Gelen" };
person.Address = new Address() { PersonAddress = "İzmir" };
await dbContext.Persons.AddAsync(person);
await dbContext.SaveChangesAsync();

//2-) Dependent entity üzerinden principal entity ekleme
Address address = new() { PersonAddress = "Ankara" };
address.Person = new Person() { Name = "Ahmet", Surname = "Yılmaz" };
await dbContext.Addresses.AddAsync(address);
await dbContext.SaveChangesAsync();


//One to Many İlişki Ekleme
Blog blog = new();
blog.Title = "C# Blog";

//Nesne referansları üzerinden ekleme
blog.Posts.Add(new Post() { Content = "C# 12 Yenilikleri" });
blog.Posts.Add(new Post() { Content = "Entity Framework Core 8 Yenilikleri" });

//Object initializer ile ekleme
blog.Posts = new HashSet<Post>()
{
    new Post() { Content = "ASP.NET Core 8 Yenilikleri" },
    new Post() { Content = "Blazor ile WebAssembly" }
};

await dbContext.Blogs.AddAsync(blog);
await dbContext.SaveChangesAsync();



Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=ECommerceDb;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Address)
            .WithOne(a => a.Person)
            .HasForeignKey<Address>(a => a.Id);
    }
}

public class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
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
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Address Address { get; set; }
}

public class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }

    public Person Person { get; set; }

}

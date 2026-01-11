using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
ETicaretDbContext dbContext = new();

#region Table Per Hierarchy Nedir
//Kalıtımsal ilişkiye sahip olan entitylerin olduğu senaryolarda her bir hiyerarşiye karşılık bir tablo oluşturan davranıştır.Discriminator ile işaretlersin tabloda bulunan bir kolon ile.
//Neden ihtiyacımız var dersek benzer alanlara sahip olan entityleri migrate ettiğimizde her entitiye karşılık bir tablo oluşturmaktansa bu entityleri tek bir tabloda modellemek istebiliriz.
#endregion
#region TPH Nasıl Uygulanır
//Default olarak kabul edilen davranıştır, bu sebeple herhangi bir configurasyon gerektirmez.
//Entityler kendi aralarında kalıtımsal ilişkiye sahipse ve DbSet ile eklendiyse default olarak uygulanacak.
#endregion
#region Discriminator Kolonu Nedir
//Default olarak içerisinde entity isimlerini tutan ve birleştirdiğimiz tabloların hangisine ait verilerinin tutulduğunu belirten yapılanmadır. Kolon adını nasıl değiştirebiliriz dersek fluent validation ile yapabiliriz. HasDiscriminator methodu ile özelleştirilebilir.
#endregion
#region Discriminator Type Değişimi
//OnModelCreting içinde HasDiscriminator için farklı tipi belirttikten sonra HasValue ile bu değeri bildirebilirsin.
#endregion
#region  TPH Veri Sorgulama
//Sorgulama süreçlerinde EfCore generate edilen sorguya bir şartı eklemektedir.
//Üst sınıfta yapılacak sorgulamalarda alt sınıf verileri de gelecektir.
//Örneğin Employee sorgusu yaparsak Plumber tablosundaki veriler de gelecektir
#endregion


Person person = new Person()
{
    Name = "Mustafa",
    Surname = "Gelen"
};

Plumber plumber = new Plumber()
{
    Name = "Yusuf",
    Surname = "Gelen",
    Branch = "Plumber"
};

await dbContext.Persons.AddRangeAsync(plumber, person);
await dbContext.SaveChangesAsync();

Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Plumber> Plumbers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=TablePerHierarchy;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading

    }
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Default
        //modelBuilder.Entity<Person>().HasDiscriminator<string>("Tablo Belirticisi");

        //Custom Type
        modelBuilder.Entity<Person>().HasDiscriminator<int>("TabloBelirticisi")
            .HasValue<Person>(1)
            .HasValue<Employee>(2)
            .HasValue<Customer>(3)
            .HasValue<Plumber>(4);
    }


}

public class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

}


public class Employee : Person
{
    public string? Departent { get; set; }
}

public class Customer : Person
{
    public string? CompanyName { get; set; }
}

public class Plumber : Employee
{
    public string? Branch { get; set; }
}

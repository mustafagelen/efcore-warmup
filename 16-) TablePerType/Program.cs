using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
ETicaretDbContext dbContext = new();

#region Table Per Type Nedir
//Her generate edilen tablolar 1-1 ilişkiye sahip olacak şekilde bütün entityler için tablolar oluşturuluyor.
//Entityler DbSet olarak bildirilmelidir.
#endregion

#region Table Per Concrete Nedir
//Sadece concrete somut olan entitylere karşılık bir tablo oluşturacak davranış modelidir. 
//TPC TPT nin daha performanslı modelidir.
#endregion

Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Plumber> Plumbers { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=TablePerType;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading

    }
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TPT uygulayabilmek için bu config yapılmalı
        //modelBuilder.Entity<Person>().ToTable("Persons");
        //modelBuilder.Entity<Employee>().ToTable("Employees");
        //modelBuilder.Entity<Customer>().ToTable("Customers");
        //modelBuilder.Entity<Plumber>().ToTable("Plumbers");

        modelBuilder.Entity<Person>().UseTpcMappingStrategy();

    }


}

 public abstract class Person
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

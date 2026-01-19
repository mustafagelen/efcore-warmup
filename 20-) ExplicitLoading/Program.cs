using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Reflection.Emit;
ETicaretDbContext dbContext = new();

#region Explicit Loading
//Oluşturulan sorguya eklenecek verilerin şartlara bağlı bir şekilde/ihtiyaçlara istinaden yüklenmesini sağlayan bir yöntemdir.
#region Reference 
//Explicit Loading operasyonunu Reference tipindeki navigation propertyler için kullanmak
var emp = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == 2);
var reqionEntry = dbContext.Entry(emp).Reference(e => e.Region).LoadAsync();
#endregion
#region Collection 
//Explicit Loading operasyonunu Collection tipindeki navigation propertyler için kullanmak
var empCollection = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == 2);
var reqionEntryCollection = dbContext.Entry(empCollection).Collection(e => e.Orders).LoadAsync();
#endregion
#region Collection Aggregate Operasyonları
//Kullanma amacı: Yüklenen ilişkisel veriler üzerinde filtreleme, sıralama gibi operasyonlar gerçekleştirmek
var empCollectionAggregate = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == 2);
var reqionAggregate = dbContext.Entry(empCollectionAggregate).Collection(e => e.Orders).Query().CountAsync();
#endregion
#region Collection Filtreleme Operasyonları
//Kullanma amacı: Yüklenen ilişkisel veriler üzerinde filtreleme, sıralama gibi operasyonlar gerçekleştirmek
var empCollectionFilter = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == 2);
var reqionFilter = dbContext.Entry(empCollectionFilter).Collection(e => e.Orders).Query().Where(o => o.OrderDate.Year == 2024).ToListAsync();
#endregion

#endregion


Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Region> Regions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=EagerLoading;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading

    }
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //AutoInclude
        modelBuilder.Entity<Employee>().Navigation(e => e.Region).AutoInclude();

    }


}


public class Person
{
    public int Id { get; set; }

}
public class Employee : Person
{
    //public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public List<Order> Orders { get; set; }
    public Region Region { get; set; }
}
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    public Employee Employee { get; set; }
}
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Reflection.Emit;
ETicaretDbContext dbContext = new();

#region Eager Loading
//Oluşturulan bir sorguya ilişkisel verilerin parça parça eklenmesini sağlayan, developerin iradesine bırakan bir yöntemdir.
#endregion
#region Include
//Eager Loading operasyonunu yapmamızı sağlayan bir fonksiyondur.
//Yani üretilen bir sorguya diğer ilişkisel tabloların dahil edilmesini sağlayan bir işleve sahiptir

//Orders tablosunu da dahil ediyoruz. Eager loading arka planda üretilen sorguya uygun joini uygular. Ayrıca ilgili tablo
var employee = await dbContext.Employees.Where(e => e.Orders.Count > 2).Include(x => x.Orders).Include(y => y.Region).ToListAsync();
#endregion

#region ThenInclude
//Üretilen sorgufa Include edilen tabloların ilişkili olduğu diğer tablolarıda sorguya ekleyebilmek için kullanılan bir fonksiyondur.
//Navigation Property koleksiyonel bir prop ise işste o zaman bu prop üzerinden diğer ilişkisel tabloya erişim gösterilebilmektedir.
var employeeThenInclude = await dbContext.Regions.Include(a => a.Employees).ThenInclude(o => o.Orders).ToListAsync();
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
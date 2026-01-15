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
//Üretilen sorguda Include edilen tabloların ilişkili olduğu diğer tablolarıda sorguya ekleyebilmek için kullanılan bir fonksiyondur.
//Navigation Property koleksiyonel bir prop ise işte o zaman bu prop üzerinden diğer ilişkisel tabloya erişim gösterilebilmektedir.
//Burdaki yaklaşımın sebebi employees içerisinde orders bir collection olarak tutuluyor.
var employeeThenInclude = await dbContext.Regions.Include(a => a.Employees).ThenInclude(o => o.Orders).ToListAsync();
#endregion
#region Filtered Include
//Sorgulama süreçlerinde Include yaparken sonuçlar üzerinde filtreleme ve sıralama gerçekleştirebilmemizi sağlayan bir özelliktir.
var filteredInclude = await dbContext.Regions.Include(a => a.Employees.Where(x => x.Name.Contains("a"))).ToListAsync();

//Desteklenen fonksiyonlar Where, OrderBy, OrderByDescending, ThenBy, Skip, ThenByDescending.
//Change Trackerin aktif olduğu durumlarda Include edilmiş sorgular üzerindeki filtreleme sonuçları beklenmeyen olabilir. Bundan dolayı sağlıklı bir filtered include operasyonu için change tracker kullanılmayan sorguları tercih etmeyi düşünebiliriz.
#endregion
#region Eager Loading İçin Kritik Bilgi
//EfCore önceden üretilmiş ve execute edilerek verileri bellege alınmış olan sorguların verilerini sonraki sorgularda kullanır.
var orders = await dbContext.Orders.ToListAsync();
var employe = await dbContext.Employees.ToListAsync();
#endregion
#region AutoInclude
//Uygulama seviyesinde bir entitye karşılık yapılan tüm sorgulamalarda kesinlikle bir tabloya Include opersayonu işlemi gerçekleştirilecekse her bir sorguya tek tek yapmaktansa merkezi hale getirme yaklaşımıdır.
var employeeAuto = await dbContext.Employees.ToListAsync();
#endregion
#region IgnoreAutoInclude
//Sadece ilgili sorgu için geçerli
var employeeIgnoreAuto = await dbContext.Employees.IgnoreAutoIncludes().ToListAsync();
#endregion
#region Kalıtımsal Durumlarda Include
#region Cast Operatörü ile
var personsCast = await dbContext.Persons
    .Where(p => p is Employee && ((Employee)p).Salary > 5000)
    .Include(e => ((Employee)e).Orders)
    .ToListAsync();

#endregion
#region as Operatörü ile
var personsAs = await dbContext.Persons
    .Where(p => p is Employee && (p as Employee).Salary > 5000)
    .Include(e => (e as Employee).Orders)
    .ToListAsync();

#endregion
#region 2.Overload Operatörü ile
var personsOfType = await dbContext.Persons.Include("Orders").ToListAsync();
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
using Microsoft.EntityFrameworkCore;
ETicaretDbContext dbContext = new();

#region Veri nasıl silinir?
//var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == 1);
//if (product != null)
//{
//    dbContext.Products.Remove(product);
//    await dbContext.SaveChangesAsync();
//}
#endregion
#region Silme işleminde Change Tracker'ın rolü nedir?
//Change Tracker, context üzerinden getirilen verilerin takibinden sorumlu mekanizmadır. Bu mekanizma sayesinde context üzerinden gelen verilerin update
//veya delete sorgularının oluşturulacağı 
#endregion
#region Takip edilmeyen nesneler nasıl silinir?
////Context üzerinden gelmediği için change tracker tarafından takip edilmez. Id ile yakalanıp silinir.
//var product = new Product() { Id = 2 };
//dbContext.Products.Remove(product);
#endregion
#region EntityState ile silme işlemi nasıl yapılır?
////EntityState ile nesnenin durumunu deleted olarak belirleyerek siliyoruz.
//var product = new Product() { Id = 3 };
//dbContext.Entry(product).State = EntityState.Deleted;
//await dbContext.SaveChangesAsync();
#endregion
#region RemoveRange
//var Products = await dbContext.Products.Where(p => p.Id > 2 && p.Id < 5).ToListAsync();
//dbContext.Products.RemoveRange(Products);
//await dbContext.SaveChangesAsync();
#endregion

Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=ECommerceDb;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading
    }

}
//Savechanges 
public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }

}
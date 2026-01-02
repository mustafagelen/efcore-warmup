using Microsoft.EntityFrameworkCore;
ETicaretDbContext dbContext = new();

#region Veri nasıl güncellenir?
//var products = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == 1);
//products.ProductName = "Updated Phone";
//products.Description = "An updated description for the smartphone.";
//await dbContext.SaveChangesAsync();
#endregion
#region Update Fonksiyonu ile Güncelleme
////Context üzerinden gelmediği için change tracker tarafından takip edilmez.
//Product p = new() { Id = 2, ProductName = "Updated Laptop", Description = "An updated description for the laptop." };
////Change tracker tarafından takip edilmeyen nesnelerin güncellenmesi için update fonksiyonu kullanılır.
////Bu fonksiyonu kullanmak için kesinlikle Id property'sinin set edilmesi gerekir.
//dbContext.Products.Update(p);
//await dbContext.SaveChangesAsync();
#endregion
#region EntityState Nedir?
//Product p = new() { Id = 3, ProductName = "Tablet", Description = "A powerful tablet device." };
//Console.WriteLine(dbContext.Entry(p).State);
#endregion
#region Birden fazla veri güncellenirken nelere dikkat edilmelidir?
////Yapılan güncellemelerden sonra SaveChangesAsync fonksiyonunu tek sefer çağırmak performans açısından daha iyidir.
//var products = await dbContext.Products.ToListAsync();
//foreach (var item in products)
//{
//    item.Description += " - Updated";
//}
//await dbContext.SaveChangesAsync();
#endregion

Console.WriteLine();
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
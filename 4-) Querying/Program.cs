using Microsoft.EntityFrameworkCore;
ETicaretDbContext dbContext = new();

#region Temel seviyede query işlemleri
#region Method Syntax
//var products = await dbContext.Products.ToListAsync();
#endregion
#region Query Syntax
//var productsQuery= await(from product in dbContext.Products select products).ToListAsync();
#endregion

#endregion
#region IQueryable ve IEnumerable Arasındaki Farklar Nelerdir?
//IQueryable: Veritabanı üzerinde sorgu oluşturur ve veritabanında çalışır. Performans açısından daha iyidir.
//IEnumerable: Bellek üzerinde çalışır. Tüm verileri belleğe yükler ve ardından filtreleme yapar. Büyük veri setlerinde performans sorunlarına yol açabilir.
#region ToListAsync Kullanımı
//Method Syntax
//var productsList = await dbContext.Products.ToListAsync();
//Query Syntax
//var productsListQuery = await (from product in dbContext.Products select product).ToListAsync();
#endregion
#endregion
#region Deffered Execution Nedir?
int productId = 1;
var productQuery = from product in dbContext.Products
                   where product.Id == productId
                   select product;
productId = 2;

//Tam bu kısımda sorgu IQueryable yapısından IEnumerable yapısına dönüşür ve sorgu veritabanına gönderilir.
//Gönderilmeden önce ise sorguda parametre olarak kullanılan productId değişkeninin değeri 2 olarak alınır. Yani son değeri alınır.
foreach (Product product in productQuery)
{
    Console.WriteLine(product.ProductName);
}

//IQuerable çalışmalarında ilgili kod yazıldığı noktada tetiklenmez yani yazıldığı noktada sorguyu generate etmez. Foreach ise bu sorguyu tetikler ve veritabanına gönderir.

#endregion
#region Çoğul veri getiren sorgular
#region ToListAsync
//Üretilen sorguyu execute etmemizi sağlayan fonksiyondur.
#region Method Syntax
//var products= await dbContext.Products.ToListAsync();
#endregion
#region Query Syntax
//var productsQuery= await (from product in dbContext.Products select product).ToListAsync();
#endregion
#endregion
#region Where
//Koşullu veri getiren sorgular için kullanılır.
#region Method Syntax
//var filteredProducts = await dbContext.Products
//    .Where(p => p.Id > 2 && p.Id < 5)
//    .ToListAsync();
#endregion
#region Query Syntax
//var filteredProductsQuery = await (from product in dbContext.Products
//                                    where product.Id > 2 && product.Id < 5
//                                    select product).ToListAsync();
#endregion
#endregion

//OrderBy
var products= await dbContext.Products.OrderBy(p=>p.ProductName).ToListAsync();
var productsQuery= await (from product in dbContext.Products
                          where product.Id > 2 && product.Id < 5 && product.ProductName.Contains("a")
                          orderby product.ProductName
                          select product).ToListAsync();

#endregion


Console.WriteLine("Done");
public class ETicaretDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Piece> Pieces { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost;Database=ECommerceDb;User Id=sa;Password=Mg123456;TrustServerCertificate=True;");
        //Provider
        //Connection String
        //Layz Loading
    }

}
public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }



    public ICollection<Piece> Pieces { get; set; }
}

public class Piece
{
    public int Id { get; set; }
    public string PieceName { get; set; }
}
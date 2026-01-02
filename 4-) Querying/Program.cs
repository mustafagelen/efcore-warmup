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
var products = await dbContext.Products.OrderBy(p => p.ProductName).ToListAsync();
var productsQuery = await (from product in dbContext.Products
                           where product.Id > 2 && product.Id < 5 && product.ProductName.Contains("a")
                           orderby product.ProductName
                           select product).ToListAsync();

#endregion
#region Tekil veri getiren sorgular
//Single vs SingleOrDefault
//Yapılan sorguda save ve sadece tek bir verinin gelmesini amaçlıyorsa Single ya da SingleOrDefault fonksiyonları kullanılabilir. Default olarak null dönecek
var productsSingle = await dbContext.Products.SingleAsync(p => p.Id == productId);
var productsSingleOrDefault = await dbContext.Products.SingleOrDefaultAsync(p => p.Id == productId);

//First vs FirstOrDefault
var productsFirst = await dbContext.Products.FirstAsync(p => p.Id == 55);
var productsFirstOrDefault = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == 55);

//Find
//Primary key columnu kullanarak tekil veri getiren sorgularda kullanılır. Repository Design Pattern'de sıkça kullanılır.
var productFind = await dbContext.Products.FindAsync(1);

//Last vs LastOrDefault
//Gelen verilerin sonuncusunu getirir.
var productsLast = await dbContext.Products.LastAsync(p => p.Id > 2);
#endregion
#region Diğer Sorgulama Fonksiyonları
//Count oluşturulan sorgunun execute edilmesi sonucunda kaç adet veri döneceğini verir. Count int LongCount ise long tipinde sonuç döner.
var productCount = await dbContext.Products.CountAsync();

//Any oluşturulan sorgunun execute edilmesi sonucunda en az bir adet veri dönüyorsa true döner. Hiç veri dönmüyorsa false döner.
var isAnyProduct = await dbContext.Products.AnyAsync(p => p.Id > 5);

//Max oluşturulan sorgunun execute edilmesi sonucunda ilgili kolondaki en büyük değeri döner.
var maxProductId = await dbContext.Products.MaxAsync(p => p.Id);

//Distinct oluşturulan sorgunun execute edilmesi sonucunda dönen verilerdeki tekrar eden kayıtları tekilleştirir.
var distinctProducts = await dbContext.Products
    .Select(p => p.ProductName)
    .Distinct().ToListAsync();

//Sum oluşturulan sorgunun execute edilmesi sonucunda ilgili kolondaki değerlerin toplamını döner.
var totalProductIds = await dbContext.Products.SumAsync(p => p.Id);

//All oluşturulan sorgunun execute edilmesi sonucunda dönen tüm verilerin belirtilen koşulu sağlaması durumunda true döner. Aksi halde false döner.
var areAllProductsAboveIdZero = await dbContext.Products.AllAsync(p => p.Id > 0);

//Contains oluşturulan sorgunun execute edilmesi sonucunda belirtilen değerin ilgili kolonda bulunup bulunmadığını kontrol eder. Bulunuyorsa true, bulunmuyorsa false döner.
var containsProductName = await dbContext.Products
    .Where(p => p.ProductName.Contains("7")).ToListAsync();

//StartsWith EndsWith
var startsWithAProducts = await dbContext.Products
    .Where(p => p.ProductName.StartsWith("A")).ToListAsync();
var endsWithZProducts = await dbContext.Products
    .Where(p => p.ProductName.EndsWith("Z")).ToListAsync();
#endregion
#region Sorgu sonucu dönüşüm fonksiyonları
//ToDictionaryAsync  ToList ile aynı amaca hizmet etmektedir. Ancak ToList gelen sorgu neticesini entity türünde bir koleksiyona dönüştürmekteyken ToDictionary gelen sorgu neticesini Dictionary türünde bir koleksiyona dönüştürür.
var productsToDictionary = await dbContext.Products.ToDictionaryAsync(p => p.Id, p => p.ProductName);

//ToArrayAsync Oluşturulan sorguyu dizi olarak elde eder. ToList ile aynı amaca hizmet eder.
var productsToArray = await dbContext.Products.ToArrayAsync();

//Select fonksiyonu işlevsel olarak birden fazla davranışı söz konusudur.
//1. İlgili sorgudan belirli kolonları çekmek için kullanılır.
var productsNames = await dbContext.Products.Select(p => new Product
{
    Id = p.Id,
    Description = p.Description
}).ToListAsync();

//2. Select fonksiyonu gelen veriler farklı türlerde karşılamamızı sağlar "T"
var productsNamesGeneric = await dbContext.Products.Select(p => new
{
    Id = p.Id,
    Description = p.Description
}).ToListAsync();

//SelectMany ilgili sorgudan gelen verilerin içerisindeki koleksiyonel yapıdaki verileri düzleştirerek tek bir koleksiyon halinde elde etmemizi sağlar.
var productsPieces = await dbContext.Products.Include(p => p.Pieces).SelectMany(p => p.Pieces, (p, k) => new
{
    ProductName = p.ProductName,
    PieceName = k.PieceName
}).ToListAsync();


#endregion

#region GroubBy Kullanımı ve Foreach
var datas = await dbContext.Products.GroupBy(p => p.ProductName.Contains("Iphone")).Select(group => new
{
    Count = group.Count(),
    ProductName = group.Key,
}).ToListAsync();

var datasQuery = await (from product in dbContext.Products
                        group product by product.ProductName
                 into gr
                        select new
                        {
                            ProductName = gr.Key,
                            Count = gr.Count(),
                        }).ToListAsync();

//Foreach sorgulama neticesinde elde edilen koleksiyonel veriler üzerinde iterasyonel olarak dönmemizi ve teker teker verileri elde edip işlemler yapabilmemizi sağlayan bir fonksiyondur.
foreach (var product in datas)
{
    Console.WriteLine(product.Count);
}

datas.ForEach(p =>
{
    Console.WriteLine("test");
});


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
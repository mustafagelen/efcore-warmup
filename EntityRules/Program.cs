using Microsoft.EntityFrameworkCore;

public class ETicaretDbContext: DbContext
{
    public  DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Provider
        //Connection String
        //Layz Loading
    }

}

public class  Product
{
    
}
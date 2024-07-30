using Microsoft.EntityFrameworkCore;

namespace CustomerApi.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseSqlite("Data Source=database.db");
    //    }
    //}
}


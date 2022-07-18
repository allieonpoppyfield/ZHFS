using Microsoft.EntityFrameworkCore;
namespace ZHFS.Database;
public class AppDbContext : DbContext
{
    //public AppDbContext() : base()
    //{
    //    Database.EnsureCreated();
    //}

    public AppDbContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-BPD4NLJ\SQLEXPRESS;Database=ZFSDB;Trusted_Connection=True;"); // тут офк заменить на конфиг (ну и вам на свой коннекшн стринг)
    }
    public DbSet<User>? Users { get; set; }
    public DbSet<Product>? Products { get; set; }
}

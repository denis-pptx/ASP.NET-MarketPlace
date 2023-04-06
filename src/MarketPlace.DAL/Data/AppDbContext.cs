using MarketPlace.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DAL.Data;


public class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Seller> Sellers { get; set; }
    public DbSet<Product> Products { get; set; }
	public DbSet<Shop> Shops { get; set; }
    public DbSet<CustomerProfile> CustomerProfiles { get; set; }
	
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

		// Shop shop = new Shop { Id = 1, Name = "restore" };
		// Product product = new Product { Id = 1, Description = "descr", Name = "phone", Price = 500, ShopId = shop.Id };
		 
		User admin = new User { Id = 1, Login = "admin", Password = "admin", Role = Enum.Role.Admin };

		// modelBuilder.Entity<Product>().HasData(product);
		// modelBuilder.Entity<Shop>().HasData(shop);
		modelBuilder.Entity<User>().HasData(admin);
    }
}

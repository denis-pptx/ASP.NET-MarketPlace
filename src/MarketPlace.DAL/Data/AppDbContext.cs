using MarketPlace.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DAL.Data;


public class AppDbContext : DbContext
{
	public DbSet<Product> Products { get; set; }
	public DbSet<User> Users { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
		Database.EnsureCreated();
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		User admin = new User { Id = 1, Login = "admin", Password = "admin", Role = Enum.Role.Administrator };
		Product product1 = new Product { Id = 1, Description = "descr", Name = "phone", Price = 500 };

		modelBuilder.Entity<User>().HasData(admin);
		modelBuilder.Entity<Product>().HasData(product1);
    }
}

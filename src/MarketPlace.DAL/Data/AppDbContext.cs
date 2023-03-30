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
}

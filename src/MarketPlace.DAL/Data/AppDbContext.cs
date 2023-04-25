namespace MarketPlace.DAL.Data;

public class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Seller> Sellers { get; set; }
    public DbSet<Product> Products { get; set; }
	public DbSet<Shop> Shops { get; set; }
    public DbSet<Profile> CustomerProfiles { get; set; }
    public DbSet<Cart> Carts { get; set; }
	
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        // Database.EnsureDeleted(); 
		// Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<User>()
			.HasIndex(u => u.Login)
			.IsUnique();


        modelBuilder.Entity<User>().HasData(new User 
		{ 
			Id = 1, 
			Login = "admin", 
			Password = "admin",
			Role = Role.Admin 
		});
    }
}

using MarketPlace.DAL.Data;
using MarketPlace.DAL.Entities;
using MarketPlace.DAL.Interfaces;
using MarketPlace.DAL.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Adding mvc support.
builder.Services.AddMvc();

// Database configuration.
var connection = builder.Configuration.GetConnectionString("SQLiteConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connection), ServiceLifetime.Singleton);

// Registering repositories.
builder.Services.AddSingleton<IRepository<Product>, EfRepository<Product>>();
builder.Services.AddSingleton<IRepository<User>, EfRepository<User>>();
builder.Services.AddSingleton<IUnitOfWork, EfUnitOfWork>();

var app = builder.Build();

// Mapping routes to controllers.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


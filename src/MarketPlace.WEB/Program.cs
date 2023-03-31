using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.Services;
using MarketPlace.DAL.Data;
using MarketPlace.DAL.Entities;
using MarketPlace.DAL.Interfaces;
using MarketPlace.DAL.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Adding mvc support.
builder.Services.AddMvc();

// Authentication / authorization settings.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/AccessDenied");
    });
builder.Services.AddAuthorization();

// Database configuration.
var connection = builder.Configuration.GetConnectionString("SQLiteConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connection), ServiceLifetime.Singleton);

// Registering repositories.
builder.Services.AddSingleton<IRepository<Product>, EfRepository<Product>>();
builder.Services.AddSingleton<IRepository<User>, EfRepository<User>>();
builder.Services.AddSingleton<IUnitOfWork, EfUnitOfWork>();

// Registering services.
builder.Services.AddSingleton<IAccountService, AccountService>();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


// Mapping routes to controllers.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


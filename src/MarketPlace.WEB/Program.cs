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
    options.UseSqlite(connection));

// Repositories registration.
builder.Services.AddScoped<IRepository<User>, EfRepository<User>>();
builder.Services.AddScoped<IRepository<Customer>, EfRepository<Customer>>();
builder.Services.AddScoped<IRepository<Seller>, EfRepository<Seller>>();
builder.Services.AddScoped<IRepository<Shop>, EfRepository<Shop>>();
builder.Services.AddScoped<IRepository<Product>, EfRepository<Product>>();
builder.Services.AddScoped<IRepository<CustomerProfile>, EfRepository<CustomerProfile>>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

// Services registration.
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IShopService, ShopService>();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


// Mapping routes to controllers.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


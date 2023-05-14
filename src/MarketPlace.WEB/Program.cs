using MarketPlace.BLL.Services;
using MarketPlace.DAL.Data;
using MarketPlace.DAL.Interfaces;
using MarketPlace.DAL.Repository;
using MarketPlace.WEB;
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
    options.UseLazyLoadingProxies().UseSqlite(connection));

// Repositories registration.
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

// Services registration.
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<ISellerService, SellerService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

// Authentication / authorization settings.
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

// Mapping routes to controllers.
app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller}/{action}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


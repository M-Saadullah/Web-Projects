using System.Security.Claims;
using Application;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using The_Outfit.Models;
using WebApplication2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<MyAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddSingleton<IArticleService,ArticleServiceRepository>();
builder.Services.AddTransient<IArticleService,ArticleServiceRepository>();
builder.Services.AddSingleton<IOrderService,OrderServiceRepository>();

builder.Services.AddSingleton<IRepository<Article>>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new GenericRepository<Article>(connectionString);
});

builder.Services.AddSingleton<IRepository<OrderDetail>>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new GenericRepository<OrderDetail>(connectionString);
});

builder.Services.AddSingleton<IRepository<OrderProduct>>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new GenericRepository<OrderProduct>(connectionString);
});


builder.Services.AddSingleton<IRepositoryService<Article>,RepositoryServiceRepository<Article>>();
builder.Services.AddSingleton<IRepositoryService<OrderDetail>, RepositoryServiceRepository<OrderDetail>>();
builder.Services.AddSingleton<IRepositoryService<OrderProduct>, RepositoryServiceRepository<OrderProduct>>();
builder.Services.AddSingleton<ICartService, CartServiceRepository>();


builder.Services.AddSingleton<ICart>(new CartRepository(connectionString));
builder.Services.AddSingleton<IArticle>(new ArticleRepository(connectionString,new GenericRepository<Article>(connectionString)));
builder.Services.AddSingleton<IOrder>(new OrderRepository());
builder.Services.AddTransient<ICart, CartRepository>(provider =>
{
    var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"TheOutfit\";Integrated Security=True;"; // Or fetch from configuration
    return new CartRepository(connectionString);
});





builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization(options => { options.AddPolicy("Admin_only", policy => policy.RequireClaim(ClaimTypes.Email, "Msaadullah021@gmail.com")); });
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

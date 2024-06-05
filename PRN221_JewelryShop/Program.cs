using BLL.Interfaces;
using BLL.Services;
using BOL.Entities;
using DAL;
using DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddDbContext<JewelryShopDbContext>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IGenericRepository<Customer>, GenericRepository<Customer>>();
builder.Services.AddHttpClient<IGoldPriceService, GoldPriceService>()
     .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
     {
         ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
     });
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
builder.Services.AddScoped<IImageService, ImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapGet("/", async (HttpContext context) =>
{
	context.Response.Redirect("/Login");
});
app.UseSession();
app.Run();

using BLL.Interfaces;
using BLL.Services;
using BOL.Entities;
using DAL;
using DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<JewelryShopDbContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IGenericRepository<Customer>, GenericRepository<Customer>>();
builder.Services.AddHttpClient<IGoldPriceService, GoldPriceService>()
     .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
     {
         ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
     });
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
app.Run();

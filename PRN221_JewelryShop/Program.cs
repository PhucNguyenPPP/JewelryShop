using BLL.Interfaces;
using BLL.Services;
using BOL;
using DAL;
using DAL.DAO;
using Repositories.Interfaces;
using Repositories.Repositories;

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
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//Add scope for services
builder.Services.AddHttpClient<IGoldPriceService, GoldPriceService>()
     .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
     {
         ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
     });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IMaterialProductService, MaterialProductService>();
builder.Services.AddScoped<ICounterService, CounterService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();

//Add scope for repositories*
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMaterialProductRepository, MaterialProductRepository>();
builder.Services.AddScoped<ICounterRepository, CounterRepository>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();

//Add scope for DAOs
builder.Services.AddScoped<IGenericDAO<Customer>, GenericDAO<Customer>>();
builder.Services.AddScoped<IGenericDAO<Employee>, GenericDAO<Employee>>();
builder.Services.AddScoped<IGenericDAO<Product>, GenericDAO<Product>>();
builder.Services.AddScoped<IGenericDAO<MaterialProduct>, GenericDAO<MaterialProduct>>();
builder.Services.AddScoped<IGenericDAO<Counter>, GenericDAO<Counter>>();
builder.Services.AddScoped<IGenericDAO<Material>, GenericDAO<Material>>();

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

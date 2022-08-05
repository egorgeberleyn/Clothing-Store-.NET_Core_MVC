var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped(sp => SessionShopCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//DI
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    db.Database.EnsureCreated();
}   
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseAuthorization();


app.UseMvcWithDefaultRoute();
app.UseMvc(routes =>
{   
    routes.MapRoute(name: "categoryFilter", template: "{controller=Category}/{action=ProductList}/{category?}");
    routes.MapRoute(name: "shopCart", template: "{controller=ShopCart}/{action=Cart}");
    routes.MapRoute(name: "AddshopCart", template: "{controller=ShopCart}/{action=AddToCart}/{id}");
    routes.MapRoute(name: "DeleteshopCart", template: "{controller=ShopCart}/{action=DeleteFromCart}/{id}");
});

app.Run();

using AssignmentFinals.Hubs;
using AssignmentFinals.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// API controllers
builder.Services.AddControllers();

// SignalR
builder.Services.AddSignalR();

// XML Service (no database)
builder.Services.AddSingleton<ProductXmlService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

// MVC Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

// API Controllers
app.MapControllers();

// SignalR Hub
app.MapHub<InventoryHub>("/inventoryHub");

app.Run();
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using SocialMedia.Services;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add PostgreSQL support
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    "Host=34.142.237.224;Database=mydb;Username=root;Password=*2FyhT%#ZHkG+MJE;";

builder.Services.AddDbContext<SocialMediaContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<DatabaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Test the database connection
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SocialMediaContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    try
    {
        // Attempt to connect to the database
        context.Database.EnsureCreated();
        context.Database.Migrate();
        logger.LogInformation("Hello Database, connection was successful.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while connecting to the database.");
    }
}

app.Run();

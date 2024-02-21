using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SocialMedia.Models; // Adjust this to the correct namespace

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});


// Add PostgreSQL support
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    "Host=34.142.237.224;Database=mydb;Username=root;Password=*2FyhT%#ZHkG+MJE;";

builder.Services.AddDbContext<SocialMediaContext>((serviceProvider, options) =>
    options.UseNpgsql(connectionString)
           .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())); // Use the built-in logger

var app = builder.Build();

// Test the database connection
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SocialMediaContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    try
    {
        // Just an example to trigger a database operation
        if (context.Database.CanConnect())
        {
            logger.LogInformation("Hello Database");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while connecting to the database.");
        // Handle the error appropriately
    }
}


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

app.Run();

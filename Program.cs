using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Social_Media.Models; // Adjust this to the correct namespace

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

// Add PostgreSQL support
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    "Host=34.142.237.224;Database=mydb;Username=root;Password=*2FyhT%#ZHkG+MJE;";

builder.Services.AddDbContext<SocialMediaContext>(options =>
    options.UseNpgsql(connectionString));

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

app.Run();



using Microsoft.EntityFrameworkCore;
using SocialMedia.Models.Database;
using SocialMedia.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration["Jwt:Key"];

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
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<GroupmemberService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key != null ? new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)) : null,
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("JwtBearer");

                logger.LogError(context.Exception, "JWT authentication failed: " + context.Exception.Message);
                logger.LogInformation("Request URL: " + context.HttpContext.Request.Path);
                // Redirect to login page
                var cookies = context.HttpContext.Request.Cookies;
                foreach (var cookie in cookies)
                {
                    context.HttpContext.Response.Cookies.Delete(cookie.Key);
                }
                context.Response.Redirect("/User/Login");

                return Task.CompletedTask;
            }
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    if (context.Request.Headers.ContainsKey("Cookie") && context.Request.Headers["Cookie"].ToString().Contains("jwt="))
    {
        var cookie = context.Request.Headers["Cookie"].ToString();
        var token = cookie.Substring(cookie.IndexOf("jwt=") + 4);
        context.Request.Headers["Authorization"] = "Bearer " + token;
    }
    await next();
});

app.UseRouting();



app.UseAuthentication(); // Make sure to call this before UseAuthorization
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

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

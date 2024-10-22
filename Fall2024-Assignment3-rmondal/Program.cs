using Fall2024_Assignment3_rmondal.Models;
using Fall2024_Assignment3_rmondal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure the database connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity for user authentication and authorization
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add controllers and views support
builder.Services.AddControllersWithViews();

// Register OpenAIService for making API calls to OpenAI
builder.Services.AddScoped<OpenAIService>();

// Load OpenAI API key from configuration (appsettings.json or secrets)
builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAISettings"));

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enforces use of HTTPS in production
}

// Enable HTTPS redirection and static files middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure authentication middleware is added
app.UseAuthorization();

// Configure default routing for controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Enable Razor Pages for Identity UI
app.MapRazorPages();

app.Run();

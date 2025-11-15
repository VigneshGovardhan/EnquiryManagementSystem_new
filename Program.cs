using EnquiryManagementSystem.Data;
using EnquiryManagementSystem.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configuration (you can move connection string to appsettings.json if you prefer)
var configuration = builder.Configuration;

// 1) Register DbContext - using SQLite file 'ems.db' (adjust path if needed)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Use a file-based SQLite DB in app root (same file you already had)
    // If you want it in Data folder or a different path, change the Data Source accordingly.
    options.UseSqlite("Data Source=ems.db");
});

// 2) Register your application services
// Make sure the EnquiryService class exists in EnquiryManagementSystem.Services and is public
builder.Services.AddScoped<EnquiryService>();

// 3) Add Razor Pages + Server-Side Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// If your service needs other services (ILogger, IHttpClientFactory, etc.), register them here.

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Serve static files from wwwroot (important so /css/... works)
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host"); // fallback route to the _Host page

app.Run();

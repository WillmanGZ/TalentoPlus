using Microsoft.AspNetCore.Identity;
using RRHH.WEB.Configs;
using RRHH.WEB.Data;
using RRHH.WEB.Data.Seeders;
using RRHH.WEB.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase();

// Use identity user system and role system
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>() // Using our dbContext
    .AddDefaultTokenProviders() // Using tokens for reset or confirmation
    .AddDefaultUI(); // Use razor pages

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Apply migrations
app.ApplyMigrations<AppDbContext>();

// Use seed to create default admin + roles
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
await IdentitySeed.SeedAsync(services);

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();  
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();

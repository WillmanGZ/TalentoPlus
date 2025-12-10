using Microsoft.AspNetCore.Identity;
using TalentoPlus.API.Configs;
using TalentoPlus.API.Data;
using TalentoPlus.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase();

builder.Services.AddIdentity<Employee, IdentityRole>(options =>
{
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<Employee>>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
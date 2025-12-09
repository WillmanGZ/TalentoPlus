using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TalentoPlus.API.Data.Entities;

namespace TalentoPlus.API.Data
{
    public class AppDbContext : IdentityDbContext<Employee>
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}

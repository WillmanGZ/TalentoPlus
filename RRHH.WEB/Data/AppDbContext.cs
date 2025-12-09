using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RRHH.WEB.Data.Entities;

namespace RRHH.WEB.Data
{
    public class AppDbContext : IdentityDbContext<Employee>
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}

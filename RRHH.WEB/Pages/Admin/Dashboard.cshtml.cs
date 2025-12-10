using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RRHH.WEB.Data;
using RRHH.WEB.Data.Entities;

namespace RRHH.WEB.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public int TotalEmployees { get; set; }

        public DashboardModel(AppDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            int employeeCount = 0;

            foreach (var user in allUsers)
            {

                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Employee"))
                {
                    employeeCount++;
                }
            }

            TotalEmployees = employeeCount;
        }
    }
}
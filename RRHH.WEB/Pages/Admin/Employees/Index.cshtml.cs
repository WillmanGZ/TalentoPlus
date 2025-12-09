using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RRHH.WEB.Data.Entities;

namespace RRHH.WEB.Pages.Admin.Employees
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Employee> _userManager;

        public IndexModel(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public List<Employee> Employees { get; set; } = new();

        public async Task OnGetAsync()
        {
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                
                if (roles.Contains("Employee"))
                    Employees.Add(user);
            }
        }
    }
}
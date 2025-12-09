using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RRHH.WEB.Data.Entities;

namespace RRHH.WEB.Pages.Admin.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly UserManager<Employee> _userManager;

        public DetailsModel(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            Employee = await _userManager.FindByIdAsync(id);

            if (Employee == null)
                return NotFound();

            return Page();
        }
    }
}
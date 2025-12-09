using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RRHH.WEB.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WEB.Pages.Admin.Employees
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateModel(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required, EmailAddress]
            public string Email { get; set; }

            [Required]
            public string UserName { get; set; }

            [Required]
            public string PhoneNumber { get; set; }

            [Required]
            public string Position { get; set; }

            [Required]
            [Range(0, double.MaxValue)]
            public decimal Salary { get; set; }

            [Required]
            public string Department { get; set; }
            
            public string EducationLevel { get; set; }
            
            public string ProfessionalProfile { get; set; }

            [Required, DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var employee = new Employee
            {
                Email = Input.Email,
                UserName = Input.UserName,
                PhoneNumber = Input.PhoneNumber,
                Position = Input.Position,
                Salary = Input.Salary,
                Department = Input.Department,
                EducationLevel = Input.EducationLevel,
                ProfessionalProfile = Input.ProfessionalProfile,
                HireDate = DateTime.UtcNow,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(employee, Input.Password);

            if (result.Succeeded)
            {
                // Ensure role exists
                if (!await _roleManager.RoleExistsAsync("Employee"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Employee"));
                }

                await _userManager.AddToRoleAsync(employee, "Employee");

                return RedirectToPage("./Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}

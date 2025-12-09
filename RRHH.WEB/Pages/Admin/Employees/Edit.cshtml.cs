using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RRHH.WEB.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WEB.Pages.Admin.Employees
{
    public class EditModel : PageModel
    {
        private readonly UserManager<Employee> _userManager;

        public EditModel(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required]
            public string Id { get; set; }

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
        }

        // Carga los datos existentes del empleado
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            // Usamos tu entidad Employee
            var employee = await _userManager.FindByIdAsync(id); 

            if (employee == null) return NotFound();

            // Mapeamos los datos de Employee a InputModel
            Input = new InputModel
            {
                Id = employee.Id,
                Email = employee.Email,
                UserName = employee.UserName,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
                Salary = employee.Salary,
                Department = employee.Department,
                ProfessionalProfile = employee.ProfessionalProfile,
                EducationLevel = employee.EducationLevel
            };

            return Page();
        }

        // Maneja la actualización de los datos
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Buscamos el empleado por el Id oculto
            var employee = await _userManager.FindByIdAsync(Input.Id); 

            if (employee == null)
                return NotFound();

            // Aplicamos los cambios del InputModel al objeto Employee
            employee.Email = Input.Email;
            employee.UserName = Input.UserName;
            employee.PhoneNumber = Input.PhoneNumber;
            employee.Position = Input.Position;
            employee.Salary = Input.Salary;
            employee.Department = Input.Department;

            var result = await _userManager.UpdateAsync(employee);

            if (result.Succeeded)
                return RedirectToPage("./Index");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RRHH.WEB.Data.Entities; // Asumo que Employee está aquí

namespace RRHH.WEB.Pages.Admin.Employees
{
    public class DeleteModel : PageModel
    {
        // 🎯 Usamos UserManager tipado con la entidad Employee
        private readonly UserManager<Employee> _userManager; 

        public DeleteModel(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        // 🎯 La propiedad que se cargará y mostrará es de tipo Employee
        public Employee Employee { get; set; } 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            // Buscamos el empleado usando el UserManager<Employee>
            Employee = await _userManager.FindByIdAsync(id); 

            if (Employee == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null) return NotFound();

            // Buscamos el empleado a eliminar
            var employeeToDelete = await _userManager.FindByIdAsync(id);

            if (employeeToDelete != null)
            {
                // La operación de borrado la maneja el UserManager
                var result = await _userManager.DeleteAsync(employeeToDelete);
                
                if (result.Succeeded)
                {
                    // Redirigimos a la lista si tiene éxito
                    return RedirectToPage("./Index");
                }
                
                // Si falla por alguna razón (poco común en Delete, pero posible)
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                    
                // Si la eliminación falla, recargamos la página con los errores.
                // Es necesario cargar de nuevo los datos del empleado para que la vista funcione.
                Employee = employeeToDelete;
                return Page();
            }

            // Si no se encuentra, simplemente redirigimos o retornamos NotFound
            return NotFound(); 
        }
    }
}
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TalentoPlus.API.Data.Entities;

public class Employee : IdentityUser
{
    [Required]
    [StringLength(100, ErrorMessage = "Position cannot exceed 100 characters.")]
    public string Position { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = "Hire date is required.")]
    public DateTime HireDate { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    [StringLength(80, ErrorMessage = "Education level cannot exceed 80 characters.")]
    public string EducationLevel { get; set; }

    [StringLength(250, ErrorMessage = "Professional profile cannot exceed 250 characters.")]
    public string ProfessionalProfile { get; set; }

    [StringLength(80, ErrorMessage = "Department cannot exceed 80 characters.")]
    public string Department { get; set; }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RRHH.WEB.Data;

namespace RRHH.WEB.Controllers
{
    [Route("Admin/[controller]")]
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("DownloadCv/{id}")]
        public async Task<IActionResult> DownloadCv(string id)
        {
            var employee = await _context.Users
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) return NotFound("Empleado no encontrado");

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text($"Hoja de Vida: {employee.UserName}")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);


                            x.Item().Text("Datos Personales").Bold().FontSize(16).Underline();
                            x.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(150);
                                    columns.RelativeColumn();
                                });

                                table.Cell().Text("Nombre:").Bold();
                                table.Cell().Text($"{employee.UserName}");

                                table.Cell().Text("Email:").Bold();
                                table.Cell().Text(employee.Email);

                                table.Cell().Text("Teléfono:").Bold();
                                table.Cell().Text(employee.PhoneNumber ?? "N/A");
                            });

                            x.Item().Text("Información Laboral").Bold().FontSize(16).Underline();
                            x.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(150);
                                    columns.RelativeColumn();
                                });

                                table.Cell().Text("Departamento:").Bold();
                                table.Cell().Text(employee.Department ?? "Sin asignar");

                                table.Cell().Text("Salario:").Bold();
                                table.Cell().Text($"$ {employee.Salary:N2}");

                                table.Cell().Text("Fecha Ingreso:").Bold();
                                table.Cell().Text(employee.HireDate.ToShortDateString());
                            });

                            x.Item().Text("Perfil Profesional").Bold().FontSize(16).Underline();
                            x.Item().Border(1).BorderColor(Colors.Grey.Lighten1).Padding(5)
                                .Text(employee.ProfessionalProfile ?? "Sin descripción profesional registrada.");

                            x.Item().Text("Nivel Educativo").Bold().FontSize(16).Underline();
                            x.Item().Text(employee.EducationLevel ?? "No registrado");
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated automaticly by TalentoPlus - ");
                            x.CurrentPageNumber();
                        });
                });
            });

            byte[] pdfBytes = document.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"CV_{employee.UserName}.pdf");
        }
    }
}
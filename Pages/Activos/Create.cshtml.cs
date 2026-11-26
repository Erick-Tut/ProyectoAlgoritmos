using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoAlgoritmos.Data;
using ProyectoAlgoritmos.Models;

namespace ProyectoAlgoritmos.Pages.Activos;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Activo Activo { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Activo.NumeroFactura = Activo.NumeroFactura?.Trim().ToUpper() ?? string.Empty;
        Activo.Telefono = Activo.Telefono?.Trim() ?? string.Empty;

        if (Activo.FechaCompra != DateOnly.MinValue && Activo.AnosGarantia >= 0)
        {
            var vencimiento = Activo.FechaCompra.AddYears(Activo.AnosGarantia);

            if (Activo.AnosGarantia > 0)
            {
                vencimiento = vencimiento.AddDays(-1);
            }

            Activo.FechaVencimientoGarantia = vencimiento;
        }

        if (await _context.Activos.AnyAsync(a => a.CodigoActivo == Activo.CodigoActivo))
        {
            ModelState.AddModelError("Activo.CodigoActivo", "Este código de activo ya existe.");
        }

        if (await _context.Activos.AnyAsync(a => a.NumeroSerie == Activo.NumeroSerie))
        {
            ModelState.AddModelError("Activo.NumeroSerie", "Este número de serie ya existe.");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Activos.Add(Activo);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
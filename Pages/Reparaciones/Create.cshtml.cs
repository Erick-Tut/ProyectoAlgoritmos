
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoAlgoritmos.Data;
using ProyectoAlgoritmos.Models;

namespace ProyectoAlgoritmos.Pages.Reparaciones;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Reparacion Reparacion { get; set; } = new();

    public Activo? Activo { get; set; }

    public async Task<IActionResult> OnGetAsync(int activoId)
    {
        Activo = await _context.Activos
            .FirstOrDefaultAsync(a => a.Id == activoId && !a.Eliminado);

        if (Activo == null)
        {
            return NotFound();
        }

        Reparacion.ActivoId = Activo.Id;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int activoId)
    {
        Activo = await _context.Activos
            .FirstOrDefaultAsync(a => a.Id == activoId && !a.Eliminado);

        if (Activo == null)
        {
            return NotFound();
        }

        Reparacion.ActivoId = Activo.Id;
        Reparacion.Eliminado = false;

        ValidarFechaReparacion();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Reparaciones.Add(Reparacion);

        await _context.SaveChangesAsync();

        return RedirectToPage(
            "/Activos/Detalles",
            new { id = Activo.Id });
    }

    private void ValidarFechaReparacion()
    {
        if (Activo == null ||
            Reparacion.FechaReparacion == DateOnly.MinValue)
        {
            return;
        }

        if (Reparacion.FechaReparacion < Activo.FechaIngreso)
        {
            ModelState.AddModelError(
                "Reparacion.FechaReparacion",
                "La fecha de reparación no puede ser anterior a la fecha de ingreso del activo.");
        }

        if (Reparacion.FechaReparacion < Activo.FechaCompra)
        {
            ModelState.AddModelError(
                "Reparacion.FechaReparacion",
                "La fecha de reparación no puede ser anterior a la fecha de compra del activo.");
        }
    }
}

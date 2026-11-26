
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoAlgoritmos.Data;
using ProyectoAlgoritmos.Models;

namespace ProyectoAlgoritmos.Pages.Reparaciones;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Reparacion Reparacion { get; set; } = new();

    public Activo? Activo { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var reparacion = await _context.Reparaciones
            .Include(r => r.Activo)
            .FirstOrDefaultAsync(r =>
                r.Id == id &&
                !r.Eliminado &&
                !r.Activo.Eliminado);

        if (reparacion == null)
        {
            return NotFound();
        }

        Reparacion = reparacion;
        Activo = reparacion.Activo;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var existente = await _context.Reparaciones
            .Include(r => r.Activo)
            .FirstOrDefaultAsync(r =>
                r.Id == id &&
                !r.Eliminado &&
                !r.Activo.Eliminado);

        if (existente == null)
        {
            return NotFound();
        }

        Activo = existente.Activo;

        // El vínculo con el activo no se modifica desde el formulario.
        Reparacion.Id = existente.Id;
        Reparacion.ActivoId = existente.ActivoId;
        Reparacion.Activo = existente.Activo;
        Reparacion.Eliminado = existente.Eliminado;

        if (Reparacion.FechaReparacion != DateOnly.MinValue)
        {
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

        if (!ModelState.IsValid)
        {
            return Page();
        }

        existente.FechaReparacion = Reparacion.FechaReparacion;
        existente.Diagnostico = Reparacion.Diagnostico;
        existente.Proveedor = Reparacion.Proveedor;
        existente.Factura = Reparacion.Factura;
        existente.FechaFactura = Reparacion.FechaFactura;
        existente.Monto = Reparacion.Monto;
        existente.FechaEntregaUsuario = Reparacion.FechaEntregaUsuario;

        await _context.SaveChangesAsync();

        return RedirectToPage(
            "/Activos/Detalles",
            new { id = existente.ActivoId });
    }
}

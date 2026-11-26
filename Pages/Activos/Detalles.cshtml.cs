
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoAlgoritmos.Data;
using ProyectoAlgoritmos.Models;

namespace ProyectoAlgoritmos.Pages.Activos;

public class DetallesModel : PageModel
{
    private readonly AppDbContext _context;

    public DetallesModel(AppDbContext context)
    {
        _context = context;
    }

    public Activo Activo { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var activo = await _context.Activos
            .AsNoTracking()
            .Include(a => a.Reparaciones.Where(r => !r.Eliminado))
            .FirstOrDefaultAsync(a => a.Id == id && !a.Eliminado);

        if (activo == null)
        {
            return NotFound();
        }

        Activo = activo;

        return Page();
    }

    public async Task<IActionResult> OnPostEliminarReparacionAsync(
        int id, int reparacionId)
    {
        var activo = await _context.Activos
            .FirstOrDefaultAsync(a => a.Id == id && !a.Eliminado);

        if (activo == null)
        {
            return NotFound();
        }

        var reparacion = await _context.Reparaciones
            .FirstOrDefaultAsync(r =>
                r.Id == reparacionId &&
                r.ActivoId == id &&
                !r.Eliminado);

        if (reparacion == null)
        {
            return NotFound();
        }

        reparacion.Eliminado = true;

        await _context.SaveChangesAsync();

        return RedirectToPage(
            "/Activos/Detalles",
            new { id = activo.Id }
        );
    }
}

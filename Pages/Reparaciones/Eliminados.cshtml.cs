
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoAlgoritmos.Data;
using ProyectoAlgoritmos.Models;

namespace ProyectoAlgoritmos.Pages.Reparaciones;

public class EliminadosModel : PageModel
{
    private readonly AppDbContext _context;

    public EliminadosModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<Reparacion> Reparaciones { get; set; }
        = new List<Reparacion>();

    public async Task OnGetAsync()
    {
        Reparaciones = await _context.Reparaciones
            .AsNoTracking()
            .Include(r => r.Activo)
            .Where(r => r.Eliminado)
            .OrderByDescending(r => r.FechaReparacion)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostRestaurarAsync(int id)
    {
        var reparacion = await _context.Reparaciones
            .FirstOrDefaultAsync(r => r.Id == id && r.Eliminado);

        if (reparacion == null)
        {
            return NotFound();
        }

        reparacion.Eliminado = false;

        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEliminarDefinitivamenteAsync(int id)
    {
        var reparacion = await _context.Reparaciones
            .FirstOrDefaultAsync(r => r.Id == id && r.Eliminado);

        if (reparacion == null)
        {
            return NotFound();
        }

        _context.Reparaciones.Remove(reparacion);

        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}

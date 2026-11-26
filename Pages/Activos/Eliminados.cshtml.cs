using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoAlgoritmos.Data;
using ProyectoAlgoritmos.Models;

namespace ProyectoAlgoritmos.Pages.Activos;

public class EliminadosModel : PageModel
{
    private readonly AppDbContext _context;

    public EliminadosModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<Activo> Activos { get; set; } = new List<Activo>();

    public async Task OnGetAsync()
    {
        Activos = await _context.Activos
            .AsNoTracking()
            .Where(a => a.Eliminado)
            .OrderBy(a => a.CodigoActivo)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostRestaurarAsync(int id)
    {
        var activo = await _context.Activos
            .FirstOrDefaultAsync(a => a.Id == id && a.Eliminado);

        if (activo == null)
        {
            return NotFound();
        }

        activo.Eliminado = false;

        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEliminarDefinitivamenteAsync(int id)
    {
        var activo = await _context.Activos
            .FirstOrDefaultAsync(a => a.Id == id && a.Eliminado);

        if (activo == null)
        {
            return NotFound();
        }

        _context.Activos.Remove(activo);

        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}
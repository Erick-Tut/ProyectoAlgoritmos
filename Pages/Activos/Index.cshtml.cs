using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoAlgoritmos.Data;
using ProyectoAlgoritmos.Models;

namespace ProyectoAlgoritmos.Pages.Activos;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<Activo> Activos { get; set; } = new List<Activo>();

    public IList<Reparacion> Reparaciones { get; set; } = new List<Reparacion>();

    [BindProperty(SupportsGet = true)]
    public string Vista { get; set; } = "activos";

    [BindProperty(SupportsGet = true)]
    public string? Buscar { get; set; }

    public int TotalActivos { get; set; }

    public int TotalReparaciones { get; set; }

    public async Task OnGetAsync()
    {
        Vista = Vista?.ToLowerInvariant() == "reparaciones"
            ? "reparaciones"
            : "activos";

        var activosQuery = _context.Activos
            .AsNoTracking()
            .Where(a => !a.Eliminado);

        var reparacionesQuery = _context.Reparaciones
            .AsNoTracking()
            .Include(r => r.Activo)
            .Where(r => !r.Eliminado && !r.Activo.Eliminado);

        TotalActivos = await activosQuery.CountAsync();
        TotalReparaciones = await reparacionesQuery.CountAsync();

        if (!string.IsNullOrWhiteSpace(Buscar))
        {
            var termino = Buscar.Trim();

            activosQuery = activosQuery.Where(a =>
                a.CodigoActivo.Contains(termino) ||
                a.Marca.Contains(termino) ||
                a.Modelo.Contains(termino) ||
                a.NumeroSerie.Contains(termino));

            reparacionesQuery = reparacionesQuery.Where(r =>
                r.Activo.CodigoActivo.Contains(termino) ||
                r.Activo.Marca.Contains(termino) ||
                r.Activo.Modelo.Contains(termino) ||
                r.Diagnostico.Contains(termino));
        }

        Activos = await activosQuery
            .OrderBy(a => a.CodigoActivo)
            .ToListAsync();

        Reparaciones = await reparacionesQuery
            .OrderByDescending(r => r.FechaReparacion)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        var activo = await _context.Activos
            .FirstOrDefaultAsync(a => a.Id == id && !a.Eliminado);

        if (activo == null)
        {
            return NotFound();
        }

        activo.Eliminado = true;

        await _context.SaveChangesAsync();

        return RedirectToPage(new { vista = "activos" });
    }
}
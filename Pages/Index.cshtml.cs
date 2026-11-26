
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoAlgoritmos.Data;
using ProyectoAlgoritmos.Models;

namespace ProyectoAlgoritmos.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public int TotalActivos { get; set; }

    public int TotalReparaciones { get; set; }

    public int GarantiasPorVencer { get; set; }

    public int GarantiasVigentes { get; set; }

    public IList<Activo> ActivosRecientes { get; set; } = new List<Activo>();

    public IList<Reparacion> ReparacionesRecientes { get; set; } = new List<Reparacion>();

    public async Task OnGetAsync()
    {
        var hoy = DateOnly.FromDateTime(DateTime.Today);
        var limiteGarantia = hoy.AddDays(30);

        var activos = _context.Activos
            .AsNoTracking()
            .Where(a => !a.Eliminado);

        TotalActivos = await activos.CountAsync();

        GarantiasVigentes = await activos
            .CountAsync(a => a.FechaVencimientoGarantia >= hoy);

        GarantiasPorVencer = await activos
            .CountAsync(a =>
                a.FechaVencimientoGarantia >= hoy &&
                a.FechaVencimientoGarantia <= limiteGarantia);

        var reparaciones = _context.Reparaciones
            .AsNoTracking()
            .Where(r => !r.Eliminado && !r.Activo.Eliminado);

        TotalReparaciones = await reparaciones.CountAsync();

        ActivosRecientes = await activos
            .OrderByDescending(a => a.FechaIngreso)
            .Take(5)
            .ToListAsync();

        ReparacionesRecientes = await reparaciones
            .Include(r => r.Activo)
            .OrderByDescending(r => r.FechaReparacion)
            .Take(5)
            .ToListAsync();
    }
}

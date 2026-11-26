using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoAlgoritmos.Data;
using ProyectoAlgoritmos.Models;

namespace ProyectoAlgoritmos.Pages.Activos;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Activo Activo { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var activo = await _context.Activos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id && !a.Eliminado);

        if (activo == null)
        {
            return NotFound();
        }

        Activo = activo;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var activoExistente = await _context.Activos
            .FirstOrDefaultAsync(a => a.Id == id && !a.Eliminado);

        if (activoExistente == null)
        {
            return NotFound();
        }

        var numero = (Activo.NumeroFactura ?? "")
            .Replace("FAC-", "")
            .Trim();

        if (System.Text.RegularExpressions.Regex.IsMatch(numero, @"^\d{5}$"))
        {
            Activo.NumeroFactura = $"FAC-{numero}";
            ModelState.Remove("Activo.NumeroFactura");
        }
        else
        {
            ModelState.AddModelError(
                "Activo.NumeroFactura",
                "Ingresa exactamente 5 dígitos."
            );
        }

        if (!string.IsNullOrWhiteSpace(Activo.Telefono))
        {
            var numeros = Activo.Telefono.Replace("-", "").Trim();

            if (numeros.Length == 8 && numeros.All(char.IsDigit))
            {
                Activo.Telefono = $"{numeros[..4]}-{numeros[4..]}";
                ModelState.Remove("Activo.Telefono");
            }
        }

        if (Activo.FechaCompra != DateOnly.MinValue &&
            Activo.AnosGarantia >= 0)
        {
            if ((long)Activo.FechaCompra.Year + Activo.AnosGarantia <= 9999)
            {
                Activo.FechaVencimientoGarantia =
                    Activo.FechaCompra.AddYears(Activo.AnosGarantia).AddDays(-1);

                ModelState.Remove("Activo.FechaVencimientoGarantia");
            }
            else
            {
                ModelState.AddModelError(
                    "Activo.AnosGarantia",
                    "Valor inválido."
                );
            }
        }

        if (!string.IsNullOrWhiteSpace(Activo.CodigoActivo) &&
            await _context.Activos.AnyAsync(a =>
                a.CodigoActivo == Activo.CodigoActivo && a.Id != id))
        {
            ModelState.AddModelError(
                "Activo.CodigoActivo",
                "Código duplicado."
            );
        }

        if (!string.IsNullOrWhiteSpace(Activo.NumeroSerie) &&
            await _context.Activos.AnyAsync(a =>
                a.NumeroSerie == Activo.NumeroSerie && a.Id != id))
        {
            ModelState.AddModelError(
                "Activo.NumeroSerie",
                "Número de serie duplicado."
            );
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        activoExistente.CodigoActivo = Activo.CodigoActivo;
        activoExistente.FechaIngreso = Activo.FechaIngreso;
        activoExistente.Marca = Activo.Marca;
        activoExistente.Modelo = Activo.Modelo;
        activoExistente.NumeroSerie = Activo.NumeroSerie;
        activoExistente.FechaCompra = Activo.FechaCompra;
        activoExistente.AnosGarantia = Activo.AnosGarantia;
        activoExistente.NumeroFactura = Activo.NumeroFactura ?? string.Empty;
        activoExistente.Monto = Activo.Monto;
        activoExistente.Proveedor = Activo.Proveedor;
        activoExistente.Direccion = Activo.Direccion;
        activoExistente.PersonaContacto = Activo.PersonaContacto;
        activoExistente.Telefono = Activo.Telefono;
        activoExistente.FechaVencimientoGarantia = Activo.FechaVencimientoGarantia;

        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProyectoAlgoritmos.Models;

public class Reparacion : IValidatableObject
{
    public int Id { get; set; }

    public bool Eliminado { get; set; } = false;

    public int ActivoId { get; set; }

    [ValidateNever]
    public Activo Activo { get; set; } = null!;

    [Required(ErrorMessage = "Campo obligatorio.")]
    public DateOnly FechaReparacion { get; set; }

    [Required(ErrorMessage = "Campo obligatorio.")]
    [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
    public string Diagnostico { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
    public string Proveedor { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string Factura { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    public DateOnly FechaFactura { get; set; }

    [Range(0.0, double.MaxValue,
        ErrorMessage = "El monto no puede ser negativo.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "Campo obligatorio.")]
    public DateOnly FechaEntregaUsuario { get; set; }

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (FechaReparacion == DateOnly.MinValue)
        {
            yield return new ValidationResult(
                "Selecciona una fecha válida.",
                [nameof(FechaReparacion)]);
        }

        if (FechaFactura == DateOnly.MinValue)
        {
            yield return new ValidationResult(
                "Selecciona una fecha válida.",
                [nameof(FechaFactura)]);
        }

        if (FechaEntregaUsuario == DateOnly.MinValue)
        {
            yield return new ValidationResult(
                "Selecciona una fecha válida.",
                [nameof(FechaEntregaUsuario)]);
        }

        if (FechaReparacion != DateOnly.MinValue &&
            FechaFactura != DateOnly.MinValue &&
            FechaFactura < FechaReparacion)
        {
            yield return new ValidationResult(
                "La fecha de factura no puede ser anterior a la reparación.",
                [nameof(FechaFactura)]);
        }

        if (FechaReparacion != DateOnly.MinValue &&
            FechaEntregaUsuario != DateOnly.MinValue &&
            FechaEntregaUsuario < FechaReparacion)
        {
            yield return new ValidationResult(
                "La fecha de entrega no puede ser anterior a la reparación.",
                [nameof(FechaEntregaUsuario)]);
        }

        if (decimal.Round(Monto, 2) != Monto)
        {
            yield return new ValidationResult(
                "Máximo 2 decimales.",
                [nameof(Monto)]);
        }
    }
}

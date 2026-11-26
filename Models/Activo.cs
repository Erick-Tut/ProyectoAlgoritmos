
using System.ComponentModel.DataAnnotations;

namespace ProyectoAlgoritmos.Models;

public class Activo : IValidatableObject
{
    public int Id { get; set; }

    public bool Eliminado { get; set; } = false;

    [Required(ErrorMessage = "Campo obligatorio.")]
    [RegularExpression(@"^\d{5}-\d{2}-\d{4}$", ErrorMessage = "Código inválido.")]
    [StringLength(13)]
    public string CodigoActivo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    public DateOnly FechaIngreso { get; set; }

    [Required(ErrorMessage = "Campo obligatorio.")]
    [StringLength(100)]
    public string Marca { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    [StringLength(100)]
    public string Modelo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    [StringLength(100)]
    public string NumeroSerie { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    public DateOnly FechaCompra { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Valor inválido.")]
    public int AnosGarantia { get; set; }

    [Required(ErrorMessage = "Campo obligatorio.")]
    [RegularExpression(@"^FAC-\d{5}$", ErrorMessage = "Factura inválida.")]
    [StringLength(9)]
    public string NumeroFactura { get; set; } = string.Empty;

    [Range(0.0, double.MaxValue, ErrorMessage = "Monto inválido.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "Campo obligatorio.")]
    [StringLength(200)]
    public string Proveedor { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    [StringLength(300)]
    public string Direccion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    [StringLength(200)]
    public string PersonaContacto { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    [RegularExpression(@"^\d{4}-?\d{4}$", ErrorMessage = "Teléfono inválido.")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obligatorio.")]
    public DateOnly FechaVencimientoGarantia { get; set; }

    public ICollection<Reparacion> Reparaciones { get; set; } = new List<Reparacion>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FechaIngreso == DateOnly.MinValue)
        {
            yield return new ValidationResult(
                "Fecha inválida.",
                [nameof(FechaIngreso)]);
        }

        if (FechaCompra == DateOnly.MinValue)
        {
            yield return new ValidationResult(
                "Fecha inválida.",
                [nameof(FechaCompra)]);
        }

        if (FechaVencimientoGarantia == DateOnly.MinValue)
        {
            yield return new ValidationResult(
                "Fecha inválida.",
                [nameof(FechaVencimientoGarantia)]);
        }

        if (FechaIngreso != DateOnly.MinValue &&
            FechaCompra != DateOnly.MinValue &&
            FechaCompra < FechaIngreso)
        {
            yield return new ValidationResult(
                "La compra no puede ser anterior al ingreso.",
                [nameof(FechaCompra)]);
        }

        if (FechaCompra != DateOnly.MinValue &&
            FechaVencimientoGarantia != DateOnly.MinValue &&
            FechaVencimientoGarantia < FechaCompra)
        {
            yield return new ValidationResult(
                "Fecha de vencimiento inválida.",
                [nameof(FechaVencimientoGarantia)]);
        }

        if (decimal.Round(Monto, 2) != Monto)
        {
            yield return new ValidationResult(
                "Máximo 2 decimales.",
                [nameof(Monto)]);
        }
    }
}

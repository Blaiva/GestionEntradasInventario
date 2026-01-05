using System.ComponentModel.DataAnnotations;

namespace GestionEntradasInventario.Models;

public class EntradaDetalles
{
    [Key]
    public int DetalleId { get; set; }

    public int EntradaId { get; set; }

    public int ProductoId { get; set; }

    [Required(ErrorMessage = "La cantidad es requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero")]
    public int Cantdiad { get; set; }

    [Required(ErrorMessage = "El costo es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor que cero")]
    public double Costo { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace GestionEntradasInventario.Models;

public class Productos
{
    [Key]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "La descripcion es requerida")]
    public string Descripcion {  get; set; }

    [Required(ErrorMessage = "El costo es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor que cero")]
    public double Costo { get; set; }

    [Required(ErrorMessage = "El precio es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero")]
    public double Precio { get; set; }

    public int Existencia { get; set; } = 0;
}

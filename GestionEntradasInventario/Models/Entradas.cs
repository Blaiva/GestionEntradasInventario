using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEntradasInventario.Models;

public class Entradas
{
    [Key]
    public int EntradaId { get; set; }

    [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El concepto es requerido")]
    public string Concepto { get; set; }

    [Required(ErrorMessage = "El total es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor que 0")]
    public double Total { get; set; }

    [ForeignKey(nameof(EntradaId))]
    public virtual ICollection<EntradaDetalles> EntradaDetalles { get; set; } = new List<EntradaDetalles>(); 
}

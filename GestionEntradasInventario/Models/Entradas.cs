using System.ComponentModel.DataAnnotations;

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
    public double Total { get; set; }
}

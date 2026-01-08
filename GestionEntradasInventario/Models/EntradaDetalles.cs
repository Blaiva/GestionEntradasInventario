using System.ComponentModel.DataAnnotations;

namespace GestionEntradasInventario.Models;

public class EntradaDetalles
{
    [Key]
    public int DetalleId { get; set; }

    public int EntradaId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public double Costo { get; set; }
}

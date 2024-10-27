using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models.Detalles;

public class CotizacionesDetalle
{
    [Key]
    public int DetalleId { get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [ForeignKey("Cotizaciones")]
    public int CotizacionId { get; set; }
    public Cotizaciones? Cotizacion { get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [ForeignKey("Articulos")]
    public int ArticuloId { get; set; }
    public Articulos? Articulos { get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int Cantidad { get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public double Precio { get; set; }
}

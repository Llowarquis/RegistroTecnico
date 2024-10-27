using RegistroTecnico.Models.Detalles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

public class Cotizaciones
{
    [Key]
    public int CotizacionId { get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public DateTime Fecha { get; set; } = DateTime.Now;


    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [ForeignKey("Clientes")]
    public int ClienteId { get; set; }
    public Clientes? Cliente { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(256, MinimumLength = 0, ErrorMessage = "El limite de caracteres es 256.")]
    public string? Observacion { get; set; }


    [Required(ErrorMessage = "Este campo es obligatirio.")]
    [Range(minimum: 0, maximum: 1000000, ErrorMessage = "El monto no puede ser > que 1,000,000.00")]
    public double Monto { get; set; }


    [ForeignKey("CotizacionId")]
    public ICollection<CotizacionesDetalle> CotizacionDetalle { get; set; } = new List<CotizacionesDetalle>();
}

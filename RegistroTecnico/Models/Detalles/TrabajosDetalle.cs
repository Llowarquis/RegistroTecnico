using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models.Detalles;

public class TrabajosDetalle
{
	[Key]
	public int DetalleId { get; set; }


	[Required(ErrorMessage = "Este campo es obligatorio.")]
    [ForeignKey("Trabajos")]
    public int TrabajosId { get; set; }
	public Trabajos? Trabajos { get; set; }


	[Required(ErrorMessage = "Este campo es obligatorio.")]
	[ForeignKey("Articulos")]
	public int ArticuloId { get; set; }
	public Articulos? Articulo { get; set; }


	[Required(ErrorMessage = "Este campo es obligatorio.")]
	public int Cantidad { get; set; }


	[Required(ErrorMessage = "Este campo es obligatorio.")]
	public double Precio { get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public double Costo { get; set; } // Este campo estara oculto a la vista del usuario
}

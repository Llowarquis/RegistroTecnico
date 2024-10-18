using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models.Detalles;

public class TrabajosDetalle
{
	[Key]
	public int DetalleId { get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public Trabajos? TrabajoId { get; set; }


	[Required(ErrorMessage = "Este campo es obligatorio.")]
	public Articulos? ArticuloId { get; set; }


	[Required(ErrorMessage = "Este campo es obligatorio.")]
	public int Cantidad { get; set; }


	[Required(ErrorMessage = "Este campo es obligatorio.")]
	public double Precio { get; set; }


	public double Costo { get; set; } // Este campo estara oculto a la vista del usuario
}

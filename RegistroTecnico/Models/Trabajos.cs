using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;
public class Trabajos
{
	[Key]
	public int TrabajosId { get; set; }


	[Required(ErrorMessage = "Este campo es obligatorio.")] 
	public DateTime Fecha { get; set; } = DateTime.Now;


	//Esta Descripcion es el problema en detalla que tiene el cliente
	[Required(ErrorMessage = "Este campo es obligatorio.")]
	[RegularExpression(@"^[a-zA-Z-ÁáÉéÍíÓóÚúÑñ\s]+$", ErrorMessage = "Este campo solo puede alojar letras/espacios.")]
	public string? Descripcion { get; set; }


	[Required(ErrorMessage = "Este campo es obligatorio.")]
	[RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Este campo solo puede alojar numeros > 0.")]
	public double Monto { get; set; }



	[ForeignKey("Cliente")]
	public int ClienteId { get; set; }
	public Clientes? Cliente { get; set; }


	[ForeignKey("Tecnico")]
	public int TecnicoId { get; set; }
	public Tecnicos? Tecnico { get; set; }
}

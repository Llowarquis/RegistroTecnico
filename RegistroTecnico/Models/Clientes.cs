using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;
public class Clientes
{
	[Key]
	public int ClienteId { get; set; }


	[RegularExpression(@"^[a-zA-Z-ÁáÉéÍíÓóÚúÑñ\s]+$", ErrorMessage = "Este campo solo puede alojar letras/espacios.")]
	[Required(ErrorMessage = "Este campo es obligatorio.")]
	public string? Nombres { get; set; }


	[RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Este campo solo puede alojar numeros.")]
	[Required(ErrorMessage = "Este campo es obligatorio.")]
	public string? WhatsApp { get; set; }
}

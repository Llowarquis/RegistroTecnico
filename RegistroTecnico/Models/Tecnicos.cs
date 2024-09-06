using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;
public class Tecnicos
{
    // prueba
    [Key]
    public int TecnicoId { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Nombres { get; set; }
    
    public double SueldoHora { get; set; }
}

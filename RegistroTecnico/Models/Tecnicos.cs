using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;
public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }
    
    public string? Nombres { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public double SueldoHora { get; set; }
}

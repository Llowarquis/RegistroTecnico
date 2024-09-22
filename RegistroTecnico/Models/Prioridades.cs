using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;
public class Prioridades
{
    [Key]
    public int PrioridadesId {  get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int Tiempo { get; set; }
}

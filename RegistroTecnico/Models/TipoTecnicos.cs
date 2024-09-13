using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;
public class TipoTecnicos
{
    [Key]
    [ForeignKey("TipoTecnicos")]
    public int TipoTecnicoId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Descripcion { get; set; }
}

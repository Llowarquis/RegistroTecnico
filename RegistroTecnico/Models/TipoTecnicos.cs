using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;
public class TipoTecnicos
{
    [Key]
    public int TipoTencicoId { get; set; }
    [Required]
    public string? Descripcion { get; set; }
}

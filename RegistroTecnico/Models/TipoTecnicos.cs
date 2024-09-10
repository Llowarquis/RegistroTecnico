using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;
public class TipoTecnicos
{
    [Key]
    [ForeignKey("TipoTecnicos")]
    public int TipoTencicoId { get; set; }
    public string? Descripcion { get; set; }
}

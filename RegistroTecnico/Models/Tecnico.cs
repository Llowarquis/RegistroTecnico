using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{
    public class Tecnico
    {
        [Key]
        private int tecnicoId { get; set; }
        private string? nombre { get; set; }
        private double sueldoHora { get; set; }
    }
}

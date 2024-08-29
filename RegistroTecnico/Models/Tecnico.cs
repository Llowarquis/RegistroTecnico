using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{
    public class Tecnico
    {
        [Key]
        public int tecnicoId { get; set; }
        public string? nombre { get; set; }
        public double sueldoHora { get; set; }
    }
}

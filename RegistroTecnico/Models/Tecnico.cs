using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{
    public class Tecnico
    {
        [Key]
        public int tecnicoId { get; set; }
        private string? nombre { get; set; }
        private double sueldoHora { get; set; }
    }
}

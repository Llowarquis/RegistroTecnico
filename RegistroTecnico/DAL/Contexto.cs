using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;

namespace RegistroTecnico.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) 
            : base(options) { }

        public DbSet<Tecnico> Tecnicos { get; set; }
    }
}

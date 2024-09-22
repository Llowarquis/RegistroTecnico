using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;

namespace RegistroTecnico.DAL;
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) 
        : base(options) { }

    public DbSet<Tecnicos> Tecnicos { get; set; }
    public DbSet<TipoTecnicos> TipoTecnicos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Trabajos> Trabajos { get; set; }
    public DbSet<Prioridades> Prioridades { get; set; }
}

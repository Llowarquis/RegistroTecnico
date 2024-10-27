using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;
using RegistroTecnico.Models.Detalles;

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
    public DbSet<Articulos> Articulos { get; set; }
    public DbSet<TrabajosDetalle> TrabajosDetalles { get; set; }
    public DbSet<Cotizaciones> Cotizaciones { get; set; }
    public DbSet<CotizacionesDetalle> CotizacionesDetalles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Articulos>().HasData(new List<Articulos>()
        {
            new Articulos() { ArticuloId = 1, Descripcion = "Memoria RAM", Existencia = 51, Precio = 4999.99, Costo = 2999.99},
            new Articulos() { ArticuloId = 2, Descripcion = "MotherBoard", Existencia = 64, Precio = 7999.99, Costo = 3999.99},
            new Articulos() { ArticuloId = 3, Descripcion = "Cables UTP Cat6 (500ft)", Existencia = 85, Precio = 2549.99, Costo = 959.99},
            new Articulos() { ArticuloId = 4, Descripcion = "Power Supply", Existencia = 71, Precio = 3699.99, Costo = 1599.99},
            new Articulos() { ArticuloId = 5, Descripcion = "Cpu", Existencia = 84, Precio = 9599.50, Costo = 4769.80}
        });
    }
}

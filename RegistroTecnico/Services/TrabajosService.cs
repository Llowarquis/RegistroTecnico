using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TrabajosService(Contexto contexto)
{
    // Se hace uso del primary constructor
    private readonly Contexto _contexto = contexto;

    // Este metodo se utilizara en a la hora de modificar o insertar un empleado
    // ambas pantallas presentaran un boton tipo guardar
    public async Task<bool> Guardar(Trabajos trabajo)
    {
        if (!await Existe(trabajo.TrabajosId))
            return await Insertar(trabajo);
        else
            return await Modificar(trabajo);
    }

    private async Task<bool> Existe(int id)
    {
        return await _contexto.Trabajos
            .AnyAsync<Trabajos>(tra => tra.TrabajosId == id);
    }

    private async Task<bool> Insertar(Trabajos trabajo)
    {
        _contexto.Trabajos.Add(trabajo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Trabajos trabajo)
    {
        _contexto.Trabajos.Update(trabajo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        return await _contexto.Trabajos
            .Where(t => t.TrabajosId == id)
            .AsNoTracking()
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<Trabajos?> Buscar(int id)
    {
        return await _contexto.Trabajos
            .Include(t => t.Tecnico)
            .Include(c => c.Cliente)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TrabajosId == id);
    }

    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return await _contexto.Trabajos
            .Include(t => t.Tecnico)
            .Include(c => c.Cliente)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExisteDescripcion( string? descripcion)
    {
        return await _contexto?.Trabajos
            .AnyAsync(e => e.Descripcion.ToLower().Equals(descripcion.ToLower()));
    }
}

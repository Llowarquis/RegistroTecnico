using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;
public class PrioridadesService(Contexto contexto)
{
    // Se hace uso del primary constructor
    private readonly Contexto _contexto = contexto;

    // Este metodo se utilizara en a la hora de modificar o insertar un empleado
    // ambas pantallas presentaran un boton tipo guardar
    public async Task<bool> Guardar(Prioridades prioridad)
    {
        if (!await Existe(prioridad.PrioridadesId))
            return await Insertar(prioridad);
        else
            return await Modificar(prioridad);
    }

    private async Task<bool> Existe(int id)
    {
        return await _contexto.Prioridades
            .AnyAsync<Prioridades>(t => t.PrioridadesId == id);
    }

    private async Task<bool> Insertar(Prioridades prioridad)
    {
        _contexto.Prioridades.Add(prioridad);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Prioridades prioridad)
    {
        _contexto.Prioridades.Update(prioridad);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        return await _contexto.Prioridades
            .Where(t => t.PrioridadesId == id)
            .AsNoTracking()
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<Prioridades?> Buscar(int id)
    {
        return await _contexto.Prioridades
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.PrioridadesId == id);
    }

    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
    {
        return await _contexto.Prioridades
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExisteDescripcion(string? descripcion)
    {
        return await _contexto?.Prioridades
            .AnyAsync(e => e.Descripcion.ToLower().Equals(descripcion.ToLower()));
    }
}

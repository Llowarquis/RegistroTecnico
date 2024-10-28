using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;
public class PrioridadesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Prioridades prioridad)
    {
        if (!await Existe(prioridad.PrioridadesId))
            return await Insertar(prioridad);
        else
            return await Modificar(prioridad);
    }

    private async Task<bool> Existe(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Prioridades
            .AnyAsync<Prioridades>(t => t.PrioridadesId == id);
    }

    private async Task<bool> Insertar(Prioridades prioridad)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		_contexto.Prioridades.Add(prioridad);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Prioridades prioridad)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		_contexto.Prioridades.Update(prioridad);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Prioridades
            .Where(t => t.PrioridadesId == id)
            .AsNoTracking()
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<Prioridades?> Buscar(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Prioridades
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.PrioridadesId == id);
    }

    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Prioridades
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExisteDescripcion(string? descripcion)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Prioridades
            .AnyAsync(e => e.Descripcion.ToLower().Equals(descripcion.ToLower()));
    }
}

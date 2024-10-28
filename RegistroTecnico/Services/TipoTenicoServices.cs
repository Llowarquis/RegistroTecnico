using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;
public class TipoTecnicoServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(TipoTecnicos tipoTecnico)
    {
        if (!await Existe(tipoTecnico.TipoTecnicoId))
            return await Insertar(tipoTecnico);
        else
            return await Modificar(tipoTecnico);
    }

    private async Task<bool> Existe(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.TipoTecnicos
            .AnyAsync<TipoTecnicos>(t => t.TipoTecnicoId == id);
    }

    private async Task<bool> Insertar(TipoTecnicos tipoTecnico)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		_contexto.TipoTecnicos.Add(tipoTecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TipoTecnicos tipoTecnico)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		_contexto.Update(tipoTecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.TipoTecnicos
            .Where(t => t.TipoTecnicoId == id)
            .AsNoTracking()
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<TipoTecnicos?> Buscar(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.TipoTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoTecnicoId == id);
    }

    public async Task<List<TipoTecnicos>> Listar(Expression<Func<TipoTecnicos, bool>> criterio)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.TipoTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ValidarDescripcion(string? name)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.TipoTecnicos
            .AnyAsync<TipoTecnicos>(t => t.Descripcion.ToLower() == name.ToLower());
    }
}
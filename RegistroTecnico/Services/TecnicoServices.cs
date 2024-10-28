using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TecnicoServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if(!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    private async Task<bool> Existe(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Tecnicos
            .AnyAsync<Tecnicos>(t => t.TecnicoId == id);
    }

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		_contexto.Tecnicos.Add(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		_contexto.Update(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Tecnicos
            .Where(t => t.TecnicoId == id)
            .AsNoTracking()
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == id);
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Tecnicos
            .AsNoTracking()
            .Where(criterio)   
            .ToListAsync();
    }

    public async Task<bool> ExisteNombre(string? name)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Tecnicos
            .AnyAsync<Tecnicos>(t => t.Nombres.ToLower() == name.ToLower());
    }
}
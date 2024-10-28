using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class ClientesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Clientes cliente)
    {
        if (!await Existe(cliente.ClienteId))
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }

    private async Task<bool> Existe(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Clientes
            .AnyAsync<Clientes>(t => t.ClienteId == id);
    }

    private async Task<bool> Insertar(Clientes cliente)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		_contexto.Clientes.Add(cliente);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Clientes cliente)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		_contexto.Clientes.Update(cliente);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Clientes
            .Where(t => t.ClienteId == id)
            .AsNoTracking()
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<Clientes?> Buscar(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.ClienteId == id);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExisteNombre(int clienteId, string? name)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Clientes
            .AnyAsync(e => e.ClienteId != clienteId
            && e.Nombres.ToLower().Equals(name.ToLower()));
    }
}

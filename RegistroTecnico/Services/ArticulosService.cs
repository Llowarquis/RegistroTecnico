using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class ArticulosService(IDbContextFactory<Contexto> DbFactory)
{
	// Buscar
	public async Task<Articulos?> Buscar(int id)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Articulos
			.AsNoTracking()
			.FirstOrDefaultAsync(a => a.ArticuloId == id);
	}

	// Listar
	public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Articulos
			.Where(criterio)
			.AsNoTracking()
			.ToListAsync();
	}
}

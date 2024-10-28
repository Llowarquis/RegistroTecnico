using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using RegistroTecnico.Models.Detalles;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TrabajosDetalleService(IDbContextFactory<Contexto> DbFactory)
{

	// Listar
	public async Task<List<TrabajosDetalle>> Listar(Expression<Func<TrabajosDetalle, bool>> criterio)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.TrabajosDetalles
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}

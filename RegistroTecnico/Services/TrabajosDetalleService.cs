using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using RegistroTecnico.Models.Detalles;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TrabajosDetalleService(Contexto contexto)
{
	private readonly Contexto _contexto = contexto;

	// Listar
	public async Task<List<TrabajosDetalle>> Listar(Expression<Func<TrabajosDetalle, bool>> criterio)
	{
		return await _contexto.TrabajosDetalles
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}

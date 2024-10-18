using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class ArticulosService(Contexto contexto)
{
	private readonly Contexto _contexto = contexto;

	// Listar
	public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
	{
		return await _contexto.Articulos
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}

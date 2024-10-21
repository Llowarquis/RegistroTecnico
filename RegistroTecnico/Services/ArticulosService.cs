using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class ArticulosService(Contexto contexto)
{
	private readonly Contexto _contexto = contexto;

	// Buscar
	public async Task<Articulos?> Buscar(int id)
	{
		return await _contexto.Articulos
			.AsNoTracking()
			.FirstOrDefaultAsync(a => a.ArticuloId == id);
	}

	// Listar
	public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
	{
		return await _contexto.Articulos
			.Where(criterio)
			.AsNoTracking()
			.ToListAsync();
	}
}

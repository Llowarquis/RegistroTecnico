using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using RegistroTecnico.Models.Detalles;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TrabajosService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Trabajos trabajo)
    {
        if (!await Existe(trabajo.TrabajosId))
            return await Insertar(trabajo);
        else
            return await Modificar(trabajo);
    }

    private async Task<bool> Existe(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Trabajos
            .AnyAsync<Trabajos>(tra => tra.TrabajosId == id);
    }

	private async Task<bool> Insertar(Trabajos trabajos)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		await AfectarArticulo(trabajos.TrabajoDetalle.ToArray(), true);
		_contexto.Trabajos.Add(trabajos);
		return await _contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Trabajos trabajos)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		var trabajoOriginal = await _contexto.Trabajos
		                        .Include(t => t.TrabajoDetalle)
		                        .AsNoTracking()
		                        .FirstOrDefaultAsync(t => t.TrabajosId == trabajos.TrabajosId);

		await AfectarArticulo(trabajoOriginal.TrabajoDetalle.ToArray(), false);
		await AfectarArticulo(trabajos.TrabajoDetalle.ToArray(), true);

        _contexto.Attach(trabajos);
		return await _contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		var trabajos = _contexto.Trabajos.Find(id);
		await AfectarArticulo(trabajos.TrabajoDetalle.ToArray(), false);

		_contexto.TrabajosDetalles.RemoveRange(trabajos.TrabajoDetalle);
		_contexto.Trabajos.Remove(trabajos);

		var cantidad = await _contexto.SaveChangesAsync();
        return cantidad > 0;
	}

    public async Task<Trabajos?> Buscar(int id)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Trabajos
            .Include(t => t.Tecnico)
            .Include(c => c.Cliente)
            .Include(td => td.TrabajoDetalle)
            .FirstOrDefaultAsync(t => t.TrabajosId == id);
    }

    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Trabajos
            .Include(t => t.Tecnico)
            .Include(c => c.Cliente)
            .Include(p => p.Prioridad)
            .Include(td => td.TrabajoDetalle)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

	private async Task AfectarArticulo(TrabajosDetalle[] detalle, bool resta = true)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		foreach (var item in detalle)
		{
			var Articulo = await _contexto.Articulos.SingleAsync(p => p.ArticuloId == item.ArticuloId);
			if (resta)
				Articulo.Existencia -= item.Cantidad;
			else
				Articulo.Existencia += item.Cantidad;
		}
	}
}

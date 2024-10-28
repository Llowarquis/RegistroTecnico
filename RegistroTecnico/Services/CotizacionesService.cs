using RegistroTecnico.DAL;
using RegistroTecnico.Models.Detalles;
using RegistroTecnico.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnico.Services;

public class CotizacionesService(IDbContextFactory<Contexto> DbFactory)
{
	public async Task<bool> Guardar(Cotizaciones cotizacion)
	{
		if (!await Existe(cotizacion.CotizacionId))
			return await Insertar(cotizacion);
		else
			return await Modificar(cotizacion);
	}

	private async Task<bool> Existe(int id)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Cotizaciones
			.AnyAsync<Cotizaciones>(c => c.CotizacionId == id);
	}

	private async Task<bool> Insertar(Cotizaciones cotizacion)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		await AfectarArticulo(cotizacion.CotizacionDetalle.ToArray(), true);
		_contexto.Cotizaciones.Add(cotizacion);
		return await _contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Cotizaciones cotizacion)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();

		var cotizacionOriginal = await _contexto.Cotizaciones
			.Include(t => t.CotizacionDetalle)
			.ThenInclude(t => t.Articulos)
			.FirstOrDefaultAsync(t => t.CotizacionId == cotizacion.CotizacionId);

		if (cotizacionOriginal == null)
			return false;

		await AfectarArticulo(cotizacionOriginal.CotizacionDetalle.ToArray(), false);

		foreach (var detalleOriginal in cotizacionOriginal.CotizacionDetalle)
		{
			if (!cotizacion.CotizacionDetalle.Any(d => d.DetalleId == detalleOriginal.DetalleId))
			{
				_contexto.CotizacionesDetalles.Remove(detalleOriginal);
			}
		}

		await AfectarArticulo(cotizacion.CotizacionDetalle.ToArray(), true);

		_contexto.Entry(cotizacionOriginal).CurrentValues.SetValues(cotizacion);

		foreach (var detalle in cotizacion.CotizacionDetalle)
		{
			var detalleExistente = cotizacionOriginal.CotizacionDetalle
				.FirstOrDefault(d => d.DetalleId == detalle.DetalleId);

			if (detalleExistente != null)
			{
				_contexto.Entry(detalleExistente).CurrentValues.SetValues(detalle);
			}
			else
			{
				cotizacionOriginal.CotizacionDetalle.Add(detalle);
			}
		}

		return await _contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		var cotizacion = await _contexto.Cotizaciones
			.Include(t => t.CotizacionDetalle)
			.FirstOrDefaultAsync(t => t.CotizacionId == id);

		if (cotizacion == null)
			return false;

		await AfectarArticulo(cotizacion.CotizacionDetalle.ToArray(), resta: false);

		_contexto.CotizacionesDetalles.RemoveRange(cotizacion.CotizacionDetalle);
		_contexto.Cotizaciones.Remove(cotizacion);

		var cantidad = await _contexto.SaveChangesAsync();
		return cantidad > 0;
	}

	public async Task<Cotizaciones?> Buscar(int id)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Cotizaciones
			.Include(c => c.Cliente)
			.Include(cd => cd.CotizacionDetalle)
			.FirstOrDefaultAsync(c => c.CotizacionId == id);
	}

	public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
	{
		await using var _contexto = await DbFactory.CreateDbContextAsync();
		return await _contexto.Cotizaciones
			.Include(c => c.Cliente)
			.Include(cd => cd.CotizacionDetalle)
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}

	private async Task AfectarArticulo(CotizacionesDetalle[] detalle, bool resta = true)
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
		await _contexto.SaveChangesAsync();
	}
}

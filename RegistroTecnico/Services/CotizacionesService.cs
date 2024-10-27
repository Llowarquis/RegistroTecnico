using RegistroTecnico.DAL;
using RegistroTecnico.Models.Detalles;
using RegistroTecnico.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnico.Services;

public class CotizacionesService(Contexto contexto)
{
	private readonly Contexto _contexto = contexto;
	public async Task<bool> Guardar(Cotizaciones cotizacion)
	{
		if (!await Existe(cotizacion.CotizacionId))
			return await Insertar(cotizacion);
		else
			return await Modificar(cotizacion);
	}

	private async Task<bool> Existe(int id)
	{
		return await _contexto.Cotizaciones
			.AnyAsync<Cotizaciones>(c => c.CotizacionId == id);
	}

	private async Task<bool> Insertar(Cotizaciones cotizacion)
	{
		await AfectarArticulo(cotizacion.CotizacionDetalle.ToArray(), true);
		_contexto.Cotizaciones.Add(cotizacion);
		return await _contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Cotizaciones cotizacion)
	{
		var trabajoOriginal = await _contexto.Cotizaciones
								.Include(c => c.CotizacionDetalle)
								.AsNoTracking()
								.FirstOrDefaultAsync(c => c.CotizacionId == cotizacion.CotizacionId);

		await AfectarArticulo(trabajoOriginal.CotizacionDetalle.ToArray(), false);
		await AfectarArticulo(cotizacion.CotizacionDetalle.ToArray(), true);

		_contexto.Attach(cotizacion);
		return await _contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		var cotizacion = _contexto.Cotizaciones.Find(id);
		await AfectarArticulo(cotizacion.CotizacionDetalle.ToArray(), false);

		_contexto.CotizacionesDetalles.RemoveRange(cotizacion.CotizacionDetalle);
		_contexto.Cotizaciones.Remove(cotizacion);

		var cantidad = await _contexto.SaveChangesAsync();
		return cantidad > 0;
	}

	public async Task<Cotizaciones?> Buscar(int id)
	{
		return await _contexto.Cotizaciones
			.Include(c => c.Cliente)
			.Include(cd => cd.CotizacionDetalle)
			.FirstOrDefaultAsync(c => c.CotizacionId == id);
	}

	public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
	{
		return await _contexto.Cotizaciones
			.Include(c => c.Cliente)
			.Include(cd => cd.CotizacionDetalle)
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}

	private async Task AfectarArticulo(CotizacionesDetalle[] detalle, bool resta = true)
	{
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

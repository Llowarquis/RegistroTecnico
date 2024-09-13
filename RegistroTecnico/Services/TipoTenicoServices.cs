using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;
public class TipoTecnicoServices(Contexto contexto)
{
    // Se hace uso del primary constructor
    private readonly Contexto _contexto = contexto;

    // Este metodo se utilizara en a la hora de modificar o insertar un empleado
    // ambas pantallas presentaran un boton tipo guardar
    public async Task<bool> Guardar(TipoTecnicos tipoTecnico)
    {
        if (!await Existe(tipoTecnico.TipoTecnicoId))
            return await Insertar(tipoTecnico);
        else
            return await Modificar(tipoTecnico);
    }

    private async Task<bool> Existe(int id)
    {
        return await _contexto.TipoTecnicos
            .AnyAsync<TipoTecnicos>(t => t.TipoTecnicoId == id);
    }

    private async Task<bool> Insertar(TipoTecnicos tipoTecnico)
    {
        _contexto.TipoTecnicos.Add(tipoTecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TipoTecnicos tipoTecnico)
    {
        _contexto.Update(tipoTecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        return await _contexto.TipoTecnicos
            .Where(t => t.TipoTecnicoId == id)
            .AsNoTracking()
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<TipoTecnicos?> Buscar(int id)
    {
        return await _contexto.TipoTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoTecnicoId == id);
    }

    public async Task<List<TipoTecnicos>> Listar(Expression<Func<TipoTecnicos, bool>> criterio)
    {
        return await _contexto.TipoTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ValidarDescripcion(string? name)
    {
        return await _contexto?.TipoTecnicos
            .AnyAsync<TipoTecnicos>(t => t.Descripcion.ToLower() == name.ToLower());
    }
}
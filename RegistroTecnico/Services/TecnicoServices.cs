using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TecnicoServices
{
    private readonly Contexto _contexto;
    public TecnicoServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    // Este metodo se utilizara en a la hora de modificar o insertar un empleado
    // ambas pantallas presentaran un boton tipo guardar
    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if(!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.Tecnicos
            .AnyAsync<Tecnicos>(t => t.TecnicoId == id);
    }

    public async Task<bool> Insertar(Tecnicos tecnico)
    {
        _contexto.Tecnicos.Add(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Tecnicos tecnico)
    {
        _contexto.Update(tecnico);
        var modificado = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(tecnico).State = EntityState.Modified;
        return modificado;
    }

    public async Task<bool> Eliminar(int id)
    {
        return await _contexto.Tecnicos
            .Where(t => t.TecnicoId == id)
            .AsNoTracking()
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        return await _contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == id);
    }

    public List<Tecnicos> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return _contexto.Tecnicos
            .AsNoTracking()
            .Where(criterio)   
            .ToList();
    }

    public async Task<bool> ExisteNombre(string? name)
    {
        return await _contexto?.Tecnicos
            .AnyAsync<Tecnicos>(t => t.Nombres.ToLower() == name.ToLower());
    }
}
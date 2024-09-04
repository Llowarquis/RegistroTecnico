using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TecnicoServices
{
    private readonly Contexto contexto;
    public TecnicoServices(Contexto contexto)
    {
        this.contexto = contexto;
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
        return await contexto.Tecnicos
            .AnyAsync<Tecnicos>(t => t.TecnicoId == id);
    }

    public async Task<bool> Insertar(Tecnicos tecnico)
    {
        contexto.Tecnicos.Add(tecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Tecnicos tecnico)
    {
        contexto.Update(tecnico);
        var modificado = await contexto.SaveChangesAsync() > 0;
        contexto.Entry(tecnico).State = EntityState.Modified;
        return modificado;
    }

    public async Task<bool> Eliminar(int id)
    {
        return await contexto.Tecnicos
            .Where(t => t.TecnicoId == id)
            .AsNoTracking()
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        return await contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == id);
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return await contexto.Tecnicos
            .AsNoTracking()
            .Where(criterio)   
            .ToListAsync();
    }

    public async Task<bool> ExisteNombre(string? name)
    {
        return await contexto?.Tecnicos
            .AnyAsync<Tecnicos>(t => t.Nombres.ToLower() == name.ToLower());
    }
}
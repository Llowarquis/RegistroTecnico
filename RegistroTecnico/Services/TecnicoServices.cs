using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services
{
    public class TecnicoServices
    {
        private readonly Contexto contexto;
        public TecnicoServices(Contexto contexto)
        {
            this.contexto = contexto;
        }

        // Este metodo se utilizara en a la hora de modificar o insertar un empleado
        // ambas pantallas presentaran un boton tipo guardar
        public async Task<bool> Guardar(Tecnico tecnico)
        {
            if(!await Existe(tecnico.tecnicoId))
                return await Insertar(tecnico);
            else
                return await Modificar(tecnico);
        }

        public async Task<bool> Existe(int id)
        {
            return await contexto.Tecnicos
                .AnyAsync<Tecnico>(t => t.tecnicoId == id);
        }

        public async Task<bool> Insertar(Tecnico tecnico)
        {
            contexto.Tecnicos.Add(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Tecnico tecnico)
        {
            contexto.Update(tecnico);
            var modificado = await contexto.SaveChangesAsync() > 0;
            contexto.Entry(tecnico).State = EntityState.Modified;
            return modificado;
        }

        public async Task<bool> Eliminar(Tecnico tecnico)
        {
            return await contexto.Tecnicos
                .Where(t => t.tecnicoId == tecnico.tecnicoId)
                .AsNoTracking()
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<Tecnico?> Buscar(int id)
        {
            return await contexto.Tecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.tecnicoId == id);
        }

        public async Task<List<Tecnico>> Listar(Expression<Func<Tecnico, bool>> criterio)
        {
            return await contexto.Tecnicos
                .AsNoTracking()
                .Where(criterio)   
                .ToListAsync();
        }

        public async Task<bool> ExisteNombre(string? name)
        {
            return await contexto.Tecnicos
                .AnyAsync<Tecnico>(t =>t.nombre.ToLower() == name.ToLower());
        }
	}
}
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

        public async Task<bool> Guardar(Tecnico tecnico)
        {
            if(!await Existe(tecnico.tecnicoId))
                return await Insertar(tecnico);
            else
                return await Modificar(tecnico);
        }

        private async Task<bool> Existe(int id)
        {
            return await contexto.Tecnicos
                .AnyAsync<Tecnico>(t => t.tecnicoId == id);
        }

        private async Task<bool> Insertar(Tecnico tecnico)
        {
            contexto.Tecnicos.Add(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tecnico tecnico)
        {
            contexto.Update(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            return await contexto.Tecnicos
                .Where(t => t.tecnicoId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<Tecnico?> Buscar(int id)
        {
            return await contexto.Tecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.tecnicoId == id);
        }

        public List<Tecnico> Listar(Expression<Func<Tecnico, bool>> criterio)
        {
            return contexto.Tecnicos
                .Where(criterio)
                .ToList();
        }

    }
}

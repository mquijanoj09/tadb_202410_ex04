using System.Data;
using IAEA_CS_NoSQL_REST_API.DbContexts;
using IAEA_CS_NoSQL_REST_API.Interfaces;
using IAEA_CS_NoSQL_REST_API.Models;
using IAEA_CS_NoSQL_REST_API.Helpers;
using MongoDB.Driver;

namespace IAEA_CS_NoSQL_REST_API.Repositories
{
    public class TipoRepository(MongoDbContext unContexto) : ITipoRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<IEnumerable<Tipo>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionTipos = conexion
                .GetCollection<Tipo>(contextoDB.ConfiguracionColecciones.ColeccionTipos);

            var losTipos = await coleccionTipos
                .Find(_ => true)
                .SortBy(tipo => tipo.Tipo_nombre)
                .ToListAsync();
            return losTipos;
        }

        public async Task<Tipo> GetByIdAsync(string tipo_id)
        {
            Tipo unTipo = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionTipos = conexion
                .GetCollection<Tipo>(contextoDB.ConfiguracionColecciones.ColeccionTipos);

            var resultado = await coleccionTipos
                .Find(tipo => tipo.Id == tipo_id)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unTipo = resultado;
            return unTipo;
        }
    }
}
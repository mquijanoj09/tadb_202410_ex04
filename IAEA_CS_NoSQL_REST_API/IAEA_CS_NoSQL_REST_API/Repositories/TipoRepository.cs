using System.Data;
using IAEA_CS_NoSQL_REST_API.DbContexts;
using IAEA_CS_NoSQL_REST_API.Interfaces;
using IAEA_CS_NoSQL_REST_API.Models;
using IAEA_CS_NoSQL_REST_API.Helpers;

namespace IAEA_CS_NoSQL_REST_API.Repositories
{
    public class TipoRepository(MongoDbContext unContexto) : ITipoRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<IEnumerable<Tipo>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT id, tipo_nombre " +
                                  "FROM core.Tipos " +
                                  "ORDER BY id DESC";

            var resultadoTipos = await conexion
                .QueryAsync<Tipo>(sentenciaSQL, new DynamicParameters());

            return resultadoTipos;
        }

        public async Task<Tipo> GetByIdAsync(int tipo_id)
        {
            Tipo unaTipo = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@tipo_id", tipo_id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, tipo_nombre " +
                                  "FROM core.Tipos " +
                                  "WHERE id = @tipo_id " +
                                  "ORDER BY id DESC";

            var resultado = await conexion.QueryAsync<Tipo>(sentenciaSQL,
                parametrosSentencia);

            if (resultado.Any())
                unaTipo = resultado.First();

            return unaTipo;
        }

    }
}
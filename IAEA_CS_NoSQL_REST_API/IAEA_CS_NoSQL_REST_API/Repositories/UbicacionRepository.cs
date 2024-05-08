using System.Data;
using IAEA_CS_NoSQL_REST_API.DbContexts;
using IAEA_CS_NoSQL_REST_API.Interfaces;
using IAEA_CS_NoSQL_REST_API.Models;
using IAEA_CS_NoSQL_REST_API.Helpers;
using MongoDB.Driver;

namespace IAEA_CS_NoSQL_REST_API.Repositories
{
    public class UbicacionRepository(MongoDbContext unContexto) : IUbicacionRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<IEnumerable<Ubicacion>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionUbicaciones = conexion
                .GetCollection<Ubicacion>(contextoDB.ConfiguracionColecciones.ColeccionUbicaciones);

            var losUbicaciones = await coleccionUbicaciones
                .Find(_ => true)
                .SortBy(ubicacion => ubicacion.Ciudad)
                .ToListAsync();
            return losUbicaciones;
        }

        public async Task<Ubicacion> GetByIdAsync(string ubicacion_id)
        {
            Ubicacion unUbicacion = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionUbicaciones = conexion
                .GetCollection<Ubicacion>(contextoDB.ConfiguracionColecciones.ColeccionUbicaciones);

            var resultado = await coleccionUbicaciones
                .Find(ubicacion => ubicacion.Id == ubicacion_id)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unUbicacion = resultado;
            return unUbicacion;
        }

        public async Task<Ubicacion> GetReactoresUbicacionAsync(string ubicacion_id)
        {
            Ubicacion reactoresUbicacion = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionReactores = conexion.GetCollection<Ubicacion>(contextoDB.ConfiguracionColecciones.ColeccionReactores);

            var filtro = Builders<Ubicacion>.Filter.Eq("id", ubicacion_id);
            var resultado = await coleccionReactores.Find(filtro).FirstOrDefaultAsync();

            if (resultado != null)
                reactoresUbicacion = resultado;

            return reactoresUbicacion;
        }
    }
}
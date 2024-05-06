using System.Data;
using IAEA_CS_NoSQL_REST_API.DbContexts;
using IAEA_CS_NoSQL_REST_API.Interfaces;
using IAEA_CS_NoSQL_REST_API.Models;
using IAEA_CS_NoSQL_REST_API.Helpers;
using MongoDB.Driver;
using System.Security.Claims;

namespace IAEA_CS_NoSQL_REST_API.Repositories
{
    public class ReactorRepository(MongoDbContext unContexto) : IReactorRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<IEnumerable<Reactor>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionReactores = conexion
                .GetCollection<Reactor>(contextoDB.ConfiguracionColecciones.ColeccionReactores);

            var losReactores = await coleccionReactores
                .Find(_ => true)
                .SortBy(reactor => reactor.Nombre)
                .ToListAsync();
            return losReactores;
        }

        public async Task<Reactor> GetByNameAsync(string reactor_nombre)
        {
            Reactor unReactor = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionReactores = conexion
                .GetCollection<Reactor>(contextoDB.ConfiguracionColecciones.ColeccionReactores);

            var resultado = await coleccionReactores
                .Find(reactor => reactor.Nombre!.ToLower().Equals(reactor_nombre.ToLower()))
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unReactor = resultado;

            return unReactor;
        }

        public async Task<Reactor> GetByIdAsync(string reactor_id)
        {
            Reactor unReactor = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionReactores = conexion
                .GetCollection<Reactor>(contextoDB.ConfiguracionColecciones.ColeccionReactores);

            var resultado = await coleccionReactores
                .Find(reactor => reactor.Id == reactor_id)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unReactor = resultado;
            return unReactor;
        }

        public async Task<bool> CreateAsync(Reactor unReactor)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionReactores = conexion.GetCollection<Reactor>(contextoDB.ConfiguracionColecciones.ColeccionReactores);

            await coleccionReactores
                .InsertOneAsync(unReactor);

            var resultado = await coleccionReactores
                .Find(reactor => reactor.Nombre == unReactor.Nombre)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                resultadoAccion = true;
            return resultadoAccion;
        }

        public async Task<bool> UpdateAsync(Reactor unReactor)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionReactores = conexion.GetCollection<Reactor>(contextoDB.ConfiguracionColecciones.ColeccionReactores);

            var resultado = await coleccionReactores
                .ReplaceOneAsync(reactor => reactor.Id == unReactor.Id, unReactor);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }
        public async Task<bool> RemoveAsync(string reactor_id)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionReactores = conexion.GetCollection<Reactor>(contextoDB.ConfiguracionColecciones.ColeccionReactores);

            var resultado = await coleccionReactores
                .DeleteOneAsync(reactor => reactor.Id == reactor_id);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }
    }
}
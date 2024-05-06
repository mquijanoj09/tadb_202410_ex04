using System;
using IAEA_CS_NoSQL_REST_API.Models;
using MongoDB.Driver;

namespace IAEA_CS_NoSQL_REST_API.DbContexts
{
    public class MongoDbContext(IConfiguration unaConfiguracion)
    {
        private readonly string cadenaConexion = unaConfiguracion.GetConnectionString("Mongo")!;
        private readonly ReactoresDatabaseSettings _reactoresDatabaseSettings = new(unaConfiguracion);

        public IMongoDatabase CreateConnection()
        {
            var clienteDB = new MongoClient(cadenaConexion);
            var miDB = clienteDB.GetDatabase(_reactoresDatabaseSettings.DatabaseName);

            return miDB;
        }

        public ReactoresDatabaseSettings ConfiguracionColecciones
        {
            get { return _reactoresDatabaseSettings; }
        }
    }
}
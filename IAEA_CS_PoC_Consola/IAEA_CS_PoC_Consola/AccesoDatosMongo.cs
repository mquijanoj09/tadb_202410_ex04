using Dapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
namespace IAEA_CS_PoC_Consola
{
    public class AccesoDatosMongo
    {
        public static string? ObtieneCadenaConexion()
        {
            //Parametrizamos el acceso al archivo de configuración appsettings.json
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration miConfiguracion = builder.Build();

            return miConfiguracion["ConnectionString:Mongo"];
        }

        public static List<Reactor> ObtieneListaReactores()
        {
            string? cadenaConexion = ObtieneCadenaConexion();

            var clienteDB = new MongoClient(cadenaConexion);
            var miDB = clienteDB.GetDatabase("reactores_db");
            var coleccionReactores = "reactores";

            var listaReactores = miDB.GetCollection<Reactor>(coleccionReactores)
                .Find(new BsonDocument())
                .SortBy(reactor => reactor.Nombre)
                .ToList();

            return listaReactores;
        }

        public static Reactor ObtieneReactor(string nombre_reactor)
        {
            string? cadenaConexion = ObtieneCadenaConexion();

            var clienteDB = new MongoClient(cadenaConexion);
            var miDB = clienteDB.GetDatabase("reactores_db");
            var coleccionReactores = "reactores";

            var filtroReactor = new BsonDocument { { "nombre", nombre_reactor } };

            var unaReactor = miDB.GetCollection<Reactor>(coleccionReactores)
                .Find(filtroReactor)
                .FirstOrDefault();

            return unaReactor;
        }

        public static List<string> ObtieneNombresReactores()
        {
            var listaReactores = ObtieneListaReactores();

            List<string> listaNombresReactores = [];

            foreach (Reactor unaReactor in listaReactores)
                listaNombresReactores.Add(unaReactor.Nombre!);

            return listaNombresReactores;
        }

        public static string ObtenerObjectIdReactor(string nombre_reactor)
        {

            string? cadenaConexion = ObtieneCadenaConexion();

            var clienteDB = new MongoClient(cadenaConexion);
            var miDB = clienteDB.GetDatabase("reactores_db");
            var coleccionReactores = "reactores";

            var filtroReactor = new BsonDocument { { "nombre", nombre_reactor } };

            var unaReactor = miDB.GetCollection<Reactor>(coleccionReactores)
                .Find(filtroReactor)
                .FirstOrDefault();

            return unaReactor.ObjectId!;
        }

        public static bool InsertaReactor(Reactor unaReactor)
        {
            string? cadenaConexion = ObtieneCadenaConexion();

            var clienteDB = new MongoClient(cadenaConexion);
            var miDB = clienteDB.GetDatabase("reactores_db");
            var miColeccion = miDB.GetCollection<Reactor>("reactores");

            miColeccion.InsertOne(unaReactor);

            string ObjectIdReactor = ObtenerObjectIdReactor(unaReactor.Nombre!);

            if (string.IsNullOrEmpty(ObjectIdReactor))
                return false;
            else
                return true;
        }

        public static bool ActualizaReactor(Reactor unaReactor)
        {
            string? cadenaConexion = ObtieneCadenaConexion();

            var clienteDB = new MongoClient(cadenaConexion);
            var miDB = clienteDB.GetDatabase("reactores_db");
            var miColeccion = miDB.GetCollection<Reactor>("reactores");

            var resultadoActualizacion = miColeccion
                                            .ReplaceOne(reactor => reactor.ObjectId == unaReactor.ObjectId, unaReactor);

            return resultadoActualizacion.IsAcknowledged;
        }

        public static bool EliminaReactor(Reactor unaReactor, out string mensajeEliminacion)
        {
            mensajeEliminacion = string.Empty;

            string? cadenaConexion = ObtieneCadenaConexion();

            var clienteDB = new MongoClient(cadenaConexion);
            var miDB = clienteDB.GetDatabase("reactores_db");
            var miColeccion = miDB.GetCollection<Reactor>("reactores");

            var resultadoEliminacion = miColeccion.DeleteOne(reactor => reactor.ObjectId == unaReactor.ObjectId);

            if (resultadoEliminacion.IsAcknowledged)
                mensajeEliminacion = "Reactor eliminada exitosamente!";
            else
                mensajeEliminacion = $"No se pudo borrar la reactor {unaReactor.Nombre!}";

            return resultadoEliminacion.IsAcknowledged;
        }
    }
}
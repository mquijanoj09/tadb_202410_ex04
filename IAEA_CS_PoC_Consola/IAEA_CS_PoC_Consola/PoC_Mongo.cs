using System;
namespace IAEA_CS_PoC_Consola
{
	public class PoC_Mongo
    {

        public static void Ejecuta_PoC()
        {
            string? cadenaConexion = AccesoDatosMongo.ObtieneCadenaConexion();
            Console.WriteLine($"El string de conexión obtenido es: \n{cadenaConexion}\n");
            //R del CRUD - Lectura de registros existentes - SELECT
            VisualizaNombresReactores();
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();

            VisualizaReactores();

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();

            //C del CRUD - Creación de un nuevo registro - INSERT
            Reactor nuevaReactor = new()
            {
                Nombre = "NUR",
                Potencia = 1,
                Estado = "OPERATIONAL",
                Fecha = new DateTime(2000, 1, 1)
            };

            Console.WriteLine($"\nRegistro de nueva reactor: {nuevaReactor.Nombre}:");

            bool resultadoInsercion = AccesoDatosMongo.InsertaReactor(nuevaReactor);

            if (resultadoInsercion == false)
                Console.WriteLine($"Inserción fallida para la reactor {nuevaReactor.Nombre}");
            else
            {
                Console.WriteLine($"Inserción exitosa! Este fue la reactor registrada");

                //Obtenemos la reactor por nombre
                nuevaReactor = AccesoDatosMongo.ObtieneReactor(nuevaReactor.Nombre);
                Console.WriteLine($"Id: {nuevaReactor.ObjectId}, Nombre: {nuevaReactor.Nombre}");
            }

            VisualizaReactores();

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();

            //U del CRUD - Actualización de un nuevo registro - UPDATE
            Reactor reactorActualizada = AccesoDatosMongo.ObtieneReactor("Mango");

            reactorActualizada.Nombre = "RRR";
            Console.WriteLine($"\n\nActualizando la reactor No. {reactorActualizada.ObjectId} " +
                $"al nuevo nombre de {reactorActualizada.Nombre}...");

            bool resultadoActualizacion = AccesoDatosMongo.ActualizaReactor(reactorActualizada);

            if (resultadoActualizacion == false)
                Console.WriteLine($"Actualización fallida para la reactor {reactorActualizada.Nombre}");
            else
            {
                Console.WriteLine($"Actualización exitosa! Este fue la reactor actualizada");

                //Obtenemos la reactor por Id
                Reactor unaReactor = AccesoDatosMongo.ObtieneReactor(reactorActualizada.ObjectId);
                Console.WriteLine($"Id: {unaReactor.ObjectId}, Nombre: {unaReactor.Nombre}");
            }

            VisualizaReactores();

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();

            //Devolvemos la reactor a su valor orignal
            reactorActualizada.Nombre = "FRH";
            Console.WriteLine($"Devolviendo el nombre original a la reactor: {reactorActualizada.Nombre}");

            AccesoDatosMongo.ActualizaReactor(reactorActualizada);

            VisualizaReactores();

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();


            //D del CRUD - Borrado de una reactor existente - DELETE
            nuevaReactor = AccesoDatosMongo.ObtieneReactor(nuevaReactor.Nombre!);
            Console.WriteLine($"\n\nBorrando la reactor {nuevaReactor.Nombre} ...");

            bool resultadoEliminacion = AccesoDatosMongo.EliminaReactor(nuevaReactor, out string mensajeEliminacion);

            if (resultadoEliminacion == false)
                Console.WriteLine(mensajeEliminacion);
            else
            {
                Console.WriteLine($"Eliminación exitosa! la reactor {nuevaReactor.Nombre} fue eliminada");
                VisualizaReactores();
            }

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();


        }

        /// <summary>
        /// Visualiza la lista de nombres de reactores registrados en la DB
        /// </summary>
        public static void VisualizaNombresReactores()
        {
            Console.WriteLine($"Nombres de reactores registradas en la DB:");
            List<string> losNombresReactores = AccesoDatosMongo.ObtieneNombresReactores();
            if (losNombresReactores.Count == 0)
                Console.WriteLine("No se encontraron reactores");
            else
            {
                Console.WriteLine($"\nSe encontraron {losNombresReactores.Count} reactores:");
                foreach (string unNombreReactor in losNombresReactores)
                    Console.WriteLine($"- {unNombreReactor}");
            }
        }

        /// <summary>
        /// Visualiza la lista de reactores con sus propiedades básicas
        /// </summary>
        public static void VisualizaReactores()
        {
            //Aqui demostramos la manipulación de una lista de objetos tipo Reactores
            List<Reactor> lasReactores = AccesoDatosMongo.ObtieneListaReactores();

            Console.WriteLine("\n\nLas reactores con sus propiedades básicas son:");

            foreach (Reactor unaReactor in lasReactores)
            {
                Console.WriteLine($"Id: {unaReactor.ObjectId}\tNombre: {unaReactor.Nombre}");
                Console.WriteLine($"Potencia: {unaReactor.Potencia}");
                Console.WriteLine($"Estado: {unaReactor.Estado}\n");
                Console.WriteLine($"Fecha: {unaReactor.Fecha}\n");
            }
        }
    }
}
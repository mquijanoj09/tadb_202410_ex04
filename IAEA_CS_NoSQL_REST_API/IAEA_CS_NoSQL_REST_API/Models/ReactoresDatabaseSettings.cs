using System;
namespace IAEA_CS_NoSQL_REST_API.Models
{
    public class ReactoresDatabaseSettings
    {
        public string DatabaseName { get; set; } = null!;
        public string ColeccionReactores { get; set; } = null!;

        public ReactoresDatabaseSettings(IConfiguration unaConfiguracion)
        {
            var configuracion = unaConfiguracion.GetSection("ReactoresDatabaseSettings");

            DatabaseName = configuracion.GetSection("DatabaseName").Value!;
            ColeccionReactores = configuracion.GetSection("ColeccionReactores").Value!;
        }
    }
}
﻿using IAEA_CS_NoSQL_REST_API.DbContexts;
using IAEA_CS_NoSQL_REST_API.Interfaces;
using IAEA_CS_NoSQL_REST_API.Models;

namespace IAEA_CS_NoSQL_REST_API.Repositories
{
    public class ResumenRepository(MongoDbContext unContexto) : IResumenRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<Resumen> GetAllAsync()
        {
            Resumen unResumen = new();

            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT COUNT(id) total FROM core.Reactores";
            unResumen.Reactores = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            sentenciaSQL = "SELECT COUNT(id) total FROM core.Tipos";
            unResumen.Tipos = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            sentenciaSQL = "SELECT COUNT(id) total FROM core.Ciudades";
            unResumen.Ubicaciones = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            return unResumen;
        }
    }
}
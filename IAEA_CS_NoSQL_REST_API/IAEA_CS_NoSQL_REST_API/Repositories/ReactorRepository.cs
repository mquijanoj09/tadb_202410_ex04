﻿using System.Data;
using IAEA_CS_NoSQL_REST_API.DbContexts;
using IAEA_CS_NoSQL_REST_API.Interfaces;
using IAEA_CS_NoSQL_REST_API.Models;
using IAEA_CS_NoSQL_REST_API.Helpers;

namespace IAEA_CS_NoSQL_REST_API.Repositories
{
    public class ReactorRepository(MongoDbContext unContexto) : IReactorRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<IEnumerable<Reactor>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT id, nombre, potencia, estado, fecha, tipo_id, ciudad_id " +
                                  "FROM core.Reactores " +
                                  "ORDER BY id DESC";

            var resultadoReactores = await conexion
                .QueryAsync<Reactor>(sentenciaSQL, new DynamicParameters());

            return resultadoReactores;
        }

        public async Task<Reactor> GetByIdAsync(int reactor_id)
        {
            Reactor unaReactor = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@reactor_id", reactor_id, System.Data.DbType.Int32,  System.Data.ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, nombre, potencia, estado, fecha, tipo_id, ciudad_id " +
                                  "FROM core.Reactores " +
                                  "WHERE id = @reactor_id " +
                                  "ORDER BY nombre";

            var resultado = await conexion.QueryAsync<Reactor>(sentenciaSQL,
                parametrosSentencia);

            if (resultado.Any())
                unaReactor = resultado.First();

            return unaReactor;
        }

        public async Task<Reactor> GetByNameAsync(string reactor_nombre)
        {
            Reactor unaReactor = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@reactor_nombre", reactor_nombre,
                                    DbType.String, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, nombre, potencia, estado, fecha, tipo_id, ciudad_id " +
                                  "FROM core.Reactores " +
                                  "WHERE nombre = @reactor_nombre " +
                                  "ORDER BY nombre";

            var resultado = await conexion.QueryAsync<Reactor>(sentenciaSQL,
                parametrosSentencia);

            if (resultado.Any())
                unaReactor = resultado.First();

            return unaReactor;
        }

        public async Task<bool> CreateAsync(Reactor unaReactor)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "core.p_inserta_reactor";
                var parametros = new
                {
                    p_nombre = unaReactor.Nombre,
                    p_potencia = unaReactor.Potencia,
                    p_estado = unaReactor.Estado,
                    p_fecha = unaReactor.Fecha,
                    p_ciudad_id = unaReactor.Ciudad_id,
                    p_tipo_id = unaReactor.Tipo_id
                };

                var cantidad_filas = await conexion.ExecuteAsync(
                    procedimiento,
                    parametros,
                    commandType: CommandType.StoredProcedure);

                if (cantidad_filas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }

        public async Task<bool> UpdateAsync(Reactor unaReactor)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "core.p_actualiza_reactor";
                var parametros = new
                {
                    p_id = unaReactor.Id,
                    p_nombre = unaReactor.Nombre,
                    p_potencia = unaReactor.Potencia,
                    p_estado = unaReactor.Estado,
                    p_fecha = unaReactor.Fecha,
                    p_ciudad_id = unaReactor.Ciudad_id,
                    p_tipo_id = unaReactor.Tipo_id
                };

                var cantidad_filas = await conexion.ExecuteAsync(
                    procedimiento,
                    parametros,
                    commandType: CommandType.StoredProcedure);

                if (cantidad_filas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }

        public async Task<bool> RemoveAsync(int reactor_id)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "core.p_elimina_reactor";
                var parametros = new
                {
                    p_id = reactor_id
                };

                var cantidad_filas = await conexion.ExecuteAsync(
                    procedimiento,
                    parametros,
                    commandType: CommandType.StoredProcedure);

                if (cantidad_filas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }
    }
}
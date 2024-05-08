﻿using System;
using IAEA_CS_NoSQL_REST_API.Helpers;
using IAEA_CS_NoSQL_REST_API.Interfaces;
using IAEA_CS_NoSQL_REST_API.Models;
using System.Runtime.Intrinsics.X86;

namespace IAEA_CS_NoSQL_REST_API.Services
{
    public class UbicacionService(IUbicacionRepository ubicacionRepository)
    {
        private readonly IUbicacionRepository _ubicacionRepository = ubicacionRepository;

        public async Task<IEnumerable<Ubicacion>> GetAllAsync()
        {
            return await _ubicacionRepository
                .GetAllAsync();
        }

        public async Task<Ubicacion> GetByIdAsync(string ubicacion_id)
        {
            Ubicacion unaUbicacion = await _ubicacionRepository
                .GetByIdAsync(ubicacion_id);

            if (unaUbicacion.Id == "")
                throw new AppValidationException($"Ubicacion no encontrada con el id {ubicacion_id}");

            return unaUbicacion;
        }

        public async Task<Ubicacion> GetReactoresUbicacionAsync(string ubicacion_id)
        {
            Ubicacion reactoresUbicacion = await _ubicacionRepository
                .GetReactoresUbicacionAsync(ubicacion_id);

            if (reactoresUbicacion.Id == "")
                throw new AppValidationException($"Ubicacion no encontrada con el id {ubicacion_id}");

            return reactoresUbicacion;
        }

    }
}

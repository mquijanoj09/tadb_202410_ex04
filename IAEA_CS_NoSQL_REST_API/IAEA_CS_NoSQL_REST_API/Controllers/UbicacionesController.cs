using System;
using Microsoft.AspNetCore.Mvc;
using IAEA_CS_NoSQL_REST_API.Services;
using IAEA_CS_NoSQL_REST_API.Helpers;
using IAEA_CS_NoSQL_REST_API.Models;

namespace IAEA_CS_NoSQL_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionesController(UbicacionService ubicacionService) : Controller
    {
        private readonly UbicacionService _ubicacionService = ubicacionService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasUbicaciones = await _ubicacionService
                .GetAllAsync();

            return Ok(lasUbicaciones);
        }

        [HttpGet("{ubicacion_id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string ubicacion_id)
        {
            try
            {
                var unaUbicacion = await _ubicacionService
                    .GetByIdAsync(ubicacion_id);

                return Ok(unaUbicacion);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpGet("{ubicacion_id:length(24)}/Reactores")]
        public async Task<IActionResult> GetReactoresUbicacionAsync(string ubicacion_id)
        {
            try
            {
                var losReactoresUbicacion = await _ubicacionService
                    .GetReactoresUbicacionAsync(ubicacion_id);

                return Ok(losReactoresUbicacion);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

    }
}
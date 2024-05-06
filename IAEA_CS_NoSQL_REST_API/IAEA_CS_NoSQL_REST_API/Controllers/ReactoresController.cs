﻿using System;
using Microsoft.AspNetCore.Mvc;
using IAEA_CS_NoSQL_REST_API.Services;
using IAEA_CS_NoSQL_REST_API.Helpers;
using IAEA_CS_NoSQL_REST_API.Models;

namespace IAEA_CS_NoSQL_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactoresController(ReactorService reactorService) : Controller
    {
        private readonly ReactorService _reactorService = reactorService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasReactores = await _reactorService
                .GetAllAsync();

            return Ok(lasReactores);
        }

        [HttpGet("{reactor_id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string reactor_id)
        {
            try
            {
                var unaReactor = await _reactorService
                    .GetByIdAsync(reactor_id);

                return Ok(unaReactor);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Reactor unaReactor)
        {
            try
            {
                var reactorCreada = await _reactorService
                    .CreateAsync(unaReactor);

                return Ok(reactorCreada);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Reactor unaReactor)
        {
            try
            {
                var reactorActualizada = await _reactorService
                    .UpdateAsync(unaReactor);

                return Ok(reactorActualizada);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        [HttpDelete("{reactor_id:length(24)}")]
        public async Task<IActionResult> RemoveAsync(string reactor_id)
        {
            try
            {
                var nombreReactorBorrada = await _reactorService
                    .RemoveAsync(reactor_id);

                return Ok($"La reactor {nombreReactorBorrada} fue eliminada correctamente!");
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}
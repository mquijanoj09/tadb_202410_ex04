﻿using IAEA_CS_NoSQL_REST_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IAEA_CS_NoSQL_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumenController : Controller
    {
        private readonly ResumenService _resumenService;

        public ResumenController(ResumenService resumenService)
        {
            _resumenService = resumenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var elResumen = await _resumenService
                .GetAllAsync();

            return Ok(elResumen);
        }
    }
}
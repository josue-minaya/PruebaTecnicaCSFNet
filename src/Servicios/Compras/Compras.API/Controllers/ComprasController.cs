using Compras.API.Response;
using Compras.Aplicacion.DTO;
using Compras.Aplicacion.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Compras.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : Controller
    {
        private readonly CompraService _service;
        private readonly ILogger<ComprasController> _logger;

        public ComprasController(CompraService service, ILogger<ComprasController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] CrearCompraDto dto)
        {
            try
            {
                await _service.CrearCompraAsync(dto);
                _logger.LogInformation("Compra registrada exitosamente con {0} ítems.", dto.Detalles.Count);
                return Ok(ApiResponse<string>.Ok(dto.Detalles.Count + " fueron creados ", "Compra creado correctamente"));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar la Compra.");
                return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Ocurrió un error al registrar la Compra" }));

            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var compras = await _service.ListarComprasAsync();
                _logger.LogInformation("Se listaron {0} Compra.", compras.Count);
                return Ok(ApiResponse<List<CompraCabDto>>.Ok(compras, "Lista de Compra"));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar Compra.");
                return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Error al obtener las Compra" }));
            }
        }
    }
}
using Movimientos.API.Response;
using Movimientos.Aplicacion.DTO;
using Movimientos.Aplicacion.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movimientos.Dominio.Entidad;
using Microsoft.AspNetCore.Authorization;

namespace Movimientos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : Controller
    {
        private readonly MovimientoService _service;
        private readonly ILogger<MovimientosController> _logger;

        public MovimientosController(MovimientoService service, ILogger<MovimientosController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] CrearMovimientoDto dto)
        {
            try
            {
                await _service.CrearMovimientoAsync(dto);
                _logger.LogInformation("Movimiento registrada exitosamente con {0} ítems.", dto.Detalles.Count);
                return Ok(ApiResponse<string>.Ok(dto.Detalles.Count + " fueron creados ", "Movimiento creado correctamente"));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar la movimiento.");
                return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Ocurrió un error al registrar la movimiento" }));

            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var movimiento = await _service.ListarMovimientoAsync();
                _logger.LogInformation("Se listaron {0} movimientos.", movimiento.Count);
                return Ok(ApiResponse<List<MovimientoCabDto>>.Ok(movimiento, "Lista de Movimiento"));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar Movimiento.");
                return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Error al obtener las Movimiento" }));
            }
        }
    }
}
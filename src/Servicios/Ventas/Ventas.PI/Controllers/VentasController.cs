using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ventas.API.Response;
using Ventas.Aplicacion.DTO;
using Ventas.Aplicacion.Servicios;
using Ventas.Dominio.Entidad;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ventas.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly VentaService _service;
    private readonly ILogger<VentasController> _logger;

    public VentasController(VentaService service, ILogger<VentasController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> Registrar([FromBody] CrearVentaDto dto)
    {
        try
        {

            await _service.CrearVentaAsync(dto);
            _logger.LogInformation("Venta registrada exitosamente con {0} ítems.", dto.Detalles.Count);
            return Ok(ApiResponse<string>.Ok(dto.Detalles.Count +" fueron creados ", "Venta creado correctamente"));

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar la venta.");
            return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Ocurrió un error al registrar la venta"  }));

        }
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var ventas = await _service.ListarVentasAsync();
            _logger.LogInformation("Se listaron {0} ventas.", ventas.Count);
            return Ok(ApiResponse<List<VentaCabDto>>.Ok(ventas, "Lista de ventas"));

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar ventas.");
            return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Error al obtener las ventas" }));
        }
    }
}

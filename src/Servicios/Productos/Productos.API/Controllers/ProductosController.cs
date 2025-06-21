using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Productos.API.Response;
using Productos.Aplicacion.DTO;
using Productos.Aplicacion.Service;
using Productos.Dominio.Entidad;


namespace Productos.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ProductoService _service;
    private readonly ILogger<ProductosController> _logger;

    public ProductosController(ProductoService service,ILogger<ProductosController> logger  )
    {
        _service = service;
        _logger = logger;
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        _logger.LogInformation("Obteniendo producto con ID: {Id}", id);
        try
        {
            var producto = await _service.ObtenerProductoPorIdAsync(id);
            if (producto == null)
            {
                _logger.LogWarning("Producto con ID {Id} no encontrado", id);
                return NotFound(ApiResponse<string>.Fail(new List<string> { "Producto no encontrado" }));
            }
            _logger.LogInformation("Producto obtenido: {@Producto}", producto);
            return Ok(ApiResponse<Producto>.Ok(producto, "Producto obtenido correctamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener producto con ID {Id}", id);
            return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Error interno del servidor" }));
        }
    }
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        _logger.LogInformation("Obteniendo lista de productos");

        try
        {

            var listaProductos = await _service.ListarProductosAsync();
            _logger.LogInformation("Se obtuvo {Cantidad} productos", listaProductos.Count);

            return Ok(ApiResponse<List<ProductoDTO>>.Ok(listaProductos, "Lista de productos"));

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener productos");
            return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Error interno del servidor" }));
        }

    }
    [HttpPost("Registrar")]
    public async Task<IActionResult> Crear(CrearProductoDto dto)
    {
        _logger.LogInformation("Intentando crear producto: {@Producto}", dto);
        try
        {
            var nuevoProducto=await _service.CrearProductoAsync(dto);
            _logger.LogInformation("Producto creado con ID: {Id}", nuevoProducto.Id_producto);
            return Ok(ApiResponse<Producto>.Ok(nuevoProducto, "Producto creado correctamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear producto");
            return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Error al crear producto" }));
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] CrearProductoDto dto)
    {
        _logger.LogInformation("Intentando actualizar producto con ID: {Id}", id);
        try
        {
             await _service.ActualizarProductoAsync(id, dto);
            _logger.LogInformation("Producto actualizado correctamente (ID: {Id})", id);
            return Ok(ApiResponse<CrearProductoDto>.Ok(dto, "Producto actualizado correctamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar producto con ID {Id}", id);
            return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Error al actualizar producto" }));
        }
    }
}

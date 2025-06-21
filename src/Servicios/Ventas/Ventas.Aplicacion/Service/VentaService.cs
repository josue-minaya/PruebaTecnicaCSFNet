using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Ventas.Aplicacion.DTO;
using Ventas.Dominio.Entidad;
using Ventas.Dominio.IRepositorio;

namespace Ventas.Aplicacion.Servicios;

public class VentaService
{
    private readonly IVentaRepository _repo;
    public readonly HttpClient _httpClientMov;

    public readonly HttpClient _httpClientPro;

    public VentaService(IVentaRepository repo, IHttpClientFactory httpClientFactory)
    {
        _repo = repo;
        _httpClientMov = httpClientFactory.CreateClient("Movimientos");
        _httpClientPro = httpClientFactory.CreateClient("Productos");

    }
    public async Task CrearVentaAsync(CrearVentaDto dto)
    {
        decimal subtotal = 0;
        decimal igv = 0;
        decimal total = 0;

        var detalles = new List<VentaDet>();

        foreach (var d in dto.Detalles)
        {
            var producto = await ObtenerPrecioProductoAsync(d.Id_Producto);
            if (producto == null)
                throw new Exception($"Producto con ID {d.Id_Producto} no encontrado.");

            var precio = producto.PrecioVenta;
            var st = precio * d.Cantidad;
            var igvDet = st * 0.18m;
            var totalDet = st + igvDet;

            subtotal += st;
            igv += igvDet;
            total += totalDet;

            detalles.Add(new VentaDet
            {
                Id_Producto = d.Id_Producto,
                Cantidad = d.Cantidad,
                Precio = precio,
                Sub_Total = st,
                Igv = igvDet,
                Total = totalDet
            });
        }

        var venta = new VentaCab
        {
            SubTotal = subtotal,
            Igv = igv,
            Total = total,
        };

        await _repo.CrearVentaCabAsync(venta);

        foreach (var detalle in detalles)
        {
            detalle.Id_VentaCab = venta.Id_VentaCab;
        }

        await _repo.CrearVentaDetAsync(detalles);

        var movimientoDetalles = detalles.Select(d => new CrearMovimientoDetDto
        {
            Id_Producto = d.Id_Producto,
            Cantidad = d.Cantidad
        }).ToList();

        await RegistrarMovimientoVentaAsync(venta.Id_VentaCab, movimientoDetalles);
    }

    public async Task<List<VentaCabDto>> ListarVentasAsync()
    {
        var entidades = await _repo.ListarCabAsync();

        var ventas = entidades.Select(vc => new VentaCabDto
        {
            Id = vc.Id_VentaCab,
            Fecha = vc.FecRegistro,
            SubTotal = vc.SubTotal,
            Igv = vc.Igv,
            Total = vc.Total,
            Detalles = vc.Detalles.Select(det => new VentaDetDto
            {
                Id_VentaDet = det.Id_VentaDet,
                Id_VentaCab=det.Id_VentaCab,
                Id_Producto = det.Id_Producto,
                Cantidad = det.Cantidad,
                Precio = det.Precio,
                Sub_Total = det.Sub_Total,
                Igv = det.Igv,
                Total = det.Total
            }).ToList()
        }).ToList();

        return ventas;
    }
    public async Task RegistrarMovimientoVentaAsync(int id_ventaCab,List<CrearMovimientoDetDto> detalle)
    {
        var movimientoDto = new CrearMovimientoDto
        {
            TipoMovimiento = 2,
            DocumentoId = id_ventaCab,
            Detalles = detalle

        };

        var response = await _httpClientMov.PostAsJsonAsync("Registrar", movimientoDto);


        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("No se pudo registrar el movimiento para el producto");
        }
    }
    public async Task<ProductoDto?> ObtenerPrecioProductoAsync(int id_producto)
    {
        var response = await _httpClientPro.GetAsync($"{id_producto}");

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        if (root.TryGetProperty("data", out var data))
        {
            return JsonSerializer.Deserialize<ProductoDto>(
                data.GetRawText(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            
        }

        return null;
    }

}

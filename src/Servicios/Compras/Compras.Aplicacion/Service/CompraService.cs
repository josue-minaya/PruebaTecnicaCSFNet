using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Compras.Aplicacion.DTO;
using Compras.Dominio.Entidad;
using Compras.Dominio.IRepositorio;

namespace Compras.Aplicacion.Service
{
    public class CompraService
    {
        private readonly ICompraRepository _repo;
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpClientPro;

        public CompraService(ICompraRepository repo, IHttpClientFactory httpClientFactory)
        {
            _repo = repo;
            _httpClient = httpClientFactory.CreateClient("Movimientos");
            _httpClientPro = httpClientFactory.CreateClient("Productos");
        }

        public async Task CrearCompraAsync(CrearCompraDto dto)
        {
            decimal subtotal = 0;
            decimal igv = 0;
            decimal total = 0;

            var detalles = dto.Detalles.Select(d =>
            {
                var st = d.Precio * d.Cantidad;
                var igvDet = st * 0.18m;
                var totalDet = st + igvDet;

                subtotal += st;
                igv += igvDet;
                total += totalDet;

                return new CompraDet
                {
                    Id_Producto = d.Id_Producto,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio,
                    Sub_Total = st,
                    Igv = igvDet,
                    Total = totalDet
                };
            }).ToList();

            var compra = new CompraCab
            {
                SubTotal = subtotal,
                Igv = igv,
                Total = total,
                // Detalles = detalles
            };


            await _repo.CrearCompraCabAsync(compra);

            foreach (var detalle in detalles)
            {
                detalle.Id_CompraCab = compra.Id_CompraCab;
            }

            await _repo.CrearCompraDetAsync(detalles);

            var movimientoDetalles = detalles.Select(d => new CrearMovimientoDetDto
            {
                Id_Producto = d.Id_Producto,
                Cantidad = d.Cantidad
            }).ToList();

            await RegistrarMovimientoCompraAsync(compra.Id_CompraCab, movimientoDetalles);

            foreach (var detalle in detalles)
            {
                var nuevoCosto = detalle.Precio;
                var nuevoPrecioVenta = Math.Round(nuevoCosto * 1.35m, 2);

                var producto = await ObtenerProductoPorIdAsync(detalle.Id_Producto);
                if (producto != null)
                {
                    producto.Costo = nuevoCosto;
                    producto.PrecioVenta = nuevoPrecioVenta;
                    await ActualizarProductoAsync(producto);
                }
            }

        }

        public async Task<List<CompraCabDto>> ListarComprasAsync()
        {
            var entidades = await _repo.ListarCabAsync();

            var ventas = entidades.Select(vc => new CompraCabDto
            {
                Id = vc.Id_CompraCab,
                Fecha = vc.FecRegistro,
                SubTotal = vc.SubTotal,
                Igv = vc.Igv,
                Total = vc.Total,
                Detalles = vc.Detalles.Select(det => new CompraDetDto
                {
                    Id_CompraDet = det.Id_CompraDet,
                    Id_CompraCab = det.Id_CompraCab,
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
        public async Task RegistrarMovimientoCompraAsync(int id_compraCab, List<CrearMovimientoDetDto> detalle)
        {
            var movimientoDto = new CrearMovimientoDto
            {
                TipoMovimiento = 1,
                DocumentoId = id_compraCab,
                Detalles = detalle
            };

            var response = await _httpClient.PostAsJsonAsync("Registrar", movimientoDto);


            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("No se pudo registrar el movimiento para el producto");
            }
        }

        public async Task<ProductoDto> ObtenerProductoPorIdAsync(int idProducto)
        {
            var response = await _httpClientPro.GetAsync($"{idProducto}");

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

        public async Task ActualizarProductoAsync(ProductoDto producto)
        {
            var response = await _httpClientPro.PutAsJsonAsync($"{producto.Id_producto}", producto);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("No se pudo actualizar el producto");
            }
        }
    }

    }

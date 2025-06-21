using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movimientos.Aplicacion.DTO;
using Movimientos.Dominio.Entidad;
using Movimientos.Dominio.IRepositorio;

namespace Movimientos.Aplicacion.Service
{
    public class MovimientoService
    {
        private readonly IMovimientoRepository _repo;

        public MovimientoService(IMovimientoRepository repo)
        {
            _repo = repo;
        }

        public async Task CrearMovimientoAsync(CrearMovimientoDto dto)
        {

            var detalles = dto.Detalles.Select(d =>
            {
                return new MovimientoDet
                {
                    Id_Producto = d.Id_Producto,
                    Cantidad = d.Cantidad,

                };
            }).ToList();

            var movimiento = new MovimientoCab
            {
                Id_TipoMovimiento=dto.TipoMovimiento,
                Id_DocumentoOrigen=dto.DocumentoId,    

            };


            await _repo.CrearMovimientoCabAsync(movimiento);

            foreach (var detalle in detalles)
            {
                detalle.Id_Movimientocab = movimiento.Id_MovimientoCab;

            }

            await _repo.CrearMovimientoDetAsync(detalles);


        }

        public async Task<List<MovimientoCabDto>> ListarMovimientoAsync()
        {
            var entidades = await _repo.ListarCabAsync();

            var movimientoDto = entidades.Select(vc => new MovimientoCabDto
            {
                Id_MovimientoCab = vc.Id_MovimientoCab,
                Id_DocumentoOrigen = vc.Id_DocumentoOrigen,
                Fec_Registro = vc.Fec_Registro,
                Id_TipoMovimiento=vc.Id_TipoMovimiento == 1 ? "ENTRADA" : "SALIDA",


                Detalles = vc.Detalles.Select(det => new MovimientoDetDto
                {
                    Id_MovimientoDet = det.Id_MovimientoDet,
                    Id_Movimientocab = det.Id_Movimientocab,
                    Id_Producto = det.Id_Producto,
                    Cantidad = det.Cantidad

                }).ToList()
            }).ToList();

            return movimientoDto;
        }
    }
}

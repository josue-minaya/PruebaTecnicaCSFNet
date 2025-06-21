using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movimientos.Dominio.Entidad;
using Movimientos.Dominio.IRepositorio;
using Movimientos.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Movimientos.Aplicacion.DTO;

namespace Movimientos.Infraestructura.Repositorio
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly MovimientoDbContext _context;

        public MovimientoRepository(MovimientoDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovimientoCab>> ListarCabAsync()
        {


            //var resultado = await _context.MovimientoCab
            //            .Where(mc => mc.Detalles.Any(d => d.Id_Producto == productoId))
            //            .Include(mc => mc.Detalles)
            //            .ToListAsync();

            var resultado = await _context.MovimientoCab
                         .Include(mc => mc.Detalles) // Asegúrate de incluir los detalles
                         .Select(mc => new MovimientoCab
                         {
                             Id_MovimientoCab = mc.Id_MovimientoCab,
                             Fec_Registro = mc.Fec_Registro,
                             Id_TipoMovimiento = mc.Id_TipoMovimiento,
                             Id_DocumentoOrigen = mc.Id_DocumentoOrigen,
                             Detalles = mc.Detalles.ToList() // ← TODOS los detalles, sin filtrar
                         })
                         .ToListAsync();

            return resultado;
        }

        public async Task<MovimientoCab> CrearMovimientoCabAsync(MovimientoCab movimiento)
        {
            _context.MovimientoCab.Add(movimiento);
            await _context.SaveChangesAsync();

            return movimiento;
        }

        public async Task CrearMovimientoDetAsync(List<MovimientoDet> MovimientoDet)
        {

            _context.MovimientoDet.AddRange(MovimientoDet);
            await _context.SaveChangesAsync();

        }

    }
}

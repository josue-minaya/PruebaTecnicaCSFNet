using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compras.Dominio.Entidad;
using Compras.Dominio.IRepositorio;
using Compras.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Compras.Infraestructura.Repositorio
{
    public class CompraRepository : ICompraRepository
    {
        private readonly CompraDbContext _context;

        public CompraRepository(CompraDbContext context)
        {
            _context = context;
        }

        public async Task<List<CompraCab>> ListarCabAsync()
        {
            return await _context.CompraCab
                   .Include(vc => vc.Detalles)
                   .AsNoTracking()
                   .ToListAsync();
        }

        public async Task<CompraCab> CrearCompraCabAsync(CompraCab venta)
        {
            _context.CompraCab.Add(venta);
            await _context.SaveChangesAsync();

            return venta;
        }

        public async Task CrearCompraDetAsync(List<CompraDet> ventaDet)
        {

            _context.CompraDet.AddRange(ventaDet);
            await _context.SaveChangesAsync();

        }

    }
}

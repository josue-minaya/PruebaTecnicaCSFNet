using Microsoft.EntityFrameworkCore;
using Ventas.Aplicacion.DTO;
using Ventas.Dominio.Entidad;
using Ventas.Dominio.IRepositorio;
using Ventas.Infraestructura.Persistencia;

namespace Ventas.Infraestructura.Repositorios;

public class VentaRepository : IVentaRepository
{
    private readonly VentasDbContext _context;

    public VentaRepository(VentasDbContext context)
    {
        _context = context;
    }

    public async Task<List<VentaCab>> ListarCabAsync()
    {
        return await _context.VentaCab
               .Include(vc => vc.Detalles)
               .AsNoTracking()
               .ToListAsync();
    }

    public async Task<VentaCab> CrearVentaCabAsync(VentaCab venta)
    {
        _context.VentaCab.Add(venta);
        await _context.SaveChangesAsync();

        return venta;
    }

    public async Task CrearVentaDetAsync(List<VentaDet> ventaDet)
    {

        _context.VentaDet.AddRange(ventaDet);
        await _context.SaveChangesAsync();

    }
}

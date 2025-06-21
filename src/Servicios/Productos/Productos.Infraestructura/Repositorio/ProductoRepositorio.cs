using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Productos.Dominio.Entidad;
using Productos.Dominio.Repositorio;
using Productos.Infraestructura.Persistencia;

namespace Productos.Infraestructura.Repositorio
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly ProductosDbContext _context;

        public ProductoRepositorio(ProductosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ListarAsync() =>
            await _context.Productos.ToListAsync();

        public async Task<Producto?> ObtenerPorIdAsync(int id) =>
            await _context.Productos.FindAsync(id);

        public async Task CrearAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

    }
}
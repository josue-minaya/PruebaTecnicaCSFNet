using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Productos.Dominio.Entidad;

namespace Productos.Dominio.Repositorio
{
    public interface IProductoRepositorio
    {
        Task<List<Producto>> ListarAsync();
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Producto producto);
        Task ActualizarAsync(Producto producto);
    }
}

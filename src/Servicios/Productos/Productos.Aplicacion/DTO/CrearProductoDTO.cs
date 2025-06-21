using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.Aplicacion.DTO
{
    public class CrearProductoDto
    {
        public string Nombre_producto { get; set; } = string.Empty;
        public string NroLote { get; set; } = string.Empty;
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}

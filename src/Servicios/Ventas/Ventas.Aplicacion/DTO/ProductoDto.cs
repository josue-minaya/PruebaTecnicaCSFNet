using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.DTO
{
    public class ProductoDto
    {
        public int Id_producto { get; set; }
        public string Nombre_producto { get; set; } = string.Empty;
        public string NroLote { get; set; } = string.Empty;
        public DateTime Fec_registro { get; set; } = DateTime.Now;
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}

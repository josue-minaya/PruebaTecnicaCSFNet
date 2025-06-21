using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.DTO
{
    public class VentaCabDto
    {
            public int Id { get; set; }
            public DateTime Fecha { get; set; }
            public decimal SubTotal { get; set; }
            public decimal Igv { get; set; }
            public decimal Total { get; set; }
            public List<VentaDetDto> Detalles { get; set; } = new();
        
    }
}

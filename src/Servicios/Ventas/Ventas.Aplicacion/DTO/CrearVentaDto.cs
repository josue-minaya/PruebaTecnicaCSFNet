using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.DTO
{
    public class CrearVentaDto
    {
        public List<CrearVentaDetDto> Detalles { get; set; } = new();
    }
}

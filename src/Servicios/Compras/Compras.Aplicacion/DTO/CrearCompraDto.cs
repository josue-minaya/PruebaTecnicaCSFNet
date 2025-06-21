using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compras.Aplicacion.DTO
{
    public class CrearCompraDto
    {
        public List<CrearCompraDetDto> Detalles { get; set; } = new();

    }
}

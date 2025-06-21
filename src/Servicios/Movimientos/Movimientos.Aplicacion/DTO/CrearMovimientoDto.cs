using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimientos.Aplicacion.DTO
{
    public class CrearMovimientoDto
    {
        public int TipoMovimiento { get; set; } 
        public int DocumentoId { get; set; }
        public List<CrearMovimientoDetDto> Detalles { get; set; } = new();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movimientos.Dominio.Entidad;

namespace Movimientos.Aplicacion.DTO
{
    public class MovimientoCabDto
    {
        public int Id_MovimientoCab { get; set; }

        public DateTime Fec_Registro { get; set; } = DateTime.Now;

        public string Id_TipoMovimiento { get; set; } = string.Empty;

        public int Id_DocumentoOrigen { get; set; }

        public List<MovimientoDetDto> Detalles { get; set; } = new();

    }
}

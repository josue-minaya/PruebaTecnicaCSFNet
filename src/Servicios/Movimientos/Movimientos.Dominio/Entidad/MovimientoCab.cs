using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimientos.Dominio.Entidad
{
    public class MovimientoCab
    {
        public int Id_MovimientoCab { get; set; }

        public DateTime Fec_Registro { get; set; } = DateTime.Now;

        public int Id_TipoMovimiento { get; set; }

        public int Id_DocumentoOrigen { get; set; }

        public List<MovimientoDet> Detalles { get; set; } = new();
    }
}

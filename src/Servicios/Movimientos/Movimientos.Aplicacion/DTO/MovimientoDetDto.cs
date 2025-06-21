using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimientos.Aplicacion.DTO
{
    public class MovimientoDetDto
    {
        public int Id_MovimientoDet { get; set; }

        public int Id_Movimientocab { get; set; }

        public int Id_Producto { get; set; }

        public int Cantidad { get; set; }

    }
}

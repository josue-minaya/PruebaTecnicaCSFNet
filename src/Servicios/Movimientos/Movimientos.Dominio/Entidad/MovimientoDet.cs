using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimientos.Dominio.Entidad
{
    public class MovimientoDet
    {
        public int Id_MovimientoDet { get; set; }

        public int Id_Movimientocab { get; set; }

        public int Id_Producto { get; set; }

        public int Cantidad { get; set; }

        public MovimientoCab MovimientoCab { get; set; } = null!;
    }
}
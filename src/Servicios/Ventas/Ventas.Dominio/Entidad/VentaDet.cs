using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Dominio.Entidad
{

    public class VentaDet
    {
        public int Id_VentaDet { get; set; } 
        public int Id_VentaCab { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Sub_Total { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public VentaCab VentaCab { get; set; } = null!;


    }
}

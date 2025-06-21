using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Dominio.Entidad
{
    public class VentaCab
    {
        public int Id_VentaCab { get; set; }
        public DateTime FecRegistro { get; set; } = DateTime.Now;
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }

        public List<VentaDet> Detalles { get; set; } = new();
    }
}

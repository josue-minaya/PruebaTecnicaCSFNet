using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compras.Dominio.Entidad
{
    public class CompraCab
    {
        public int Id_CompraCab { get; set; }

        public DateTime FecRegistro { get; set; } = DateTime.Now;

        public decimal SubTotal { get; set; }

        public decimal Igv { get; set; }

        public decimal Total { get; set; }

        // Relación 1:N
        public List<CompraDet> Detalles { get; set; } = new();
    }
}

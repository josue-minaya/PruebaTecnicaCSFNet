using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compras.Aplicacion.DTO
{
    public class CompraCabDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public List<CompraDetDto> Detalles { get; set; } = new();

    }
}

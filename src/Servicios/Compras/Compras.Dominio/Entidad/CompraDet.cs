using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compras.Dominio.Entidad
{
    public class CompraDet
    {
        public int Id_CompraDet { get; set; }

        public int Id_CompraCab { get; set; }

        public int Id_Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public decimal Sub_Total { get; set; }

        public decimal Igv { get; set; }

        public decimal Total { get; set; }

        public CompraCab CompraCab { get; set; } = null!;
    }
}
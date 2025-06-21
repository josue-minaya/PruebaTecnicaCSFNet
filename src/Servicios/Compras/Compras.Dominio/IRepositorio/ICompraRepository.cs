using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compras.Dominio.Entidad;

namespace Compras.Dominio.IRepositorio
{
    public interface ICompraRepository
    {
        Task<List<CompraCab>> ListarCabAsync();
        Task<CompraCab> CrearCompraCabAsync(CompraCab compra);
        Task CrearCompraDetAsync(List<CompraDet> CompraDet);

    }
}

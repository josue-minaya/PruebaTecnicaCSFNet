using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Dominio.Entidad;

namespace Ventas.Dominio.IRepositorio
{
    public interface IVentaRepository
    {
        Task<List<VentaCab>> ListarCabAsync();
        Task<VentaCab> CrearVentaCabAsync(VentaCab venta);
        Task CrearVentaDetAsync(List<VentaDet> ventaDet);

    }
}

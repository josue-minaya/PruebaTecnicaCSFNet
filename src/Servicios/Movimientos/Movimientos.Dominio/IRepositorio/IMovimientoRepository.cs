using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movimientos.Dominio.Entidad;

namespace Movimientos.Dominio.IRepositorio
{
    public interface IMovimientoRepository
    {
        Task<List<MovimientoCab>> ListarCabAsync();
        Task<MovimientoCab> CrearMovimientoCabAsync(MovimientoCab movimiento);
        Task CrearMovimientoDetAsync(List<MovimientoDet> MovimientoDet);

    }
}

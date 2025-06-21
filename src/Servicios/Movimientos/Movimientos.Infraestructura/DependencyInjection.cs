using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movimientos.Aplicacion.Service;
using Movimientos.Dominio.IRepositorio;
using Movimientos.Infraestructura.Persistencia;
using Movimientos.Infraestructura.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Movimientos.Infraestructura
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // Contexto de BD
            services.AddDbContext<MovimientoDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            // Repositorios y servicios
            services.AddScoped<IMovimientoRepository, MovimientoRepository>();
            services.AddScoped<MovimientoService>();


            return services;
        }
    }
}

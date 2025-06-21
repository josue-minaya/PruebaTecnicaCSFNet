using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ventas.Aplicacion.Servicios;
using Ventas.Dominio.IRepositorio;
using Ventas.Infraestructura.Persistencia;
using Ventas.Infraestructura.Repositorios;

namespace Ventas.Infraestructura
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // Contexto de BD
            services.AddDbContext<VentasDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            // Repositorios y servicios
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddScoped<VentaService>();


            return services;
        }
    }
}

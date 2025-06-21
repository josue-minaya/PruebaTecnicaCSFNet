using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compras.Aplicacion.Service;
using Compras.Dominio.IRepositorio;
using Compras.Infraestructura.Persistencia;
using Compras.Infraestructura.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Compras.Infraestructura
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // Contexto de BD
            services.AddDbContext<CompraDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            // Repositorios y servicios
            services.AddScoped<ICompraRepository, CompraRepository>();
            services.AddScoped<CompraService>();


            return services;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Productos.Aplicacion.Mapper;
using Productos.Aplicacion.Service;
using Productos.Dominio.Repositorio;
using Productos.Infraestructura.Persistencia;
using Productos.Infraestructura.Repositorio;

namespace Productos.Infraestructura
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // Contexto de BD
            services.AddDbContext<ProductosDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // AutoMapper
             services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // Repositorios y servicios
            services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
            services.AddScoped<ProductoService>();


            return services;
        }
    }
}

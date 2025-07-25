﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Productos.Infraestructura.Extensions
{
    public static class CorsServiceExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services, string[] origenesPermitidos)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", policy =>
                {
                    policy
                        .WithOrigins(origenesPermitidos)
                        .WithMethods("POST","GET","PUT")
                        .WithHeaders("*");
                });
            });

            return services;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Productos.Aplicacion.DTO;
using Productos.Dominio.Entidad;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Productos.Aplicacion.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoDTO, Producto>();
            CreateMap<Producto, CrearProductoDto>();
            CreateMap<CrearProductoDto, Producto>();
            CreateMap<Producto, Producto>();
        }
    }
}
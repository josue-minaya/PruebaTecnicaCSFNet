using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Productos.Aplicacion.DTO;
using Productos.Dominio.Entidad;
using Productos.Dominio.Repositorio;

namespace Productos.Aplicacion.Service
{
    public class ProductoService
    {
        private readonly IProductoRepositorio _repo;
        private readonly IMapper _mapper;

        public ProductoService(IProductoRepositorio repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> ListarProductosAsync()
        {
            var productos = await _repo.ListarAsync();

            return _mapper.Map<List<ProductoDTO>>(productos); 
        }

        public async Task<Producto> CrearProductoAsync(CrearProductoDto dto)
        {
            var producto = _mapper.Map<Producto>(dto);

            await _repo.CrearAsync(producto);

            return producto;
        }

        public async Task ActualizarProductoAsync(int id, CrearProductoDto dto)
        {
            var producto = await _repo.ObtenerPorIdAsync(id);
            if (producto is null) throw new Exception("Producto no encontrado");

            _mapper.Map(dto, producto);


            await _repo.ActualizarAsync(producto);
        }

        public async Task<Producto> ObtenerProductoPorIdAsync(int id)
        {
            var producto = await _repo.ObtenerPorIdAsync(id);
            if (producto is null) throw new Exception("Producto no encontrado");
            return producto;
        }
    }
}

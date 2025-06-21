
using Microsoft.EntityFrameworkCore;
using Productos.Dominio.Entidad;

namespace Productos.Infraestructura.Persistencia
{
    public class ProductosDbContext : DbContext
    {
        public ProductosDbContext(DbContextOptions<ProductosDbContext> options)
            : base(options) { }

        public DbSet<Producto> Productos => Set<Producto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasKey(p => p.Id_producto);
        }
    }
}

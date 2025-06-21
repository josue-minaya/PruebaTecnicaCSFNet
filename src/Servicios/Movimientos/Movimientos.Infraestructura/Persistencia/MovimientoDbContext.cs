using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Movimientos.Dominio.Entidad;
using Microsoft.EntityFrameworkCore;

namespace Movimientos.Infraestructura.Persistencia
{
    public class MovimientoDbContext : DbContext
    {
        public MovimientoDbContext(DbContextOptions<MovimientoDbContext> options) : base(options) { }

        public DbSet<MovimientoCab> MovimientoCab => Set<MovimientoCab>();
        public DbSet<MovimientoDet> MovimientoDet => Set<MovimientoDet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovimientoCab>()
            .HasKey(vc => vc.Id_MovimientoCab);

            modelBuilder.Entity<MovimientoDet>()
                .HasKey(vd => vd.Id_MovimientoDet);

            modelBuilder.Entity<MovimientoCab>()
                .HasMany(vc => vc.Detalles)
                .WithOne(vd => vd.MovimientoCab)
                .HasForeignKey(vd => vd.Id_Movimientocab)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
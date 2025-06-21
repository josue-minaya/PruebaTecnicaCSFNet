using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Compras.Dominio.Entidad;
using Microsoft.EntityFrameworkCore;

namespace Compras.Infraestructura.Persistencia
{
    public class CompraDbContext : DbContext
    {
        public CompraDbContext(DbContextOptions<CompraDbContext> options) : base(options) { }

        public DbSet<CompraCab> CompraCab => Set<CompraCab>();
        public DbSet<CompraDet> CompraDet => Set<CompraDet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompraCab>()
            .HasKey(vc => vc.Id_CompraCab);

            modelBuilder.Entity<CompraDet>()
                .HasKey(vd => vd.Id_CompraDet);

            modelBuilder.Entity<CompraCab>()
                .HasMany(vc => vc.Detalles)
                .WithOne(vd => vd.CompraCab)
                .HasForeignKey(vd => vd.Id_CompraCab)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
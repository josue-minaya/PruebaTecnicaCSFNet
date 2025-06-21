using Microsoft.EntityFrameworkCore;
using Ventas.Dominio.Entidad;

namespace Ventas.Infraestructura.Persistencia;

public class VentasDbContext : DbContext
{
    public VentasDbContext(DbContextOptions<VentasDbContext> options) : base(options) { }

    public DbSet<VentaCab> VentaCab => Set<VentaCab>();
    public DbSet<VentaDet> VentaDet => Set<VentaDet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VentaCab>()
        .HasKey(vc => vc.Id_VentaCab);

        modelBuilder.Entity<VentaDet>()
            .HasKey(vd => vd.Id_VentaDet);

        modelBuilder.Entity<VentaCab>()
            .HasMany(vc => vc.Detalles)
            .WithOne(vd => vd.VentaCab)
            .HasForeignKey(vd => vd.Id_VentaCab)  // Usa el nombre correcto
            .OnDelete(DeleteBehavior.Cascade);


    }
}

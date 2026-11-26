using Microsoft.EntityFrameworkCore;
using ProyectoAlgoritmos.Models;

namespace ProyectoAlgoritmos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Activo> Activos => Set<Activo>();

    public DbSet<Reparacion> Reparaciones => Set<Reparacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activo>(entity =>
        {
            entity.HasIndex(a => a.CodigoActivo).IsUnique();
            entity.HasIndex(a => a.NumeroSerie).IsUnique();

            entity.Property(a => a.CodigoActivo).HasMaxLength(50);
            entity.Property(a => a.Marca).HasMaxLength(100);
            entity.Property(a => a.Modelo).HasMaxLength(100);
            entity.Property(a => a.NumeroSerie).HasMaxLength(100);
            entity.Property(a => a.NumeroFactura).HasMaxLength(100);
            entity.Property(a => a.Proveedor).HasMaxLength(200);
            entity.Property(a => a.Direccion).HasMaxLength(300);
            entity.Property(a => a.PersonaContacto).HasMaxLength(200);
            entity.Property(a => a.Telefono).HasMaxLength(50);
            entity.Property(a => a.Monto).HasPrecision(18, 2);

            entity.HasMany(a => a.Reparaciones)
                .WithOne(r => r.Activo)
                .HasForeignKey(r => r.ActivoId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        modelBuilder.Entity<Reparacion>(entity =>
        {
            entity.Property(r => r.Diagnostico).HasMaxLength(1000);
            entity.Property(r => r.Proveedor).HasMaxLength(200);
            entity.Property(r => r.Factura).HasMaxLength(100);
            entity.Property(r => r.Monto).HasPrecision(18, 2);
        });
    }
}

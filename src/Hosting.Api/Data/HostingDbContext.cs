using Hosting.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hosting.Api.Data;

public class HostingDbContext : DbContext
{
    public HostingDbContext(DbContextOptions<HostingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Rol> Roles { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.ToTable("roles");

            entity.HasKey(r => r.IdRol);

            entity.Property(r => r.IdRol)
                .HasColumnName("id_rol");

            entity.Property(r => r.Codigo)
                .HasColumnName("codigo")
                .HasMaxLength(30)
                .IsRequired();

            entity.Property(r => r.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");

            entity.HasKey(u => u.IdUsuario);

            entity.Property(u => u.IdUsuario)
                .HasColumnName("id_usuario");

            entity.Property(u => u.IdRol)
                .HasColumnName("id_rol");

            entity.Property(u => u.NombreUsuario)
                .HasColumnName("nombre_usuario")
                .HasMaxLength(80)
                .IsRequired();

            entity.Property(u => u.Correo)
                .HasColumnName("correo")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(u => u.ContrasenaHash)
                .HasColumnName("contrasena_hash");

            entity.Property(u => u.Activo)
                .HasColumnName("activo")
                .IsRequired();

            entity.Property(u => u.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol);
        });
    }
}
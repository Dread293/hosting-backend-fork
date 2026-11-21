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

    public DbSet<Juego> Juegos { get; set; }

    public DbSet<VersionJuego> VersionesJuegos { get; set; }

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


        modelBuilder.Entity<Juego>(entity =>
        {
            entity.ToTable("juegos");

            entity.HasKey(j => j.IdJuego);

            entity.Property(j => j.IdJuego)
                .HasColumnName("id_juego");

            entity.Property(j => j.Codigo)
                .HasColumnName("codigo")
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(j => j.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(j => j.Descripcion)
                .HasColumnName("descripcion");

            entity.Property(j => j.Activo)
                .HasColumnName("activo")
                .IsRequired();

            entity.HasIndex(j => j.Codigo)
                .IsUnique();
        });

        modelBuilder.Entity<VersionJuego>(entity =>
        {
            entity.ToTable("versiones_juegos");

            entity.HasKey(v => v.IdVersion);

            entity.Property(v => v.IdVersion)
                .HasColumnName("id_version");

            entity.Property(v => v.IdJuego)
                .HasColumnName("id_juego");

            entity.Property(v => v.Version)
                .HasColumnName("version")
                .HasMaxLength(30)
                .IsRequired();

            entity.Property(v => v.EtiquetaImagen)
                .HasColumnName("etiqueta_imagen")
                .HasMaxLength(100);

            entity.Property(v => v.Activa)
                .HasColumnName("activa")
                .IsRequired();

            entity.HasOne(v => v.Juego)
                .WithMany(j => j.Versiones)
                .HasForeignKey(v => v.IdJuego);

            entity.HasIndex(v => new { v.IdJuego, v.Version })
                .IsUnique();
        });
    }
}
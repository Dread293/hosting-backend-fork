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

    public DbSet<Servidor> Servidores { get; set; }

    public DbSet<ConfiguracionServidor> ConfiguracionesServidores { get; set; }

    public DbSet<PuertoServidor> PuertosServidores { get; set; }

    public DbSet<AlmacenamientoServidor> AlmacenamientosServidores { get; set; }

    public DbSet<EventoServidor> EventosServidores { get; set; }

    public DbSet<CopiaSeguridad> CopiasSeguridad { get; set; }

    public DbSet<IdentidadExterna> IdentidadesExternas { get; set; }

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
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Restrict);
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

        modelBuilder.Entity<Servidor>(entity =>
        {
            entity.ToTable("servidores");

            entity.HasKey(s => s.IdServidor);

            entity.Property(s => s.IdServidor)
                .HasColumnName("id_servidor");

            entity.Property(s => s.IdUsuario)
                .HasColumnName("id_usuario");

            entity.Property(s => s.IdVersion)
                .HasColumnName("id_version");

            entity.Property(s => s.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(s => s.IdContenedor)
                .HasColumnName("id_contenedor")
                .HasMaxLength(150);

            entity.Property(s => s.EstadoDeseado)
                .HasColumnName("estado_deseado")
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(s => s.EstadoActual)
                .HasColumnName("estado_actual")
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(s => s.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.Property(s => s.FechaActualizacion)
                .HasColumnName("fecha_actualizacion")
                .HasColumnType("timestamp with time zone");

            entity.Property(s => s.FechaEliminacion)
                .HasColumnName("fecha_eliminacion")
                .HasColumnType("timestamp with time zone");

            entity.HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.VersionJuego)
                .WithMany()
                .HasForeignKey(s => s.IdVersion)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ConfiguracionServidor>(entity =>
        {
            entity.ToTable("configuraciones_servidores");

            entity.HasKey(c => c.IdServidor);

            entity.Property(c => c.IdServidor)
                .HasColumnName("id_servidor");

            entity.Property(c => c.NombreMundo)
                .HasColumnName("nombre_mundo")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(c => c.TamanoMundo)
                .HasColumnName("tamano_mundo")
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(c => c.Dificultad)
                .HasColumnName("dificultad")
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(c => c.MaxJugadores)
                .HasColumnName("max_jugadores")
                .IsRequired();

            entity.Property(c => c.ContrasenaCifrada)
                .HasColumnName("contrasena_cifrada");

            entity.Property(c => c.Seguro)
                .HasColumnName("seguro")
                .IsRequired();

            entity.HasOne(c => c.Servidor)
                .WithOne(s => s.Configuracion)
                .HasForeignKey<ConfiguracionServidor>(c => c.IdServidor);
        });

        modelBuilder.Entity<PuertoServidor>(entity =>
        {
            entity.ToTable("puertos_servidores");

            entity.HasKey(p => p.IdPuerto);

            entity.Property(p => p.IdPuerto)
                .HasColumnName("id_puerto");

            entity.Property(p => p.IdServidor)
                .HasColumnName("id_servidor");

            entity.Property(p => p.PuertoHost)
                .HasColumnName("puerto_host")
                .IsRequired();

            entity.Property(p => p.PuertoContenedor)
                .HasColumnName("puerto_contenedor")
                .IsRequired();

            entity.Property(p => p.Protocolo)
                .HasColumnName("protocolo")
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(p => p.Activo)
                .HasColumnName("activo")
                .IsRequired();

            entity.HasOne(p => p.Servidor)
                .WithOne(s => s.Puerto)
                .HasForeignKey<PuertoServidor>(p => p.IdServidor);

            entity.HasIndex(p => p.IdServidor)
                .IsUnique();

            entity.HasIndex(p => p.PuertoHost)
                .IsUnique()
                .HasFilter("activo = TRUE");
        });

        modelBuilder.Entity<AlmacenamientoServidor>(entity =>
        {
            entity.ToTable("almacenamientos_servidores");

            entity.HasKey(a => a.IdAlmacenamiento);

            entity.Property(a => a.IdAlmacenamiento)
                .HasColumnName("id_almacenamiento");

            entity.Property(a => a.IdServidor)
                .HasColumnName("id_servidor");

            entity.Property(a => a.ClaveAlmacenamiento)
                .HasColumnName("clave_almacenamiento");

            entity.Property(a => a.RutaAlmacenamiento)
                .HasColumnName("ruta_almacenamiento")
                .IsRequired();

            entity.Property(a => a.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.HasOne(a => a.Servidor)
                .WithOne(s => s.Almacenamiento)
                .HasForeignKey<AlmacenamientoServidor>(a => a.IdServidor);

            entity.HasIndex(a => a.IdServidor)
                .IsUnique();

            entity.HasIndex(a => a.ClaveAlmacenamiento)
                .IsUnique();
        });

        modelBuilder.Entity<EventoServidor>(entity =>
        {
            entity.ToTable("eventos_servidores");

            entity.HasKey(e => e.IdEvento);

            entity.Property(e => e.IdEvento)
                .HasColumnName("id_evento")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.IdServidor)
                .HasColumnName("id_servidor");

            entity.Property(e => e.IdUsuario)
                .HasColumnName("id_usuario");

            entity.Property(e => e.TipoEvento)
                .HasColumnName("tipo_evento")
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Resultado)
                .HasColumnName("resultado")
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.Detalles)
                .HasColumnName("detalles")
                .HasColumnType("jsonb");

            entity.Property(e => e.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.HasOne(e => e.Servidor)
                .WithMany(s => s.Eventos)
                .HasForeignKey(e => e.IdServidor);

            entity.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.IdUsuario);
        });

        modelBuilder.Entity<CopiaSeguridad>(entity =>
        {
            entity.ToTable("copias_seguridad");

            entity.HasKey(c => c.IdCopia);

            entity.Property(c => c.IdCopia)
                .HasColumnName("id_copia");

            entity.Property(c => c.IdServidor)
                .HasColumnName("id_servidor");

            entity.Property(c => c.NombreArchivo)
                .HasColumnName("nombre_archivo")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(c => c.RutaAlmacenamiento)
                .HasColumnName("ruta_almacenamiento")
                .IsRequired();

            entity.Property(c => c.TamanoArchivo)
                .HasColumnName("tamano_archivo")
                .IsRequired();

            entity.Property(c => c.Estado)
                .HasColumnName("estado")
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(c => c.FechaCopia)
                .HasColumnName("fecha_copia")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.HasOne(c => c.Servidor)
                .WithMany(s => s.CopiasSeguridad)
                .HasForeignKey(c => c.IdServidor);
        });

        modelBuilder.Entity<IdentidadExterna>(entity =>
        {
            entity.ToTable("identidades_externas");

            entity.HasKey(e => e.IdIdentidad);

            entity.Property(e => e.IdIdentidad)
                .HasColumnName("id_identidad");

            entity.Property(e => e.IdUsuario)
                .HasColumnName("id_usuario");

            entity.Property(e => e.Proveedor)
                .HasColumnName("proveedor")
                .HasMaxLength(30)
                .IsRequired();

            entity.Property(e => e.IdUsuarioProveedor)
                .HasColumnName("id_usuario_proveedor")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.CorreoProveedor)
                .HasColumnName("correo_proveedor")
                .HasMaxLength(255);

            entity.Property(e => e.FechaVinculacion)
                .HasColumnName("fecha_vinculacion")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.HasIndex(e => new
            {
                e.Proveedor,
                e.IdUsuarioProveedor
            })
            .IsUnique()
            .HasDatabaseName("uq_identidades_externas_proveedor_usuario");

            entity.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.IdUsuario);
        });
    }
}
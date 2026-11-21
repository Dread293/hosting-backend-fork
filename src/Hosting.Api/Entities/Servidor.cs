namespace Hosting.Api.Entities;

public class Servidor
{
    public Guid IdServidor { get; set; }

    public Guid IdUsuario { get; set; }

    public int IdVersion { get; set; }

    public string Nombre { get; set; } = null!;

    public string? IdContenedor { get; set; }

    public string EstadoDeseado { get; set; } = null!;

    public string EstadoActual { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public DateTime? FechaEliminacion { get; set; }

    // Relaciones
    public Usuario Usuario { get; set; } = null!;

    public VersionJuego VersionJuego { get; set; } = null!;

    public ConfiguracionServidor Configuracion { get; set; } = null!;

    public PuertoServidor Puerto { get; set; } = null!;

    public AlmacenamientoServidor Almacenamiento { get; set; } = null!;

    public ICollection<EventoServidor> Eventos { get; set; }
        = new List<EventoServidor>();

    public ICollection<CopiaSeguridad> CopiasSeguridad { get; set; }
        = new List<CopiaSeguridad>();
}
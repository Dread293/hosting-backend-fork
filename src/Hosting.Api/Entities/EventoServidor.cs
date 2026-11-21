namespace Hosting.Api.Entities;

public class EventoServidor
{
    public long IdEvento { get; set; }

    public Guid IdServidor { get; set; }

    public Guid? IdUsuario { get; set; }

    public string TipoEvento { get; set; } = null!;

    public string Resultado { get; set; } = null!;

    public string? Detalles { get; set; }

    public DateTime FechaCreacion { get; set; }

    // Relaciones
    public Servidor Servidor { get; set; } = null!;

    public Usuario? Usuario { get; set; }
}
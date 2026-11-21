namespace Hosting.Api.Entities;

public class AlmacenamientoServidor
{
    public Guid IdAlmacenamiento { get; set; }

    public Guid IdServidor { get; set; }

    public Guid ClaveAlmacenamiento { get; set; }

    public string RutaAlmacenamiento { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    // Relación
    public Servidor Servidor { get; set; } = null!;
}
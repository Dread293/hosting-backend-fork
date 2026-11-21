namespace Hosting.Api.Entities;

public class CopiaSeguridad
{
    public Guid IdCopia { get; set; }

    public Guid IdServidor { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public string RutaAlmacenamiento { get; set; } = null!;

    public long TamanoArchivo { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaCopia { get; set; }

    public Servidor Servidor { get; set; } = null!;
}
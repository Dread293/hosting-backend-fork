namespace Hosting.Api.Entities;

public class PuertoServidor
{
    public Guid IdPuerto { get; set; }

    public Guid IdServidor { get; set; }

    public int PuertoHost { get; set; }

    public int PuertoContenedor { get; set; }

    public string Protocolo { get; set; } = null!;

    public bool Activo { get; set; }

    public Servidor Servidor { get; set; } = null!;
}
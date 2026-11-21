namespace Hosting.Api.Entities;

public class ConfiguracionServidor
{
    public Guid IdServidor { get; set; }

    public string NombreMundo { get; set; } = null!;

    public string TamanoMundo { get; set; } = null!;

    public string Dificultad { get; set; } = null!;

    public short MaxJugadores { get; set; }

    public string? ContrasenaCifrada { get; set; }

    public bool Seguro { get; set; }

    // Relación
    public Servidor Servidor { get; set; } = null!;
}
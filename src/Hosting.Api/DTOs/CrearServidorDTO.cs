namespace Hosting.Api.DTOs;

public class CrearServidorDTO
{
    public string Nombre { get; set; } = null!;

    public string NombreMundo { get; set; } = null!;

    public string TamanoMundo { get; set; } = null!;

    public string Dificultad { get; set; } = null!;

    public short MaxJugadores { get; set; }

    public string? Contrasena { get; set; }
}
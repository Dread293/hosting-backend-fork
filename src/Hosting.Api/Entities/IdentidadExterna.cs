namespace Hosting.Api.Entities;

public class IdentidadExterna
{
    public Guid IdIdentidad { get; set; }

    public Guid IdUsuario { get; set; }

    public string Proveedor { get; set; } = null!;

    public string IdUsuarioProveedor { get; set; } = null!;

    public string? CorreoProveedor { get; set; }

    public DateTime FechaVinculacion { get; set; }

    // Relación
    public Usuario Usuario { get; set; } = null!;
}
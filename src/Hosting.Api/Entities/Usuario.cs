namespace Hosting.Api.Entities;

public class Usuario
{
    public Guid IdUsuario { get; set; }

    public short IdRol { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? ContrasenaHash { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public Rol Rol { get; set; } = null!;
}
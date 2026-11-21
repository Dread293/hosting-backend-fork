namespace Hosting.Api.Entities;

public class Rol
{
    public short IdRol { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
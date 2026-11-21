using Hosting.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hosting.Api.Data;

public static class DatabaseSeeder
{
    public static async Task InitializeAsync(HostingDbContext context)
    {
        // 1. Crear los roles iniciales
        if (!await context.Roles.AnyAsync(r => r.Codigo == "ADMIN"))
        {
            context.Roles.Add(new Rol
            {
                Codigo = "ADMIN",
                Nombre = "Administrador"
            });
        }

        if (!await context.Roles.AnyAsync(r => r.Codigo == "USER"))
        {
            context.Roles.Add(new Rol
            {
                Codigo = "USER",
                Nombre = "Usuario"
            });
        }

        await context.SaveChangesAsync();

        // 2. Buscar o crear el juego Terraria
        var terraria = await context.Juegos
            .FirstOrDefaultAsync(j => j.Codigo == "TERRARIA");

        if (terraria == null)
        {
            terraria = new Juego
            {
                Codigo = "TERRARIA",
                Nombre = "Terraria",
                Descripcion = "Juego de supervivencia y aventura",
                Activo = true
            };

            context.Juegos.Add(terraria);
            await context.SaveChangesAsync();
        }

        // 3. Crear la versión inicial de Terraria
        if (!await context.VersionesJuegos.AnyAsync(v =>
            v.IdJuego == terraria.IdJuego &&
            v.Version == "1.4.5.8"))
        {
            context.VersionesJuegos.Add(new VersionJuego
            {
                IdJuego = terraria.IdJuego,
                Version = "1.4.5.8",
                EtiquetaImagen = "terraria-hosting:1.4.5.8",
                Activa = true
            });

            await context.SaveChangesAsync();
        }
    }
}
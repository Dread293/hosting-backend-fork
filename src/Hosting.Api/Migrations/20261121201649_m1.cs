using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hosting.Api.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "juegos",
                columns: table => new
                {
                    id_juego = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_juegos", x => x.id_juego);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id_rol = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "versiones_juegos",
                columns: table => new
                {
                    id_version = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_juego = table.Column<short>(type: "smallint", nullable: false),
                    version = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    etiqueta_imagen = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    activa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_versiones_juegos", x => x.id_version);
                    table.ForeignKey(
                        name: "FK_versiones_juegos_juegos_id_juego",
                        column: x => x.id_juego,
                        principalTable: "juegos",
                        principalColumn: "id_juego",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    id_rol = table.Column<short>(type: "smallint", nullable: false),
                    nombre_usuario = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    correo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    contrasena_hash = table.Column<string>(type: "text", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "servidores",
                columns: table => new
                {
                    id_servidor = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    id_version = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    id_contenedor = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    estado_deseado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    estado_actual = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servidores", x => x.id_servidor);
                    table.ForeignKey(
                        name: "FK_servidores_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_servidores_versiones_juegos_id_version",
                        column: x => x.id_version,
                        principalTable: "versiones_juegos",
                        principalColumn: "id_version",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "almacenamientos_servidores",
                columns: table => new
                {
                    id_almacenamiento = table.Column<Guid>(type: "uuid", nullable: false),
                    id_servidor = table.Column<Guid>(type: "uuid", nullable: false),
                    clave_almacenamiento = table.Column<Guid>(type: "uuid", nullable: false),
                    ruta_almacenamiento = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_almacenamientos_servidores", x => x.id_almacenamiento);
                    table.ForeignKey(
                        name: "FK_almacenamientos_servidores_servidores_id_servidor",
                        column: x => x.id_servidor,
                        principalTable: "servidores",
                        principalColumn: "id_servidor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "configuraciones_servidores",
                columns: table => new
                {
                    id_servidor = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_mundo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tamano_mundo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    dificultad = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    max_jugadores = table.Column<short>(type: "smallint", nullable: false),
                    contrasena_cifrada = table.Column<string>(type: "text", nullable: true),
                    seguro = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_configuraciones_servidores", x => x.id_servidor);
                    table.ForeignKey(
                        name: "FK_configuraciones_servidores_servidores_id_servidor",
                        column: x => x.id_servidor,
                        principalTable: "servidores",
                        principalColumn: "id_servidor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "copias_seguridad",
                columns: table => new
                {
                    id_copia = table.Column<Guid>(type: "uuid", nullable: false),
                    id_servidor = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_archivo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ruta_almacenamiento = table.Column<string>(type: "text", nullable: false),
                    tamano_archivo = table.Column<long>(type: "bigint", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_copia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_copias_seguridad", x => x.id_copia);
                    table.ForeignKey(
                        name: "FK_copias_seguridad_servidores_id_servidor",
                        column: x => x.id_servidor,
                        principalTable: "servidores",
                        principalColumn: "id_servidor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "eventos_servidores",
                columns: table => new
                {
                    id_evento = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_servidor = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: true),
                    tipo_evento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    resultado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    detalles = table.Column<string>(type: "jsonb", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventos_servidores", x => x.id_evento);
                    table.ForeignKey(
                        name: "FK_eventos_servidores_servidores_id_servidor",
                        column: x => x.id_servidor,
                        principalTable: "servidores",
                        principalColumn: "id_servidor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eventos_servidores_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "puertos_servidores",
                columns: table => new
                {
                    id_puerto = table.Column<Guid>(type: "uuid", nullable: false),
                    id_servidor = table.Column<Guid>(type: "uuid", nullable: false),
                    puerto_host = table.Column<int>(type: "integer", nullable: false),
                    puerto_contenedor = table.Column<int>(type: "integer", nullable: false),
                    protocolo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_puertos_servidores", x => x.id_puerto);
                    table.ForeignKey(
                        name: "FK_puertos_servidores_servidores_id_servidor",
                        column: x => x.id_servidor,
                        principalTable: "servidores",
                        principalColumn: "id_servidor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_almacenamientos_servidores_clave_almacenamiento",
                table: "almacenamientos_servidores",
                column: "clave_almacenamiento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_almacenamientos_servidores_id_servidor",
                table: "almacenamientos_servidores",
                column: "id_servidor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_copias_seguridad_id_servidor",
                table: "copias_seguridad",
                column: "id_servidor");

            migrationBuilder.CreateIndex(
                name: "IX_eventos_servidores_id_servidor",
                table: "eventos_servidores",
                column: "id_servidor");

            migrationBuilder.CreateIndex(
                name: "IX_eventos_servidores_id_usuario",
                table: "eventos_servidores",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_juegos_codigo",
                table: "juegos",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_puertos_servidores_id_servidor",
                table: "puertos_servidores",
                column: "id_servidor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_puertos_servidores_puerto_host",
                table: "puertos_servidores",
                column: "puerto_host",
                unique: true,
                filter: "activo = TRUE");

            migrationBuilder.CreateIndex(
                name: "IX_servidores_id_usuario",
                table: "servidores",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_servidores_id_version",
                table: "servidores",
                column: "id_version");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_rol",
                table: "usuarios",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "IX_versiones_juegos_id_juego_version",
                table: "versiones_juegos",
                columns: new[] { "id_juego", "version" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "almacenamientos_servidores");

            migrationBuilder.DropTable(
                name: "configuraciones_servidores");

            migrationBuilder.DropTable(
                name: "copias_seguridad");

            migrationBuilder.DropTable(
                name: "eventos_servidores");

            migrationBuilder.DropTable(
                name: "puertos_servidores");

            migrationBuilder.DropTable(
                name: "servidores");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "versiones_juegos");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "juegos");
        }
    }
}

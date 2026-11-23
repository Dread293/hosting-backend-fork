using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hosting.Api.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "identidades_externas",
                columns: table => new
                {
                    id_identidad = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    proveedor = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    id_usuario_proveedor = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    correo_proveedor = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    fecha_vinculacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identidades_externas", x => x.id_identidad);
                    table.ForeignKey(
                        name: "FK_identidades_externas_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_identidades_externas_id_usuario",
                table: "identidades_externas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "uq_identidades_externas_proveedor_usuario",
                table: "identidades_externas",
                columns: new[] { "proveedor", "id_usuario_proveedor" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "identidades_externas");
        }
    }
}

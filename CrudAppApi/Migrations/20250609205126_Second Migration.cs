using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudAppApi.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Telefonos_TelefonoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_TelefonoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TelefonoId",
                table: "Usuarios");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Telefonos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_UsuarioId",
                table: "Telefonos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefonos_Usuarios_UsuarioId",
                table: "Telefonos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefonos_Usuarios_UsuarioId",
                table: "Telefonos");

            migrationBuilder.DropIndex(
                name: "IX_Telefonos_UsuarioId",
                table: "Telefonos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Telefonos");

            migrationBuilder.AddColumn<Guid>(
                name: "TelefonoId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TelefonoId",
                table: "Usuarios",
                column: "TelefonoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Telefonos_TelefonoId",
                table: "Usuarios",
                column: "TelefonoId",
                principalTable: "Telefonos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

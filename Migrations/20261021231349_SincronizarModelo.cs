using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAlgoritmos.Migrations
{
    /// <inheritdoc />
    public partial class SincronizarModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Reparaciones",
                newName: "FechaReparacion");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Reparaciones",
                newName: "Diagnostico");

            migrationBuilder.RenameColumn(
                name: "Costo",
                table: "Reparaciones",
                newName: "Monto");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaDiagnostico",
                table: "Reparaciones",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaEntrega",
                table: "Reparaciones",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "NumeroFactura",
                table: "Reparaciones",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AnosGarantia",
                table: "Activos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContactoProveedor",
                table: "Activos",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DireccionProveedor",
                table: "Activos",
                type: "TEXT",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaIngreso",
                table: "Activos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "NumeroFacturaCompra",
                table: "Activos",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelefonoProveedor",
                table: "Activos",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDiagnostico",
                table: "Reparaciones");

            migrationBuilder.DropColumn(
                name: "FechaEntrega",
                table: "Reparaciones");

            migrationBuilder.DropColumn(
                name: "NumeroFactura",
                table: "Reparaciones");

            migrationBuilder.DropColumn(
                name: "AnosGarantia",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "ContactoProveedor",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "DireccionProveedor",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "FechaIngreso",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "NumeroFacturaCompra",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "TelefonoProveedor",
                table: "Activos");

            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "Reparaciones",
                newName: "Costo");

            migrationBuilder.RenameColumn(
                name: "FechaReparacion",
                table: "Reparaciones",
                newName: "Fecha");

            migrationBuilder.RenameColumn(
                name: "Diagnostico",
                table: "Reparaciones",
                newName: "Descripcion");
        }
    }
}

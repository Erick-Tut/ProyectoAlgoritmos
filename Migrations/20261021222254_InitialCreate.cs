using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAlgoritmos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodigoInventario = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Marca = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NumeroSerie = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ProveedorCompra = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CostoCompra = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    FechaCompra = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    FechaFinGarantia = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Notas = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reparaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActivoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Costo = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Proveedor = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reparaciones_Activos_ActivoId",
                        column: x => x.ActivoId,
                        principalTable: "Activos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activos_CodigoInventario",
                table: "Activos",
                column: "CodigoInventario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activos_NumeroSerie",
                table: "Activos",
                column: "NumeroSerie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reparaciones_ActivoId",
                table: "Reparaciones",
                column: "ActivoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reparaciones");

            migrationBuilder.DropTable(
                name: "Activos");
        }
    }
}

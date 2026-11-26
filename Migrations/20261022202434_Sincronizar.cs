using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAlgoritmos.Migrations
{
    /// <inheritdoc />
    public partial class Sincronizar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactoProveedor",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "Notas",
                table: "Activos");

            migrationBuilder.RenameColumn(
                name: "NumeroFactura",
                table: "Reparaciones",
                newName: "Factura");

            migrationBuilder.RenameColumn(
                name: "FechaEntrega",
                table: "Reparaciones",
                newName: "FechaFactura");

            migrationBuilder.RenameColumn(
                name: "FechaDiagnostico",
                table: "Reparaciones",
                newName: "FechaEntregaUsuario");

            migrationBuilder.RenameColumn(
                name: "TelefonoProveedor",
                table: "Activos",
                newName: "Telefono");

            migrationBuilder.RenameColumn(
                name: "ProveedorCompra",
                table: "Activos",
                newName: "Proveedor");

            migrationBuilder.RenameColumn(
                name: "NumeroFacturaCompra",
                table: "Activos",
                newName: "NumeroFactura");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Activos",
                newName: "PersonaContacto");

            migrationBuilder.RenameColumn(
                name: "FechaFinGarantia",
                table: "Activos",
                newName: "FechaVencimientoGarantia");

            migrationBuilder.RenameColumn(
                name: "DireccionProveedor",
                table: "Activos",
                newName: "Direccion");

            migrationBuilder.RenameColumn(
                name: "CostoCompra",
                table: "Activos",
                newName: "Monto");

            migrationBuilder.RenameColumn(
                name: "CodigoInventario",
                table: "Activos",
                newName: "CodigoActivo");

            migrationBuilder.RenameIndex(
                name: "IX_Activos_CodigoInventario",
                table: "Activos",
                newName: "IX_Activos_CodigoActivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaFactura",
                table: "Reparaciones",
                newName: "FechaEntrega");

            migrationBuilder.RenameColumn(
                name: "FechaEntregaUsuario",
                table: "Reparaciones",
                newName: "FechaDiagnostico");

            migrationBuilder.RenameColumn(
                name: "Factura",
                table: "Reparaciones",
                newName: "NumeroFactura");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "Activos",
                newName: "TelefonoProveedor");

            migrationBuilder.RenameColumn(
                name: "Proveedor",
                table: "Activos",
                newName: "ProveedorCompra");

            migrationBuilder.RenameColumn(
                name: "PersonaContacto",
                table: "Activos",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "NumeroFactura",
                table: "Activos",
                newName: "NumeroFacturaCompra");

            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "Activos",
                newName: "CostoCompra");

            migrationBuilder.RenameColumn(
                name: "FechaVencimientoGarantia",
                table: "Activos",
                newName: "FechaFinGarantia");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "Activos",
                newName: "DireccionProveedor");

            migrationBuilder.RenameColumn(
                name: "CodigoActivo",
                table: "Activos",
                newName: "CodigoInventario");

            migrationBuilder.RenameIndex(
                name: "IX_Activos_CodigoActivo",
                table: "Activos",
                newName: "IX_Activos_CodigoInventario");

            migrationBuilder.AddColumn<string>(
                name: "ContactoProveedor",
                table: "Activos",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notas",
                table: "Activos",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);
        }
    }
}

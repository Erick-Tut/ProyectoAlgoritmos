using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAlgoritmos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPapeleraActivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Activos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Activos");
        }
    }
}

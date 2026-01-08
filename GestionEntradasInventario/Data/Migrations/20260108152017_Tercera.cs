using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEntradasInventario.Migrations
{
    /// <inheritdoc />
    public partial class Tercera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cantdiad",
                table: "EntradaDetalles",
                newName: "Cantidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "EntradaDetalles",
                newName: "Cantdiad");
        }
    }
}

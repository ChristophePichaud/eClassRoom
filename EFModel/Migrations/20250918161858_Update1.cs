using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFModel.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomFormation",
                table: "SallesDeFormation",
                newName: "Nom");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "MachinesVirtuelles");

            migrationBuilder.RenameColumn(
                name: "Nom",
                table: "SallesDeFormation",
                newName: "NomFormation");
        }
    }
}

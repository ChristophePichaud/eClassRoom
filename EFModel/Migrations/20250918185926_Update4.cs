using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFModel.Migrations
{
    /// <inheritdoc />
    public partial class Update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_FormateurId",
                table: "SallesDeFormation");

            migrationBuilder.RenameColumn(
                name: "NomMarketingVM",
                table: "MachinesVirtuelles",
                newName: "NomMarketing");

            migrationBuilder.AlterColumn<int>(
                name: "FormateurId",
                table: "SallesDeFormation",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_FormateurId",
                table: "SallesDeFormation",
                column: "FormateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_FormateurId",
                table: "SallesDeFormation");

            migrationBuilder.RenameColumn(
                name: "NomMarketing",
                table: "MachinesVirtuelles",
                newName: "NomMarketingVM");

            migrationBuilder.AlterColumn<int>(
                name: "FormateurId",
                table: "SallesDeFormation",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_FormateurId",
                table: "SallesDeFormation",
                column: "FormateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id");
        }
    }
}

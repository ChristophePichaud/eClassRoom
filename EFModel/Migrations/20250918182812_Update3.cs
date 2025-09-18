using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFModel.Migrations
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Clients_ClientId1",
                table: "SallesDeFormation");

            migrationBuilder.DropIndex(
                name: "IX_SallesDeFormation_ClientId1",
                table: "SallesDeFormation");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "SallesDeFormation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId1",
                table: "SallesDeFormation",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SallesDeFormation_ClientId1",
                table: "SallesDeFormation",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SallesDeFormation_Clients_ClientId1",
                table: "SallesDeFormation",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}

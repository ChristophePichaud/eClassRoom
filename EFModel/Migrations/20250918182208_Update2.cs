using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFModel.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachinesVirtuelles_SallesDeFormation_SalleDeFormationId",
                table: "MachinesVirtuelles");

            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Clients_ClientId",
                table: "SallesDeFormation");

            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_FormateurId",
                table: "SallesDeFormation");

            migrationBuilder.DropIndex(
                name: "IX_MachinesVirtuelles_SalleDeFormationId",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "SalleDeFormationId",
                table: "MachinesVirtuelles");

            migrationBuilder.AlterColumn<int>(
                name: "FormateurId",
                table: "SallesDeFormation",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ClientId1",
                table: "SallesDeFormation",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalleDeFormationMachineVirtuelle",
                columns: table => new
                {
                    MachineVirtuelleId = table.Column<int>(type: "integer", nullable: false),
                    SalleDeFormationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalleDeFormationMachineVirtuelle", x => new { x.MachineVirtuelleId, x.SalleDeFormationId });
                    table.ForeignKey(
                        name: "FK_SalleDeFormationMachineVirtuelle_MachinesVirtuelles_Machine~",
                        column: x => x.MachineVirtuelleId,
                        principalTable: "MachinesVirtuelles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalleDeFormationMachineVirtuelle_SallesDeFormation_SalleDeF~",
                        column: x => x.SalleDeFormationId,
                        principalTable: "SallesDeFormation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SallesDeFormation_ClientId1",
                table: "SallesDeFormation",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_SalleDeFormationMachineVirtuelle_SalleDeFormationId",
                table: "SalleDeFormationMachineVirtuelle",
                column: "SalleDeFormationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SallesDeFormation_Clients_ClientId",
                table: "SallesDeFormation",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SallesDeFormation_Clients_ClientId1",
                table: "SallesDeFormation",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_FormateurId",
                table: "SallesDeFormation",
                column: "FormateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Clients_ClientId",
                table: "SallesDeFormation");

            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Clients_ClientId1",
                table: "SallesDeFormation");

            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_FormateurId",
                table: "SallesDeFormation");

            migrationBuilder.DropTable(
                name: "SalleDeFormationMachineVirtuelle");

            migrationBuilder.DropIndex(
                name: "IX_SallesDeFormation_ClientId1",
                table: "SallesDeFormation");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "SallesDeFormation");

            migrationBuilder.AlterColumn<int>(
                name: "FormateurId",
                table: "SallesDeFormation",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalleDeFormationId",
                table: "MachinesVirtuelles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MachinesVirtuelles_SalleDeFormationId",
                table: "MachinesVirtuelles",
                column: "SalleDeFormationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachinesVirtuelles_SallesDeFormation_SalleDeFormationId",
                table: "MachinesVirtuelles",
                column: "SalleDeFormationId",
                principalTable: "SallesDeFormation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SallesDeFormation_Clients_ClientId",
                table: "SallesDeFormation",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_FormateurId",
                table: "SallesDeFormation",
                column: "FormateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelToNewRequirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachinesVirtuelles_Utilisateurs_StagiaireId",
                table: "MachinesVirtuelles");

            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_FormateurId",
                table: "SallesDeFormation");

            migrationBuilder.DropIndex(
                name: "IX_MachinesVirtuelles_StagiaireId",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "DiskISO",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "FichierRDP",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "Supervision",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "TypeOS",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "TypeVM",
                table: "MachinesVirtuelles");

            migrationBuilder.RenameColumn(
                name: "FormateurId",
                table: "SallesDeFormation",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_SallesDeFormation_FormateurId",
                table: "SallesDeFormation",
                newName: "IX_SallesDeFormation_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "StagiaireId",
                table: "MachinesVirtuelles",
                newName: "VmType");

            migrationBuilder.RenameColumn(
                name: "MotDePasseAdministrateur",
                table: "Clients",
                newName: "DomainName");

            migrationBuilder.RenameColumn(
                name: "EmailAdministrateur",
                table: "Clients",
                newName: "BillingEmail");

            migrationBuilder.RenameColumn(
                name: "Adresse",
                table: "Clients",
                newName: "AddressLine1");

            migrationBuilder.AlterColumn<string>(
                name: "NomMarketing",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "MachinesVirtuelles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PublicIp",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RdpInfo",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "MachinesVirtuelles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VmISO",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VmMachineId",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VmOsType",
                table: "MachinesVirtuelles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "Clients",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminUserId",
                table: "Clients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LicenseType",
                table: "Clients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MachinesVirtuelles_OwnerId",
                table: "MachinesVirtuelles",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachinesVirtuelles_Utilisateurs_OwnerId",
                table: "MachinesVirtuelles",
                column: "OwnerId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_CreatedBy",
                table: "SallesDeFormation",
                column: "CreatedBy",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachinesVirtuelles_Utilisateurs_OwnerId",
                table: "MachinesVirtuelles");

            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_CreatedBy",
                table: "SallesDeFormation");

            migrationBuilder.DropIndex(
                name: "IX_MachinesVirtuelles_OwnerId",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "PublicIp",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "RdpInfo",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "VmISO",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "VmMachineId",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "VmOsType",
                table: "MachinesVirtuelles");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AdminUserId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LicenseType",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "SallesDeFormation",
                newName: "FormateurId");

            migrationBuilder.RenameIndex(
                name: "IX_SallesDeFormation_CreatedBy",
                table: "SallesDeFormation",
                newName: "IX_SallesDeFormation_FormateurId");

            migrationBuilder.RenameColumn(
                name: "VmType",
                table: "MachinesVirtuelles",
                newName: "StagiaireId");

            migrationBuilder.RenameColumn(
                name: "DomainName",
                table: "Clients",
                newName: "MotDePasseAdministrateur");

            migrationBuilder.RenameColumn(
                name: "BillingEmail",
                table: "Clients",
                newName: "EmailAdministrateur");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                table: "Clients",
                newName: "Adresse");

            migrationBuilder.AlterColumn<string>(
                name: "NomMarketing",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiskISO",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FichierRDP",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Supervision",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeOS",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeVM",
                table: "MachinesVirtuelles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "Clients",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MachinesVirtuelles_StagiaireId",
                table: "MachinesVirtuelles",
                column: "StagiaireId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachinesVirtuelles_Utilisateurs_StagiaireId",
                table: "MachinesVirtuelles",
                column: "StagiaireId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SallesDeFormation_Utilisateurs_FormateurId",
                table: "SallesDeFormation",
                column: "FormateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

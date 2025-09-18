using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EFModel.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomSociete = table.Column<string>(type: "text", nullable: false),
                    Adresse = table.Column<string>(type: "text", nullable: false),
                    CodePostal = table.Column<string>(type: "text", nullable: false),
                    Ville = table.Column<string>(type: "text", nullable: false),
                    Pays = table.Column<string>(type: "text", nullable: false),
                    EmailAdministrateur = table.Column<string>(type: "text", nullable: false),
                    Mobile = table.Column<string>(type: "text", nullable: false),
                    MotDePasseAdministrateur = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    Mois = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Montant = table.Column<decimal>(type: "numeric", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factures_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachinesVirtuelles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeOS = table.Column<string>(type: "text", nullable: false),
                    TypeVM = table.Column<string>(type: "text", nullable: false),
                    Sku = table.Column<string>(type: "text", nullable: false),
                    Offer = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    DiskISO = table.Column<string>(type: "text", nullable: false),
                    NomMarketingVM = table.Column<string>(type: "text", nullable: false),
                    FichierRDP = table.Column<string>(type: "text", nullable: false),
                    Supervision = table.Column<string>(type: "text", nullable: false),
                    StagiaireId = table.Column<int>(type: "integer", nullable: false),
                    SalleDeFormationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachinesVirtuelles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProvisionningVMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalleDeFormationId = table.Column<int>(type: "integer", nullable: false),
                    StagiaireId = table.Column<int>(type: "integer", nullable: false),
                    VmName = table.Column<string>(type: "text", nullable: false),
                    PublicIp = table.Column<string>(type: "text", nullable: false),
                    DateProvisionning = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvisionningVMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SallesDeFormation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomFormation = table.Column<string>(type: "text", nullable: false),
                    FormateurId = table.Column<int>(type: "integer", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SallesDeFormation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SallesDeFormation_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    MotDePasse = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    SalleDeFormationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_SallesDeFormation_SalleDeFormationId",
                        column: x => x.SalleDeFormationId,
                        principalTable: "SallesDeFormation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factures_ClientId",
                table: "Factures",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_MachinesVirtuelles_SalleDeFormationId",
                table: "MachinesVirtuelles",
                column: "SalleDeFormationId");

            migrationBuilder.CreateIndex(
                name: "IX_MachinesVirtuelles_StagiaireId",
                table: "MachinesVirtuelles",
                column: "StagiaireId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvisionningVMs_SalleDeFormationId",
                table: "ProvisionningVMs",
                column: "SalleDeFormationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvisionningVMs_StagiaireId",
                table: "ProvisionningVMs",
                column: "StagiaireId");

            migrationBuilder.CreateIndex(
                name: "IX_SallesDeFormation_ClientId",
                table: "SallesDeFormation",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SallesDeFormation_FormateurId",
                table: "SallesDeFormation",
                column: "FormateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_ClientId",
                table: "Utilisateurs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_SalleDeFormationId",
                table: "Utilisateurs",
                column: "SalleDeFormationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachinesVirtuelles_SallesDeFormation_SalleDeFormationId",
                table: "MachinesVirtuelles",
                column: "SalleDeFormationId",
                principalTable: "SallesDeFormation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachinesVirtuelles_Utilisateurs_StagiaireId",
                table: "MachinesVirtuelles",
                column: "StagiaireId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvisionningVMs_SallesDeFormation_SalleDeFormationId",
                table: "ProvisionningVMs",
                column: "SalleDeFormationId",
                principalTable: "SallesDeFormation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvisionningVMs_Utilisateurs_StagiaireId",
                table: "ProvisionningVMs",
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
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SallesDeFormation_Clients_ClientId",
                table: "SallesDeFormation");

            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateurs_Clients_ClientId",
                table: "Utilisateurs");

            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateurs_SallesDeFormation_SalleDeFormationId",
                table: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "MachinesVirtuelles");

            migrationBuilder.DropTable(
                name: "ProvisionningVMs");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "SallesDeFormation");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}

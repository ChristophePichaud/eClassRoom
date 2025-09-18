# Arborescence complète des fichiers de la solution eClassRoom

```
eClassRoom/
│
├── .gitignore
├── eClassRoom.sln
├── files.txt
├── README.md
│
├── .github/
│   ├── copilot-instructions.md
│   └── workflows/
│
├── Client/
│   ├── App.razor
│   ├── Client.csproj
│   ├── Program.cs
│   ├── _Imports.razor
│   ├── Pages/
│   │   ├── Clients.razor
│   │   ├── Clients.razor.cs
│   │   ├── Counter.razor
│   │   ├── Index.razor
│   │   ├── Login.razor
│   │   ├── Login.razor.cs
│   │   ├── MachinesVirtuelles.razor
│   │   ├── MachinesVirtuelles.razor.cs
│   │   ├── SalleDeFormation.razor
│   │   ├── SallesDeFormation.razor
│   │   ├── SallesDeFormation.razor.cs
│   │   ├── Utilisateurs.razor
│   │   ├── Utilisateurs.razor.cs
│   │   └── Weather.razor
│   ├── Layout/
│   │   ├── MainLayout.razor
│   │   ├── MainLayout.razor.css
│   │   ├── NavMenu.razor
│   │   └── NavMenu.razor.css
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── wwwroot/
│   │   ├── favicon.png
│   │   ├── icon-192.png
│   │   ├── index.html
│   │   ├── css/
│   │   │   └── app.css
│   │   ├── lib/
│   │   │   └── bootstrap/
│   │   │       └── dist/
│   │   │           ├── css/
│   │   │           └── js/
│   │   └── sample-data/
│   │       └── weather.json
│   └── ... bin/, obj/ (build system)
│
├── Docs/
│   ├── Arborescence-Fichiers.md
│   ├── AzureInfrastructure.md
│   ├── AzureSalleDeFormation_metier.md
│   ├── AzureSalleDeFormation_sequence.puml
│   ├── AzureSalleDeFormation_technique.md
│   ├── Clients.md
│   ├── ClientsService.md
│   ├── dotnet-cli-creation.md
│   ├── EFModel.jpg
│   ├── EFModel.puml
│   ├── Files.md
│   ├── GestionJWT.md
│   ├── introduction m�tier.puml
│   ├── Login.md
│   ├── MachinesVirtuelles.md
│   ├── MachinesVirtuelles_Metier.md
│   ├── MyProject.md
│   ├── MyProject.pdf
│   ├── m�tier.png
│   ├── Project.md
│   ├── Project.pdf
│   ├── Provisionner_SalleDeFormation.png
│   ├── SallesDeFormation.md
│   ├── SallesDeFormation_Metier.md
│   ├── scenario.png
│   ├── scenario.puml
│   ├── Specifications.md
│   ├── Utilisateurs.md
│   ├── Utilisateurs_Metier.md
│   └── VSCode_Chat_CopilotForGitHub.png
│
├── EFMigrator/
│   ├── appsettings.json
│   ├── EFMigrator.csproj
│   ├── Program.cs
│   └── ... bin/, obj/ (build system)
│
├── EFModel/
│   ├── appsettings.json
│   ├── Class1.cs
│   ├── EClassRoomDbContext.cs
│   ├── EClassRoomDbContextFactory.cs
│   ├── EFModel.csproj
│   ├── Migrations/
│   │   ├── 20250918143152_InitialCreate.cs
│   │   ├── 20250918143152_InitialCreate.Designer.cs
│   │   ├── 20250918161858_Update1.cs
│   │   ├── 20250918161858_Update1.Designer.cs
│   │   ├── 20250918182208_Update2.cs
│   │   ├── 20250918182208_Update2.Designer.cs
│   │   ├── 20250918182812_Update3.cs
│   │   ├── 20250918182812_Update3.Designer.cs
│   │   ├── 20250918185926_Update4.cs
│   │   ├── 20250918185926_Update4.Designer.cs
│   │   ├── 20250918191931_Update5.cs
│   │   ├── 20250918191931_Update5.Designer.cs
│   │   └── EClassRoomDbContextModelSnapshot.cs
│   ├── Models/
│   │   ├── Client.cs
│   │   ├── Facture.cs
│   │   ├── MachineVirtuelle.cs
│   │   ├── ProvisionningVM.cs
│   │   ├── SalleDeFormation.cs
│   │   └── Utilisateur.cs
│   └── ... bin/, obj/ (build system)
│
├── Server/
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── Program.cs
│   ├── Server.csproj
│   ├── Server.csproj.user
│   ├── Server.http
│   ├── Startup.cs
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── ClientController.cs
│   │   ├── Controllers2/
│   │   ├── MachinesController.cs
│   │   ├── SallesController.cs
│   │   └── UsersController.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── Services/
│   │   ├── AuthService.cs
│   │   ├── AzureInfrastructureService.cs
│   │   ├── AzureSalleDeFormationService.cs
│   │   ├── ClientService.cs
│   │   ├── MachineVirtuelleService.cs
│   │   ├── SalleDeFormationService.cs
│   │   └── UtilisateurService.cs
│   ├── WeatherForecast.cs
│   └── ... bin/, obj/ (build system)
│
├── Shared/
│   ├── Class1.cs
│   ├── Shared.csproj
│   ├── Dtos/
│   │   ├── ClientDto.cs
│   │   ├── LoginDto.cs
│   │   ├── MachineVirtuelleDto.cs
│   │   ├── ProvisionningResultDto.cs
│   │   ├── SalleDeFormationDto.cs
│   │   └── UtilisateurDto.cs
│   └── ... bin/, obj/ (build system)
│
├── Test.CLI/
│   ├── appsettings.json
│   ├── Program.cs
│   ├── Test.CLI.csproj
│   ├── Properties/
│   │   └── launchSettings.json
│   └── ... bin/, obj/ (build system)
```

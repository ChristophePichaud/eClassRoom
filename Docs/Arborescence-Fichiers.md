# Arborescence des fichiers de la solution eClassRoom

```
eClassRoom/
│
├── .github/
│   └── copilot-instructions.md
│
├── Client/
│   ├── Pages/
│   │   ├── Clients.razor
│   │   ├── Clients.razor.cs
│   │   ├── Utilisateurs.razor
│   │   ├── Utilisateurs.razor.cs
│   │   └── ... (autres pages Razor)
│   └── ... (autres dossiers/fichiers client)
│
├── Docs/
│   ├── Arborescence-Fichiers.md
│   ├── Clients.md
│   └── ... (autres docs techniques)
│
├── EFModel/
│   ├── EClassRoomDbContext.cs
│   └── Models/
│       ├── Client.cs
│       ├── Utilisateur.cs
│       ├── SalleDeFormation.cs
│       ├── MachineVirtuelle.cs
│       ├── Facture.cs
│       └── ProvisionningVM.cs
│
├── Server/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── ClientsController.cs
│   │   ├── UtilisateursController.cs
│   │   ├── SallesDeFormationController.cs
│   │   └── ... (autres contrôleurs)
│   ├── Services/
│   │   ├── ClientService.cs
│   │   ├── UtilisateurService.cs
│   │   ├── SalleDeFormationService.cs
│   │   └── ... (autres services)
│   ├── Program.cs
│   ├── appsettings.json
│   └── ... (autres fichiers serveur)
│
├── Shared/
│   └── Dtos/
│       ├── ClientDto.cs
│       ├── UtilisateurDto.cs
│       ├── SalleDeFormationDto.cs
│       ├── MachineVirtuelleDto.cs
│       └── ... (autres DTO)
│
├── Test.CLI/
│   └── Program.cs
│
└── ... (autres dossiers/fichiers racine)
```

**Remarques :**
- Les dossiers principaux correspondent à chaque couche de l’architecture (Client, Server, EFModel, Shared).
- Les fichiers `.razor` et `.razor.cs` sont dans `Client/Pages`.
- Les modèles EF sont dans `EFModel/Models`.
- Les DTO sont dans `Shared/Dtos`.
- Les contrôleurs et services sont dans `Server/Controllers` et `Server/Services`.
- Les fichiers de documentation sont dans `Docs/`.

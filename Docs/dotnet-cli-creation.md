# Création de l’architecture multi-couches avec la CLI .NET

Voici les commandes pour générer la structure de l’application :

1. **Créer la solution principale**
   ```sh
   dotnet new sln -n eClassRoom
   ```

2. **Créer le projet Blazor WebAssembly (Client)**
   ```sh
   dotnet new blazorwasm -n Client -o Client --no-https
   ```

3. **Créer le projet Web API (Server)**
   ```sh
   dotnet new webapi -n Server -o Server
   ```

4. **Créer la bibliothèque partagée pour les DTO (Shared)**
   ```sh
   dotnet new classlib -n Shared -o Shared
   ```

5. **Créer le projet EFModel pour les entités et le DbContext**
   ```sh
   dotnet new classlib -n EFModel -o EFModel
   ```

6. **Ajouter les projets à la solution**
   ```sh
   dotnet sln add .\Client\Client.csproj
   dotnet sln add .\Server\Server.csproj
   dotnet sln add .\Shared\Shared.csproj
   dotnet sln add .\EFModel\EFModel.csproj
   ```

7. **Ajouter les références de projet**
   - Le serveur doit référencer `Shared` et `EFModel` :
     ```sh
     dotnet add .\Server\Server.csproj reference .\Shared\Shared.csproj
     dotnet add .\Server\Server.csproj reference .\EFModel\EFModel.csproj
     ```
   - Le client doit référencer `Shared` :
     ```sh
     dotnet add .\Client\Client.csproj reference .\Shared\Shared.csproj
     ```

8. **Installer les packages nécessaires**
   - Pour Entity Framework Core et PostgreSQL dans `EFModel` et `Server` :
     ```sh
     dotnet add .\EFModel\EFModel.csproj package Microsoft.EntityFrameworkCore
     dotnet add .\EFModel\EFModel.csproj package Npgsql.EntityFrameworkCore.PostgreSQL
     dotnet add .\Server\Server.csproj package Microsoft.EntityFrameworkCore.Design
     dotnet add .\Server\Server.csproj package Microsoft.AspNetCore.Authentication.JwtBearer
     ```

## Résumé

- **Client** : Blazor WebAssembly pour l’UI.
- **Server** : API REST .NET.
- **Shared** : DTO partagés.
- **EFModel** : Entités EF Core et DbContext.

Cette structure respecte l’architecture multi-couches décrite dans la documentation du projet.

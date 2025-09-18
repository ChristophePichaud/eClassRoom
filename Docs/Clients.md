# Documentation technique : Gestion des clients

## Vue d'ensemble

La gestion des clients repose sur l’architecture multi-couches :
- **Client** (Blazor WebAssembly) : Affichage, création et gestion des clients via une page Razor et un fichier code-behind.
- **Server** : Expose des endpoints REST sécurisés pour la gestion des clients, en s’appuyant sur la couche service.
- **Shared** : Utilisation de DTO (`ClientDto`) pour échanger les données de façon sécurisée.
- **EFModel** : Entité `Client` mappée sur la base PostgreSQL.

## Fonctionnalités côté client

- La page `Clients.razor` affiche la liste des clients dans un tableau BootstrapBlazor.
- Un bouton "Nouveau Client" ouvre un formulaire modal pour la création d’un client.
- Toute la logique métier (chargement, création, gestion du modal) est déportée dans le fichier code-behind `Clients.razor.cs`.
- Les appels HTTP sont réalisés via `HttpClient` et consomment l’API REST du serveur.
- Après création d’un client, la liste est automatiquement rafraîchie.

## Fonctionnalités côté serveur

- Le contrôleur REST reçoit les requêtes, les valide et délègue à la couche service (`ClientService`).
- Le service effectue la conversion entité <-> DTO et gère la persistance via Entity Framework.
- Les mots de passe administrateurs sont traités de façon sécurisée (jamais exposés côté client).

## Sécurité

- Les endpoints sont sécurisés par authentification JWT.
- Les DTO permettent de maîtriser les données exposées au client.

## Références de code

- `Client/Pages/Clients.razor` : UI et modal de création.
- `Client/Pages/Clients.razor.cs` : Code-behind, logique métier et appels API.
- `Shared/Dtos/ClientDto.cs` : Définition du DTO.
- `Server/Services/ClientService.cs` : Service métier côté serveur.

## Exemple d’usage

1. L’utilisateur clique sur "Nouveau Client", saisit les informations et valide.
2. Le client Blazor envoie la requête POST à l’API.
3. Le serveur crée le client, retourne le DTO.
4. Le client recharge la liste et affiche le nouveau client.

---
Cette documentation est générée automatiquement à chaque évolution du code lié à la gestion des clients.
  - Le contrôleur `ClientsController` reçoit les requêtes du client, valide les données, puis délègue la logique à la couche de services (`ClientService`).
  - La couche de services utilise Entity Framework (`EFModel`) pour interagir avec la base PostgreSQL (lecture, création, modification, suppression d’entités `Client`).
  - Les entités EF ne sont jamais exposées directement au client : seules les données des DTO transitent.

## Résumé du flux

1. L’utilisateur interagit avec la page `Clients.razor` (affichage, ajout, édition, suppression).
2. Le code-behind effectue des appels HTTP vers l’API REST du serveur.
3. Le serveur traite les requêtes via le contrôleur et la couche de services, qui utilise EF pour manipuler la base de données.
4. Les réponses sont renvoyées sous forme de DTO et affichées côté client.

## Références de code

- **Clients.razor** : UI et composants BootstrapBlazor.
- **Clients.razor.cs** : Logique métier côté client, appels API, gestion des états.
- **ClientsController.cs** (Server) : Endpoints REST pour la gestion des clients.
- **Services/ClientService.cs** (Server) : Logique métier et accès aux données via EF.
- **EFModel/Models/Client.cs** : Entité EF représentant un client.
- **Shared/ClientDto.cs** : DTO utilisé pour le transport des données client.

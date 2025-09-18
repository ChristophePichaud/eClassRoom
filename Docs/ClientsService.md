# Documentation technique : Gestion des clients

## Service côté serveur (`ClientService`)
Le service `ClientService` centralise la logique métier liée aux clients :
- Fournit des méthodes asynchrones pour lister, ajouter, modifier et supprimer des clients.
- Utilise les DTO (`ClientDto`) pour échanger les données entre le serveur et le client, évitant d’exposer directement les entités EF.
- Garantit que les mots de passe administrateurs ne sont jamais exposés côté client.

## Page Razor côté client (`Clients.razor`)
- Affiche la liste des clients dans un tableau BootstrapBlazor.
- Récupère les données via un appel HTTP GET à l’API REST (`api/clients`).
- Utilise les DTO pour le binding et l’affichage.

## Sécurité et architecture
- Les opérations CRUD sont sécurisées côté serveur (authentification requise).
- L’utilisation de DTO permet de maîtriser les données exposées au client.
- L’architecture en couches facilite la maintenance et l’évolution de l’application.

## Références
- `Server/Services/ClientService.cs`
- `Client/Pages/Clients.razor`

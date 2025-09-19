# Synthèse Architecture eClassRoom

## Vue d’ensemble

L’application eClassRoom repose sur une architecture **multi-couches** moderne, sécurisée et évolutive, adaptée à la gestion de salles de formation virtuelles.

## Couches principales

- **Client** : Application web Blazor WebAssembly (SPA) pour l’interface utilisateur.  
  - Affichage, saisie, interactions utilisateurs.
  - Appels HTTP sécurisés vers l’API via JWT.

- **Server** : API REST .NET (Web API) pour la logique métier et l’accès aux données.
  - Contrôleurs REST sécurisés par JWT.
  - Gestion des droits, logique métier, orchestration des services.

- **EFModel** : Couche d’accès aux données basée sur Entity Framework Core.
  - Modèles de données (Clients, Utilisateurs, Salles, VMs, Factures…).
  - Connexion à la base PostgreSQL.

- **Shared** : Objets de transfert de données (DTO) partagés entre client et serveur.
  - Garantit la sécurité et la cohérence des échanges.

## Sécurité

- Authentification basée sur des tokens JWT.
- Les endpoints sensibles sont protégés par `[Authorize]`.
- Les mots de passe sont stockés hashés côté serveur.
- CORS configuré pour permettre les échanges entre le client et l’API.

## Points forts

- **Séparation claire des responsabilités** : chaque couche a un rôle précis.
- **Extensible** : ajout de nouvelles fonctionnalités ou entités facilité.
- **Sécurisé** : gestion des droits, authentification forte, données sensibles protégées.
- **Interopérable** : API REST standard, compatible avec d’autres clients ou outils.

## Schéma de fonctionnement

1. L’utilisateur se connecte via le portail web (Blazor).
2. Le client envoie ses requêtes à l’API REST, en incluant le token JWT.
3. Le serveur valide, traite la demande, et interagit avec la base de données via EF Core.
4. Les données sont échangées sous forme de DTO, jamais d’entités directes.
5. Les réponses sont renvoyées au client pour affichage ou action.

---

**En résumé** :  
eClassRoom est une solution web robuste, sécurisée et moderne, pensée pour la gestion efficace de salles de formation virtuelles, avec une architecture claire et évolutive.

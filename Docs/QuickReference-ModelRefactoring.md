# Guide de Référence Rapide - Refonte du Modèle EF (2025)

## 📋 Vue d'ensemble

Ce document fournit un guide de référence rapide pour la refonte du modèle Entity Framework d'eClassRoom réalisée en janvier 2025.

## 🔗 Documents détaillés

- **[ModelRefactoring2025.md](ModelRefactoring2025.md)** - Guide complet de refactoring avec analyse d'impact
- **[ModelSpecifications2025.md](ModelSpecifications2025.md)** - Spécifications complètes du modèle mis à jour

## 🎯 Objectif de la refonte

Aligner le modèle de données avec les exigences métier réelles du système de gestion de salles de formation virtuelles, en utilisant des types fortement typés (enums) et une nomenclature plus claire.

## 📊 Changements principaux

### Nouvelles énumérations

| Enum | Valeurs | Usage |
|------|---------|-------|
| `LicenseType` | Academic, Professional, Gov | Type de licence client |
| `UserRole` | SuperAdmin, Admin, Student | Rôle utilisateur |
| `VmOsType` | Linux, Windows | Système d'exploitation VM |
| `VmType` | Standard_B2s, Standard_B4ms, etc. | Type/taille VM Azure |
| `VmStatus` | NotCreated, Created, Active, Stopped, Unknown | Statut VM |

### Entité Client - Champs modifiés

| Ancien | Nouveau | Type |
|--------|---------|------|
| `Adresse` | `AddressLine1` | Renommé |
| `EmailAdministrateur` | `BillingEmail` | Renommé |
| `MotDePasseAdministrateur` | - | Supprimé |
| - | `AddressLine2` | Ajouté (nullable) |
| - | `DomainName` | Ajouté |
| - | `AdminUserId` | Ajouté (nullable) |
| - | `LicenseType` | Ajouté (enum) |

### Entité Utilisateur - Champs modifiés

| Ancien | Nouveau | Type |
|--------|---------|------|
| `Role` (RoleUtilisateur) | `Role` (UserRole) | Enum changé |
| Administrateur | Admin | Valeur changée |
| Formateur | Admin | Valeur changée |
| Stagiaire | Student | Valeur changée |

### Entité SalleDeFormation - Champs modifiés

| Ancien | Nouveau | Type |
|--------|---------|------|
| `FormateurId` | `CreatedBy` | Renommé |

**Sémantique:** Le champ identifie maintenant l'utilisateur Admin qui a créé la salle, plutôt qu'un "formateur".

### Entité MachineVirtuelle - Champs modifiés

| Ancien | Nouveau | Type |
|--------|---------|------|
| `StagiaireId` | `OwnerId` | Renommé |
| `TypeOS` (string) | `VmOsType` (enum) | Type changé |
| `TypeVM` (string) | `VmType` (enum) | Type changé |
| `DiskISO` | `VmISO` | Renommé |
| `FichierRDP` | `RdpInfo` | Renommé |
| `Supervision` | - | Supprimé |
| - | `VmMachineId` | Ajouté (nullable) |
| - | `PublicIp` | Ajouté (nullable) |
| - | `Status` | Ajouté (enum) |

## 🗄️ Migration de base de données

**Fichier de migration:** `EFModel/Migrations/20251006090212_UpdateModelToNewRequirements.cs`

### Actions principales
- Renommage de colonnes
- Conversion string → int pour les enums
- Ajout de nouvelles colonnes
- Suppression de colonnes obsolètes

### ⚠️ Attention
- Perte de données potentielle pour les colonnes supprimées
- Migration des valeurs d'enum nécessaire
- **Sauvegarder la base avant migration**

## 💾 Scripts de migration de données recommandés

```sql
-- Migrer les rôles utilisateurs AVANT la migration EF
UPDATE "Utilisateurs" 
SET "Role" = 'Admin' 
WHERE "Role" IN ('Administrateur', 'Formateur');

UPDATE "Utilisateurs" 
SET "Role" = 'Student' 
WHERE "Role" = 'Stagiaire';
```

## 🔧 Services mis à jour

| Service | Changements principaux |
|---------|----------------------|
| `ClientService` | Gestion LicenseType, nouveaux champs |
| `UtilisateurService` | RoleUtilisateur → UserRole |
| `MachineVirtuelleService` | Conversion enums, nouveaux champs |
| `SalleDeFormationService` | FormateurId → CreatedBy |

## 📦 DTOs mis à jour

Tous les DTOs ont été mis à jour pour refléter les changements du modèle. Les enums sont exposés sous forme de strings dans les DTOs pour faciliter le transfert HTTP.

## ✅ Statut des projets

| Projet | Statut | Notes |
|--------|--------|-------|
| EFModel | ✅ OK | Builds sans erreur |
| Shared (DTOs) | ✅ OK | Builds sans erreur |
| Server (Services) | ✅ OK | Builds sans erreur |
| Client (Blazor) | ⚠️ À mettre à jour | Références aux anciens champs |
| Test.CLI | ⚠️ À mettre à jour | Tests à adapter |

## 📝 Fichiers sources

### Modèle
- `EFModel/Models/Enums.cs` - Toutes les énumérations
- `EFModel/Models/Client.cs`
- `EFModel/Models/Utilisateur.cs`
- `EFModel/Models/SalleDeFormation.cs`
- `EFModel/Models/MachineVirtuelle.cs`
- `EFModel/EClassRoomDbContext.cs`

### DTOs
- `Shared/Dtos/ClientDto.cs`
- `Shared/Dtos/UtilisateurDto.cs`
- `Shared/Dtos/SalleDeFormationDto.cs`
- `Shared/Dtos/MachineVirtuelleDto.cs`

### Services
- `Server/Services/ClientService.cs`
- `Server/Services/UtilisateurService.cs`
- `Server/Services/SalleDeFormationService.cs`
- `Server/Services/MachineVirtuelleService.cs`

## 🎓 Points clés à retenir

1. **Enums fortement typés** : Tous les types précédemment en string sont maintenant des enums
2. **Nomenclature cohérente** : Les noms de champs reflètent mieux leur fonction
3. **Rôles simplifiés** : 3 rôles au lieu de différentes variantes
4. **VM ownership** : Propriété claire des VMs par les étudiants
5. **Statut VM** : Suivi du cycle de vie des machines virtuelles

## 🚀 Prochaines étapes recommandées

1. Tester la migration sur un environnement de développement
2. Mettre à jour les pages Blazor du client
3. Adapter les tests automatisés
4. Mettre à jour les services Azure
5. Former les utilisateurs sur les nouveaux concepts (si nécessaire)
6. Déployer en staging puis production

## 📞 Ressources

Pour plus de détails, consulter :
- **Documentation technique complète** : [ModelRefactoring2025.md](ModelRefactoring2025.md)
- **Spécifications du modèle** : [ModelSpecifications2025.md](ModelSpecifications2025.md)
- **Documentation originale** : [eClassRoom.md](eClassRoom.md)

---

*Document créé le 6 janvier 2025*

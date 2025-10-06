# README - Refonte du Modèle Entity Framework

## 📌 Résumé exécutif

En janvier 2025, le modèle Entity Framework de l'application eClassRoom a été refondu pour mieux correspondre aux exigences métier. Cette refonte modernise le modèle de données en introduisant des types fortement typés (enums) et une nomenclature plus claire.

## 🎯 Objectifs atteints

✅ Création de 5 nouvelles énumérations fortement typées
✅ Mise à jour de 4 entités principales (Client, Utilisateur, SalleDeFormation, MachineVirtuelle)
✅ Création d'une migration Entity Framework
✅ Mise à jour de tous les DTOs
✅ Mise à jour de tous les services backend
✅ Documentation complète en français

## 📚 Documentation disponible

### Documents principaux

1. **[QuickReference-ModelRefactoring.md](QuickReference-ModelRefactoring.md)**
   - Guide de référence rapide
   - Tableau récapitulatif des changements
   - Statut des projets
   - **À lire en premier**

2. **[ModelSpecifications2025.md](ModelSpecifications2025.md)**
   - Spécifications complètes du modèle
   - Diagrammes relationnels
   - Règles métier
   - Guide de migration

3. **[ModelRefactoring2025.md](ModelRefactoring2025.md)**
   - Guide détaillé de refactoring
   - Analyse d'impact
   - Notes de compatibilité
   - Exemples de code

## 🔑 Changements majeurs

### Nouvelles énumérations
- `LicenseType` : Academic, Professional, Gov
- `UserRole` : SuperAdmin, Admin, Student
- `VmOsType` : Linux, Windows
- `VmType` : Types de VM Azure (Standard_B2s, etc.)
- `VmStatus` : NotCreated, Created, Active, Stopped, Unknown

### Entités modifiées

| Entité | Changements principaux |
|--------|----------------------|
| **Client** | + DomainName, BillingEmail, LicenseType<br>- MotDePasseAdministrateur |
| **Utilisateur** | Role: RoleUtilisateur → UserRole |
| **SalleDeFormation** | FormateurId → CreatedBy |
| **MachineVirtuelle** | + 8 nouveaux champs<br>Conversion strings → enums |

## 🗄️ Migration de base de données

**Migration créée:** `20251006090212_UpdateModelToNewRequirements`

⚠️ **Important:** Cette migration modifie et supprime des colonnes. Sauvegarder la base de données avant d'exécuter la migration.

### Script de pré-migration recommandé

```sql
-- À exécuter AVANT la migration EF
UPDATE "Utilisateurs" 
SET "Role" = 'Admin' 
WHERE "Role" IN ('Administrateur', 'Formateur');

UPDATE "Utilisateurs" 
SET "Role" = 'Student' 
WHERE "Role" = 'Stagiaire';
```

## 🏗️ Architecture

```
┌─────────────┐
│   Client    │─────┐
└─────────────┘     │
                    │ 1:N
                    ▼
            ┌─────────────────┐
            │  Utilisateur    │
            └─────────────────┘
                    │ 1:N
                    ▼
            ┌─────────────────┐
            │ SalleDeFormation│
            └─────────────────┘
                    │ N:N
                    ▼
            ┌─────────────────┐
            │ MachineVirtuelle│
            └─────────────────┘
```

## 🔧 Statut technique

### ✅ Composants fonctionnels
- EFModel (Entities + Migration)
- Shared (DTOs)
- Server (Services + API)

### ⏳ Composants nécessitant une mise à jour
- Client (Blazor pages) - références aux anciens champs
- Test.CLI - tests à adapter
- Services Azure - mise à jour selon besoins

## 📦 Fichiers clés modifiés

### Modèle
```
EFModel/
├── Models/
│   ├── Enums.cs (nouveau)
│   ├── Client.cs (modifié)
│   ├── Utilisateur.cs (modifié)
│   ├── SalleDeFormation.cs (modifié)
│   └── MachineVirtuelle.cs (modifié)
├── EClassRoomDbContext.cs (modifié)
└── Migrations/
    └── 20251006090212_UpdateModelToNewRequirements.cs (nouveau)
```

### DTOs & Services
```
Shared/Dtos/ (tous modifiés)
Server/Services/ (tous modifiés)
```

## 🚀 Prochaines étapes

1. ✅ Modèle EF mis à jour
2. ✅ Migration créée
3. ✅ DTOs mis à jour
4. ✅ Services mis à jour
5. ✅ Documentation créée
6. ⏳ Tester la migration en dev
7. ⏳ Mettre à jour le client Blazor
8. ⏳ Adapter les tests
9. ⏳ Déployer en production

## 🎓 Pour commencer

1. Lire le [Guide de référence rapide](QuickReference-ModelRefactoring.md)
2. Consulter les [Spécifications du modèle](ModelSpecifications2025.md)
3. Examiner le [Guide de refactoring](ModelRefactoring2025.md)

## 📞 Support

Pour toute question sur la refonte :
1. Consulter les documents de référence ci-dessus
2. Examiner le code source dans `EFModel/Models/`
3. Vérifier la migration dans `EFModel/Migrations/`

---

## 🔍 Recherche rapide

| Je cherche... | Document |
|---------------|----------|
| Vue d'ensemble rapide | [QuickReference-ModelRefactoring.md](QuickReference-ModelRefactoring.md) |
| Spécifications complètes | [ModelSpecifications2025.md](ModelSpecifications2025.md) |
| Détails techniques | [ModelRefactoring2025.md](ModelRefactoring2025.md) |
| Diagramme relationnel | [ModelSpecifications2025.md](ModelSpecifications2025.md#2-diagramme-relationnel) |
| Guide de migration | [ModelSpecifications2025.md](ModelSpecifications2025.md#5-points-dattention-pour-la-migration) |
| Règles métier | [ModelSpecifications2025.md](ModelSpecifications2025.md#3-règles-métier) |

---

*Dernière mise à jour : 6 janvier 2025*

# Entity Framework Model Refactoring - January 2025

## Vue d'ensemble

Ce document décrit la refonte complète du modèle de données Entity Framework pour l'application eClassRoom selon les nouvelles spécifications métier. La refonte vise à aligner le modèle avec les exigences réelles du système de gestion de salles de formation virtuelles.

## Changements majeurs

### 1. Nouvelles énumérations

Toutes les énumérations sont maintenant fortement typées et stockées en base de données sous forme d'entiers (int) au lieu de chaînes de caractères.

#### LicenseType (Type de licence client)
```csharp
public enum LicenseType
{
    Academic,      // Licence académique
    Professional,  // Licence professionnelle
    Gov           // Licence gouvernementale
}
```

#### UserRole (Rôle utilisateur)
```csharp
public enum UserRole
{
    SuperAdmin,   // Super administrateur (gère tous les clients)
    Admin,        // Administrateur (gère un client)
    Student       // Étudiant (utilise les VMs)
}
```

**Note**: Remplace l'ancienne énumération `RoleUtilisateur` (Administrateur, Formateur, Stagiaire).

#### VmOsType (Type de système d'exploitation)
```csharp
public enum VmOsType
{
    Linux,
    Windows
}
```

#### VmType (Type de machine virtuelle Azure)
```csharp
public enum VmType
{
    Standard_B2s,
    Standard_B4ms,
    Standard_D2s_v3,
    Standard_D4s_v3,
    Standard_E2s_v3,
    Standard_E4s_v3
}
```

#### VmStatus (Statut d'une machine virtuelle)
```csharp
public enum VmStatus
{
    NotCreated,   // Pas encore créée
    Created,      // Créée mais pas démarrée
    Active,       // Active et utilisable
    Stopped,      // Arrêtée
    Unknown       // Statut inconnu
}
```

### 2. Entité Client (Customer)

Représente une entreprise cliente utilisant la solution eClassRoom.

#### Nouveaux champs
- `DomainName` (string) : Nom de domaine de l'entreprise
- `BillingEmail` (string) : Email de facturation
- `AddressLine1` (string) : Première ligne d'adresse
- `AddressLine2` (string?) : Deuxième ligne d'adresse (optionnel)
- `AdminUserId` (int?) : ID de l'utilisateur administrateur (optionnel)
- `LicenseType` (LicenseType enum) : Type de licence

#### Champs renommés
- `Adresse` → `AddressLine1`
- `EmailAdministrateur` → `BillingEmail`

#### Champs supprimés
- `MotDePasseAdministrateur` : La gestion des mots de passe se fait uniquement via l'entité Utilisateur

#### Structure finale
```csharp
public class Client
{
    public int Id { get; set; }
    public string NomSociete { get; set; }
    public string DomainName { get; set; }
    public string BillingEmail { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string CodePostal { get; set; }
    public string Ville { get; set; }
    public string Pays { get; set; }
    public string? Mobile { get; set; }
    public int? AdminUserId { get; set; }
    public LicenseType LicenseType { get; set; }
    
    // Navigation properties
    public ICollection<SalleDeFormation> SallesDeFormation { get; set; }
    public ICollection<Utilisateur> Utilisateurs { get; set; }
}
```

### 3. Entité Utilisateur (User)

Représente un utilisateur du système avec un rôle spécifique.

#### Changements
- `Role` : Utilise maintenant l'énumération `UserRole` au lieu de `RoleUtilisateur`
- Les valeurs de rôle ont changé :
  - `Administrateur` → `Admin`
  - `Formateur` → `Admin` (les formateurs sont maintenant des admins)
  - `Stagiaire` → `Student`

#### Structure finale
```csharp
public class Utilisateur
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string MotDePasse { get; set; }
    public UserRole Role { get; set; }
    public int ClientId { get; set; }
    
    // Navigation property
    public Client Client { get; set; }
}
```

### 4. Entité SalleDeFormation (Training Room)

Représente une salle de formation virtuelle où les étudiants utilisent des machines virtuelles.

#### Champs renommés
- `FormateurId` → `CreatedBy` : L'ID de l'utilisateur (avec rôle Admin) qui a créé la salle

#### Sémantique mise à jour
Le champ `CreatedBy` indique l'utilisateur Admin qui a créé la salle de formation. Cet utilisateur a la responsabilité de gérer cette salle.

#### Structure finale
```csharp
public class SalleDeFormation
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public int CreatedBy { get; set; }
    public Utilisateur Formateur { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    
    // Collections
    public ICollection<Utilisateur> Stagiaires { get; set; }
    public ICollection<MachineVirtuelle> Machines { get; set; }
}
```

### 5. Entité MachineVirtuelle (Virtual Machine)

Représente une machine virtuelle provisionnée dans Azure pour un étudiant.

#### Nouveaux champs
- `OwnerId` (int) : ID de l'utilisateur étudiant propriétaire
- `VmMachineId` (string?) : ID unique de la VM dans Azure
- `VmISO` (string?) : Référence à l'ISO dans le Blob Storage
- `PublicIp` (string?) : Adresse IP publique pour la connexion
- `Status` (VmStatus enum) : Statut actuel de la VM

#### Champs renommés
- `StagiaireId` → `OwnerId`
- `TypeOS` (string) → `VmOsType` (enum)
- `TypeVM` (string) → `VmType` (enum)
- `DiskISO` → `VmISO`
- `FichierRDP` → `RdpInfo`

#### Champs supprimés
- `Supervision` : Non requis dans les nouvelles spécifications

#### Structure finale
```csharp
public class MachineVirtuelle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public Utilisateur Stagiaire { get; set; }
    public VmOsType VmOsType { get; set; }
    public VmType VmType { get; set; }
    public string? VmMachineId { get; set; }
    public string? VmISO { get; set; }
    public string Sku { get; set; }
    public string Offer { get; set; }
    public string Version { get; set; }
    public string? RdpInfo { get; set; }
    public string? PublicIp { get; set; }
    public VmStatus Status { get; set; }
    public string? NomMarketing { get; set; }
    
    // Navigation property
    public ICollection<SalleDeFormation> Salles { get; set; }
}
```

## Mise à jour du DbContext

Le `EClassRoomDbContext` a été mis à jour pour refléter les nouveaux noms de champs :

```csharp
// MachineVirtuelle - Stagiaire (Owner)
modelBuilder.Entity<MachineVirtuelle>()
    .HasOne(m => m.Stagiaire)
    .WithMany()
    .HasForeignKey(m => m.OwnerId);  // Changé de StagiaireId à OwnerId

// SalleDeFormation - Formateur (Utilisateur who created it)
modelBuilder.Entity<SalleDeFormation>()
    .HasOne(s => s.Formateur)
    .WithMany()
    .HasForeignKey(s => s.CreatedBy)  // Changé de FormateurId à CreatedBy
    .OnDelete(DeleteBehavior.Restrict);
```

## Migration de base de données

La migration `UpdateModelToNewRequirements` a été créée et effectue les opérations suivantes :

### Modifications sur la table Clients
- Renommage : `Adresse` → `AddressLine1`
- Renommage : `EmailAdministrateur` → `BillingEmail`
- Renommage : `MotDePasseAdministrateur` → `DomainName`
- Ajout : `AddressLine2` (nullable)
- Ajout : `AdminUserId` (nullable)
- Ajout : `LicenseType` (int, default: 0)
- Modification : `Mobile` devient nullable

### Modifications sur la table SallesDeFormation
- Renommage : `FormateurId` → `CreatedBy`
- Mise à jour de la foreign key correspondante

### Modifications sur la table MachinesVirtuelles
- Renommage : `StagiaireId` → `OwnerId` (réutilisé pour `VmType` dans la migration)
- Suppression : `TypeOS`, `TypeVM`, `DiskISO`, `FichierRDP`, `Supervision`
- Ajout : `VmOsType` (int enum)
- Ajout : `VmType` (int enum)
- Ajout : `OwnerId` (int)
- Ajout : `VmMachineId` (nullable)
- Ajout : `VmISO` (nullable)
- Ajout : `PublicIp` (nullable)
- Ajout : `RdpInfo` (nullable)
- Ajout : `Status` (int enum, default: 0)
- Modification : `NomMarketing` devient nullable

### Modifications sur la table Utilisateurs
- Changement de type : `Role` passe de string à int (enum)
- Migration des valeurs existantes requise

## Impact sur les DTOs

Les DTOs (Data Transfer Objects) dans le projet Shared ont été mis à jour en conséquence :

### ClientDto
```csharp
public class ClientDto
{
    public int Id { get; set; }
    public string NomSociete { get; set; }
    public string DomainName { get; set; }
    public string BillingEmail { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string CodePostal { get; set; }
    public string Ville { get; set; }
    public string Pays { get; set; }
    public string? Mobile { get; set; }
    public int? AdminUserId { get; set; }
    public string LicenseType { get; set; }  // String pour transfert HTTP
}
```

### MachineVirtuelleDto
```csharp
public class MachineVirtuelleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public string VmOsType { get; set; }  // String pour transfert HTTP
    public string VmType { get; set; }    // String pour transfert HTTP
    public string? VmMachineId { get; set; }
    public string? VmISO { get; set; }
    public string Sku { get; set; }
    public string Offer { get; set; }
    public string Version { get; set; }
    public string? RdpInfo { get; set; }
    public string? PublicIp { get; set; }
    public string Status { get; set; }    // String pour transfert HTTP
    public string? NomMarketing { get; set; }
}
```

### SalleDeFormationDto
```csharp
public class SalleDeFormationDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public int ClientId { get; set; }
    public ClientDto Client { get; set; }
    public int CreatedBy { get; set; }     // Changé de FormateurId
    public UtilisateurDto Formateur { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public ICollection<UtilisateurDto> Stagiaires { get; set; }
    public ICollection<MachineVirtuelleDto> Machines { get; set; }
}
```

## Impact sur les Services

Les services ont été mis à jour pour utiliser les nouveaux champs et effectuer les conversions enum ↔ string :

### ClientService
- Gestion de `LicenseType` : Conversion enum ↔ string
- Mise à jour de tous les mappings de champs

### UtilisateurService
- Classe helper renommée : `RoleUtilisateurHelper` → `UserRoleHelper`
- Mise à jour des conversions : `RoleUtilisateur` → `UserRole`

### MachineVirtuelleService
- Conversion des enums `VmOsType`, `VmType`, `VmStatus` ↔ string
- Mise à jour de tous les mappings de champs

### SalleDeFormationService
- Mise à jour du champ `FormateurId` → `CreatedBy`
- Mise à jour de tous les mappings de champs dans les méthodes ToDto

## Compatibilité et migration de données

### Attention : Perte de données potentielle

La migration effectue des modifications destructives :
1. Suppression de colonnes (TypeOS, TypeVM, etc.)
2. Renommage de colonnes qui peut causer des pertes si les anciennes valeurs ne correspondent pas aux nouvelles structures

### Recommandations pour la migration

1. **Sauvegarder la base de données** avant d'exécuter la migration
2. **Script de migration de données** : Créer un script pour :
   - Migrer `TypeOS` (string) → `VmOsType` (enum)
   - Migrer `TypeVM` (string) → `VmType` (enum)
   - Migrer les rôles utilisateurs : `Administrateur` → `Admin`, `Formateur` → `Admin`, `Stagiaire` → `Student`
   - Copier `DiskISO` → `VmISO`
   - Copier `FichierRDP` → `RdpInfo`
3. **Tester en environnement de développement** avant de déployer en production

### Script de migration de données (exemple)

```sql
-- Migrer les rôles utilisateurs (avant la migration EF)
UPDATE "Utilisateurs" 
SET "Role" = 'Admin' 
WHERE "Role" IN ('Administrateur', 'Formateur');

UPDATE "Utilisateurs" 
SET "Role" = 'Student' 
WHERE "Role" = 'Stagiaire';

-- Après la migration EF, si nécessaire, corriger les valeurs d'enum
-- Note: Les enums sont stockés comme int, donc vérifier les valeurs
```

## Prochaines étapes

1. ✅ Créer les nouvelles énumérations
2. ✅ Mettre à jour les entités du modèle
3. ✅ Créer la migration EF
4. ✅ Mettre à jour les DTOs
5. ✅ Mettre à jour les services
6. ⏳ Mettre à jour les contrôleurs API
7. ⏳ Mettre à jour le client Blazor
8. ⏳ Tester la migration sur une base de développement
9. ⏳ Mettre à jour la documentation utilisateur
10. ⏳ Déployer en production

## Références

- Fichier de migration : `EFModel/Migrations/20251006090212_UpdateModelToNewRequirements.cs`
- Modèle : `EFModel/Models/`
- DTOs : `Shared/Dtos/`
- Services : `Server/Services/`
- DbContext : `EFModel/EClassRoomDbContext.cs`

# Spécifications du modèle EF - Version mise à jour (2025)

## 1. Entités du modèle EF

### Client (Customer)
Représente une entreprise cliente utilisant la solution eClassRoom.

**Propriétés:**
- `Id` : int - Identifiant unique
- `NomSociete` : string - Nom de la société
- `DomainName` : string - Nom de domaine de l'entreprise
- `BillingEmail` : string - Email de facturation
- `AddressLine1` : string - Première ligne d'adresse
- `AddressLine2` : string? - Deuxième ligne d'adresse (optionnel)
- `CodePostal` : string - Code postal
- `Ville` : string - Ville
- `Pays` : string - Pays
- `Mobile` : string? - Numéro de téléphone (optionnel)
- `AdminUserId` : int? - ID de l'utilisateur administrateur (optionnel)
- `LicenseType` : LicenseType (enum) - Type de licence (Academic, Professional, Gov)

**Relations:**
- `Utilisateurs` : ICollection<Utilisateur> - Liste des utilisateurs de ce client
- `SallesDeFormation` : ICollection<SalleDeFormation> - Liste des salles de formation

### LicenseType (Enum)
Type de licence pour un client.

**Valeurs:**
- `Academic` = 0 - Licence académique
- `Professional` = 1 - Licence professionnelle
- `Gov` = 2 - Licence gouvernementale

### Utilisateur (User)
Représente un utilisateur du système.

**Propriétés:**
- `Id` : int - Identifiant unique
- `Email` : string - Adresse email (utilisée pour la connexion)
- `Nom` : string - Nom de famille
- `Prenom` : string - Prénom
- `MotDePasse` : string - Mot de passe hashé
- `Role` : UserRole (enum) - Rôle de l'utilisateur
- `ClientId` : int - ID du client auquel appartient l'utilisateur

**Relations:**
- `Client` : Client - Client auquel appartient l'utilisateur

### UserRole (Enum)
Rôle d'un utilisateur dans le système.

**Valeurs:**
- `SuperAdmin` = 0 - Super administrateur (gère tous les clients)
- `Admin` = 1 - Administrateur (gère un client, peut créer des salles de formation)
- `Student` = 2 - Étudiant (utilise les machines virtuelles)

### SalleDeFormation (Training Room)
Représente une salle de formation virtuelle où les étudiants utilisent des machines virtuelles.

**Propriétés:**
- `Id` : int - Identifiant unique
- `Nom` : string - Nom de la salle de formation
- `ClientId` : int - ID du client propriétaire
- `CreatedBy` : int - ID de l'utilisateur (Admin) qui a créé cette salle
- `DateDebut` : DateTime - Date de début de la formation
- `DateFin` : DateTime - Date de fin de la formation

**Relations:**
- `Client` : Client - Client propriétaire de la salle
- `Formateur` : Utilisateur - Utilisateur Admin qui a créé la salle
- `Stagiaires` : ICollection<Utilisateur> - Liste des étudiants inscrits
- `Machines` : ICollection<MachineVirtuelle> - Liste des machines virtuelles provisionnées

**Note importante:** Le champ `CreatedBy` remplace l'ancien `FormateurId`. Il identifie l'utilisateur Admin qui a la responsabilité de gérer cette salle de formation.

### MachineVirtuelle (Virtual Machine)
Représente une machine virtuelle provisionnée dans Azure pour un étudiant.

**Propriétés:**
- `Id` : int - Identifiant unique
- `Name` : string - Nom de la machine virtuelle
- `OwnerId` : int - ID de l'utilisateur étudiant propriétaire
- `VmOsType` : VmOsType (enum) - Type de système d'exploitation
- `VmType` : VmType (enum) - Type/taille de VM Azure
- `VmMachineId` : string? - ID unique de la VM dans Azure (optionnel)
- `VmISO` : string? - Référence à l'ISO dans le Blob Storage (optionnel)
- `Sku` : string - SKU Azure
- `Offer` : string - Offre Azure
- `Version` : string - Version Azure
- `RdpInfo` : string? - Contenu du fichier RDP pour la connexion (optionnel)
- `PublicIp` : string? - Adresse IP publique pour la connexion (optionnel)
- `Status` : VmStatus (enum) - Statut actuel de la VM
- `NomMarketing` : string? - Nom marketing de la VM (optionnel)

**Relations:**
- `Stagiaire` : Utilisateur - Étudiant propriétaire de la VM
- `Salles` : ICollection<SalleDeFormation> - Salles de formation où cette VM est utilisée

### VmOsType (Enum)
Type de système d'exploitation pour une machine virtuelle.

**Valeurs:**
- `Linux` = 0 - Système Linux
- `Windows` = 1 - Système Windows

### VmType (Enum)
Type/taille de machine virtuelle Azure.

**Valeurs:**
- `Standard_B2s` = 0 - VM B2s (2 vCPUs, 4 GB RAM)
- `Standard_B4ms` = 1 - VM B4ms (4 vCPUs, 16 GB RAM)
- `Standard_D2s_v3` = 2 - VM D2s v3 (2 vCPUs, 8 GB RAM)
- `Standard_D4s_v3` = 3 - VM D4s v3 (4 vCPUs, 16 GB RAM)
- `Standard_E2s_v3` = 4 - VM E2s v3 (2 vCPUs, 16 GB RAM)
- `Standard_E4s_v3` = 5 - VM E4s v3 (4 vCPUs, 32 GB RAM)

**Note:** D'autres types peuvent être ajoutés selon les besoins.

### VmStatus (Enum)
Statut d'une machine virtuelle.

**Valeurs:**
- `NotCreated` = 0 - VM pas encore créée
- `Created` = 1 - VM créée mais pas démarrée
- `Active` = 2 - VM active et utilisable
- `Stopped` = 3 - VM arrêtée
- `Unknown` = 4 - Statut inconnu

### Facture (Invoice)
Représente une facture pour un client (inchangée dans cette refonte).

**Propriétés:**
- `Id` : int - Identifiant unique
- `ClientId` : int - ID du client
- `Mois` : DateTime - Mois de facturation
- `Montant` : decimal - Montant de la facture
- `Details` : string - Détails de la facture

**Relations:**
- `Client` : Client - Client facturé

### ProvisionningVM
Représente un enregistrement de provisionnement de VM (inchangée dans cette refonte).

**Propriétés:**
- `Id` : int - Identifiant unique
- `SalleDeFormationId` : int - ID de la salle de formation
- `StagiaireId` : int - ID de l'étudiant
- `VmName` : string - Nom de la VM
- `PublicIp` : string - IP publique de la VM
- `DateProvisionning` : DateTime - Date de provisionnement

**Relations:**
- `SalleDeFormation` : SalleDeFormation - Salle de formation
- `Stagiaire` : Utilisateur - Étudiant

---

## 2. Diagramme relationnel

```
┌─────────────────┐
│     Client      │
│─────────────────│
│ Id (PK)         │
│ NomSociete      │
│ DomainName      │
│ BillingEmail    │
│ AddressLine1    │
│ AddressLine2    │
│ CodePostal      │
│ Ville           │
│ Pays            │
│ Mobile          │
│ AdminUserId     │
│ LicenseType     │
└─────────────────┘
        │
        │ 1:N
        ├──────────────────────────────┐
        │                              │
        ▼                              ▼
┌─────────────────┐          ┌─────────────────────┐
│  Utilisateur    │          │  SalleDeFormation   │
│─────────────────│          │─────────────────────│
│ Id (PK)         │          │ Id (PK)             │
│ Email           │          │ Nom                 │
│ Nom             │          │ ClientId (FK)       │
│ Prenom          │          │ CreatedBy (FK)      │◄──┐
│ MotDePasse      │◄─────────│ DateDebut           │   │
│ Role            │ 1:N      │ DateFin             │   │
│ ClientId (FK)   │          └─────────────────────┘   │
└─────────────────┘                    │                │
        │                              │ N:N            │ 1:N
        │ 1:N                          │                │
        │                              ▼                │
        │                    ┌─────────────────────┐   │
        │                    │ Stagiaires          │   │
        │                    │ (Join Table)        │   │
        │                    └─────────────────────┘   │
        │                              │                │
        │                              │ N:N            │
        │                              ▼                │
        │                    ┌─────────────────────┐   │
        └────────────────────│ MachineVirtuelle    │   │
                 1:N         │─────────────────────│   │
                             │ Id (PK)             │   │
                             │ Name                │   │
                             │ OwnerId (FK)        │───┘
                             │ VmOsType            │
                             │ VmType              │
                             │ VmMachineId         │
                             │ VmISO               │
                             │ Sku                 │
                             │ Offer               │
                             │ Version             │
                             │ RdpInfo             │
                             │ PublicIp            │
                             │ Status              │
                             │ NomMarketing        │
                             └─────────────────────┘
```

---

## 3. Règles métier

### Gestion des clients
1. Un client est créé par un SuperAdmin
2. Chaque client a un type de licence (Academic, Professional, Gov)
3. Un client peut avoir plusieurs utilisateurs
4. Un client peut avoir plusieurs salles de formation

### Gestion des utilisateurs
1. Les utilisateurs ont trois rôles possibles :
   - **SuperAdmin** : Gère tous les clients et la plateforme
   - **Admin** : Gère un client spécifique, peut créer des salles de formation
   - **Student** : Utilise les machines virtuelles dans les salles de formation
2. Chaque utilisateur appartient à un seul client (sauf SuperAdmin)
3. Les mots de passe sont hashés avant stockage

### Gestion des salles de formation
1. Seuls les utilisateurs Admin peuvent créer des salles de formation
2. Une salle de formation :
   - Appartient à un client
   - A un créateur (utilisateur Admin)
   - A une date de début et une date de fin
   - Peut contenir plusieurs étudiants
   - Peut contenir plusieurs machines virtuelles

### Gestion des machines virtuelles
1. Chaque VM a un propriétaire (étudiant)
2. Une VM peut être dans plusieurs salles de formation
3. Les types de VM sont limités aux types Azure disponibles
4. Chaque VM a un statut qui évolue : NotCreated → Created → Active → Stopped
5. Les informations de connexion (RdpInfo, PublicIp) sont remplies lors du provisionnement

### Provisionnement
1. Les VMs sont provisionnées dans Azure par l'infrastructure service
2. Le provisionnement crée une VM avec :
   - Un type de VM spécifique
   - Un système d'exploitation (Linux ou Windows)
   - Une IP publique pour la connexion
   - Un fichier RDP pour les connexions Windows

---

## 4. Changements par rapport à la version précédente

### Entité Client
- ✅ Ajout de `DomainName`
- ✅ Ajout de `BillingEmail`
- ✅ Ajout de `AddressLine1` et `AddressLine2`
- ✅ Ajout de `AdminUserId`
- ✅ Ajout de `LicenseType` (enum)
- ❌ Suppression de `MotDePasseAdministrateur`
- 🔄 Renommage de `Adresse` → `AddressLine1`
- 🔄 Renommage de `EmailAdministrateur` → `BillingEmail`

### Entité Utilisateur
- 🔄 Changement de l'enum `RoleUtilisateur` → `UserRole`
- 🔄 Valeurs d'enum mises à jour : `Administrateur` → `Admin`, `Formateur` → `Admin`, `Stagiaire` → `Student`

### Entité SalleDeFormation
- 🔄 Renommage de `FormateurId` → `CreatedBy`
- 📝 Clarification de la sémantique : "créé par un Admin" au lieu de "animé par un formateur"

### Entité MachineVirtuelle
- ✅ Ajout de `OwnerId`
- ✅ Ajout de `VmOsType` (enum)
- ✅ Ajout de `VmType` (enum)
- ✅ Ajout de `VmMachineId`
- ✅ Ajout de `VmISO`
- ✅ Ajout de `PublicIp`
- ✅ Ajout de `RdpInfo`
- ✅ Ajout de `Status` (enum)
- ❌ Suppression de `Supervision`
- 🔄 Renommage de `StagiaireId` → `OwnerId`
- 🔄 Changement de `TypeOS` (string) → `VmOsType` (enum)
- 🔄 Changement de `TypeVM` (string) → `VmType` (enum)
- 🔄 Renommage de `DiskISO` → `VmISO`
- 🔄 Renommage de `FichierRDP` → `RdpInfo`

### Nouveaux enums
- ✅ `LicenseType` : Academic, Professional, Gov
- ✅ `UserRole` : SuperAdmin, Admin, Student
- ✅ `VmOsType` : Linux, Windows
- ✅ `VmType` : Standard_B2s, Standard_B4ms, etc.
- ✅ `VmStatus` : NotCreated, Created, Active, Stopped, Unknown

---

## 5. Points d'attention pour la migration

### Données existantes
1. **Rôles utilisateurs** : Les valeurs doivent être migrées
   - `Administrateur` → `Admin`
   - `Formateur` → `Admin`
   - `Stagiaire` → `Student`

2. **Types de VMs** : Les chaînes de caractères doivent être converties en enums
   - Vérifier que tous les types existants correspondent aux valeurs d'enum

3. **Systèmes d'exploitation** : Conversion string → enum
   - Vérifier que toutes les valeurs sont "Linux" ou "Windows"

4. **Statuts de VMs** : Initialiser à `NotCreated` pour les VMs existantes ou à la valeur appropriée

### Validation
- Tous les champs obligatoires doivent être renseignés
- Les dates de début doivent être antérieures aux dates de fin
- Les emails doivent être uniques par client
- Les types de licence, rôles, types de VM doivent correspondre aux valeurs d'enum

---

## 6. Fichiers de référence

- **Modèle** : `EFModel/Models/`
  - `Client.cs`
  - `Utilisateur.cs`
  - `SalleDeFormation.cs`
  - `MachineVirtuelle.cs`
  - `Enums.cs`
  
- **Migration** : `EFModel/Migrations/20251006090212_UpdateModelToNewRequirements.cs`

- **DbContext** : `EFModel/EClassRoomDbContext.cs`

- **DTOs** : `Shared/Dtos/`
  - `ClientDto.cs`
  - `UtilisateurDto.cs`
  - `SalleDeFormationDto.cs`
  - `MachineVirtuelleDto.cs`

- **Services** : `Server/Services/`
  - `ClientService.cs`
  - `UtilisateurService.cs`
  - `SalleDeFormationService.cs`
  - `MachineVirtuelleService.cs`

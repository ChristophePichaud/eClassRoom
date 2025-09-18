# Spécifications du modèle EF, des API et des Services

## 1. Entités du modèle EF

### Client
- Id : int
- NomSociete : string
- Adresse : string
- CodePostal : string
- Ville : string
- Pays : string
- EmailAdministrateur : string
- Mobile : string
- MotDePasseAdministrateur : string
- Utilisateurs : ICollection<Utilisateur>
- Salles : ICollection<SalleDeFormation>

### Utilisateur
- Id : int
- Email : string
- Nom : string
- Prenom : string
- MotDePasse : string
- Role : RoleUtilisateur
- ClientId : int
- Client : Client

### RoleUtilisateur (Enum)
- Administrateur
- Formateur
- Stagiaire

### SalleDeFormation
- Id : int
- NomFormation : string
- FormateurId : int
- Formateur : Utilisateur
- Stagiaires : ICollection<Utilisateur>
- DateDebut : DateTime
- DateFin : DateTime
- ClientId : int
- Client : Client
- Machines : ICollection<MachineVirtuelle>

### MachineVirtuelle
- Id : int
- TypeOS : string
- TypeVM : string
- Sku : string
- Offer : string
- Version : string
- DiskISO : string
- NomMarketingVM : string
- FichierRDP : string
- Supervision : string
- StagiaireId : int
- Stagiaire : Utilisateur
- SalleDeFormationId : int
- Salle : SalleDeFormation

### Facture
- Id : int
- ClientId : int
- Client : Client
- Mois : DateTime
- Montant : decimal
- Details : string

### ProvisionningVM
- Id : int
- SalleDeFormationId : int
- SalleDeFormation : SalleDeFormation
- StagiaireId : int
- Stagiaire : Utilisateur
- VmName : string
- PublicIp : string
- DateProvisionning : DateTime

---

## 2. Liste des End Points Web API

### UsersController
- GET /users : Récupère tous les utilisateurs
- GET /users/{id} : Récupère un utilisateur par son id
- POST /users : Crée un nouvel utilisateur
- PUT /users/{id} : Met à jour un utilisateur
- DELETE /users/{id} : Supprime un utilisateur

### SallesController
- GET /salles : Récupère toutes les salles de formation
- GET /salles/{id} : Récupère une salle par son id
- POST /salles : Crée une nouvelle salle
- PUT /salles/{id} : Met à jour une salle
- DELETE /salles/{id} : Supprime une salle

### MachinesController
- GET /machines : Récupère toutes les machines virtuelles
- GET /machines/{id} : Récupère une machine par son id
- POST /machines : Crée une nouvelle machine
- PUT /machines/{id} : Met à jour une machine
- DELETE /machines/{id} : Supprime une machine

### AuthController
- POST /api/auth/login : Authentifie un utilisateur et retourne un token JWT

---

## 3. Liste des Services

### UtilisateurService
- GetAllAsync() : Liste des utilisateurs
- GetByIdAsync(id) : Détail d'un utilisateur
- AddAsync(dto) : Ajout d'un utilisateur
- UpdateAsync(id, dto) : Mise à jour d'un utilisateur
- DeleteAsync(id) : Suppression d'un utilisateur

### SalleDeFormationService
- GetAllAsync() : Liste des salles de formation
- GetByIdAsync(id) : Détail d'une salle
- AddAsync(dto) : Ajout d'une salle
- UpdateAsync(id, dto) : Mise à jour d'une salle
- DeleteAsync(id) : Suppression d'une salle

### MachineVirtuelleService
- GetAllAsync() : Liste des machines virtuelles
- GetByIdAsync(id) : Détail d'une machine
- AddAsync(dto) : Ajout d'une machine
- UpdateAsync(id, dto) : Mise à jour d'une machine
- DeleteAsync(id) : Suppression d'une machine

### AuthService
- ValidateUserAsync(username, password) : Valide un utilisateur
- GenerateJwtToken(user) : Génère un token JWT

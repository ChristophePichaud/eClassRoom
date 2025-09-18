namespace Shared.Dtos
{
    public class SalleDeFormationDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int ClientId { get; set; }
        public ClientDto Client { get; set; } // Pour lecture seule, ne pas utiliser à la création
        public int FormateurId { get; set; } // Ajout de la clé étrangère vers le formateur
        public UtilisateurDto Formateur { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        // Liste des stagiaires dans la salle
        public ICollection<UtilisateurDto> Stagiaires { get; set; } = new List<UtilisateurDto>();
        public ICollection<MachineVirtuelleDto> Machines { get; set; } = new List<MachineVirtuelleDto>(); // Ajouté
    }
}

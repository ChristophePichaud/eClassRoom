namespace Shared.Dtos
{
    public class SalleDeFormationDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int ClientId { get; set; }
        public ClientDto Client { get; set; } // Ajout de la navigation vers le client
        public int FormateurId { get; set; } // Ajout de la clé étrangère vers le formateur
        public UtilisateurDto Formateur { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        // Liste des stagiaires dans la salle
        public ICollection<UtilisateurDto> Stagiaires { get; set; } = new List<UtilisateurDto>();
    }
}

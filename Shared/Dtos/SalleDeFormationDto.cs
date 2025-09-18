namespace Shared.Dtos
{
    public class SalleDeFormationDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public UtilisateurDto Formateur { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int ClientId { get; set; } // Ajout de la clé étrangère vers Client

        // Liste des stagiaires dans la salle
        public ICollection<UtilisateurDto> Stagiaires { get; set; } = new List<UtilisateurDto>();
    }
}

namespace Shared.Dtos
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string NomSociete { get; set; }
        public string Adresse { get; set; }
        public string CodePostal { get; set; } // Ajouté
        public string Ville { get; set; }      // Ajouté
        public string Pays { get; set; }       // Ajouté
        public string EmailAdministrateur { get; set; }
        public string Mobile { get; set; }     // Ajouté
        public string MotDePasseAdministrateur { get; set; }
    }
}
namespace Shared.Dtos
{
    public class MachineVirtuelleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOS { get; set; }
        public string TypeVM { get; set; }
        public string Sku { get; set; }
        public string Offer { get; set; }
        public string Version { get; set; }
        public string DiskISO { get; set; }
        public string NomMarketing { get; set; } // Renommé depuis NomMarketingVM
        public string FichierRDP { get; set; }
        public string Supervision { get; set; }
        public int StagiaireId { get; set; }
        // Pas de navigation vers Salles ici (évite les cycles)
    }
}

using System.Collections.Generic;

public class MachineVirtuelle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TypeOS { get; set; }
    public string TypeVM { get; set; }
    public string Sku { get; set; }
    public string Offer { get; set; }
    public string Version { get; set; }
    public string DiskISO { get; set; }
    public string NomMarketing { get; set; } // Garde ce nom en EFModel
    public string FichierRDP { get; set; }
    public string Supervision { get; set; }
    public int StagiaireId { get; set; }
    public Utilisateur Stagiaire { get; set; }
    public ICollection<SalleDeFormation> Salles { get; set; } = new List<SalleDeFormation>();
}
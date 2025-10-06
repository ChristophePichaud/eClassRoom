using System.Collections.Generic;
using EFModel.Models;

    /// <summary>
    /// Represents a customer/client company in the eClassRoom system
    /// </summary>
    public class Client
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Company name
        /// </summary>
        public string NomSociete { get; set; }
        
        /// <summary>
        /// Domain name of the company
        /// </summary>
        public string DomainName { get; set; }
        
        /// <summary>
        /// Billing email address
        /// </summary>
        public string BillingEmail { get; set; }
        
        /// <summary>
        /// Address line 1
        /// </summary>
        public string AddressLine1 { get; set; }
        
        /// <summary>
        /// Address line 2 (optional)
        /// </summary>
        public string? AddressLine2 { get; set; }
        
        /// <summary>
        /// Zip/Postal code
        /// </summary>
        public string CodePostal { get; set; }
        
        /// <summary>
        /// City
        /// </summary>
        public string Ville { get; set; }
        
        /// <summary>
        /// Country name
        /// </summary>
        public string Pays { get; set; }
        
        /// <summary>
        /// Phone number (optional)
        /// </summary>
        public string? Mobile { get; set; }
        
        /// <summary>
        /// Administrator user ID for this client
        /// </summary>
        public int? AdminUserId { get; set; }
        
        /// <summary>
        /// Type of license (Academic, Professional, Gov)
        /// </summary>
        public LicenseType LicenseType { get; set; }

        // Navigation properties
        public ICollection<SalleDeFormation> SallesDeFormation { get; set; } = new List<SalleDeFormation>();
        public ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
    }
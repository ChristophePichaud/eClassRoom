using System;
using System.Collections.Generic;

namespace Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for Training Room
    /// </summary>
    public class SalleDeFormationDto
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Name of the training room
        /// </summary>
        public string Nom { get; set; }
        
        /// <summary>
        /// Customer/Client ID that owns this training room
        /// </summary>
        public int ClientId { get; set; }
        public ClientDto Client { get; set; } // For read only, not used on creation
        
        /// <summary>
        /// User ID who created this training room (must be Admin role)
        /// </summary>
        public int CreatedBy { get; set; }
        public UtilisateurDto Formateur { get; set; }
        
        /// <summary>
        /// Start date of the training
        /// </summary>
        public DateTime DateDebut { get; set; }
        
        /// <summary>
        /// End date of the training
        /// </summary>
        public DateTime DateFin { get; set; }

        /// <summary>
        /// List of students in this training room
        /// </summary>
        public ICollection<UtilisateurDto> Stagiaires { get; set; } = new List<UtilisateurDto>();
        
        /// <summary>
        /// List of virtual machines in this training room
        /// </summary>
        public ICollection<MachineVirtuelleDto> Machines { get; set; } = new List<MachineVirtuelleDto>();
    }
}

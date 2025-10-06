using System;
using System.Collections.Generic;

    /// <summary>
    /// Training room where students will use virtual machines
    /// </summary>
    public class SalleDeFormation
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
        public Client Client { get; set; }
        
        /// <summary>
        /// User ID who created this training room (must be Admin role)
        /// </summary>
        public int CreatedBy { get; set; }
        public Utilisateur Formateur { get; set; }
        
        /// <summary>
        /// Start date of the training
        /// </summary>
        public DateTime DateDebut { get; set; }
        
        /// <summary>
        /// End date of the training
        /// </summary>
        public DateTime DateFin { get; set; }
        
        /// <summary>
        /// List of students (users with Student role) in this training room
        /// </summary>
        public ICollection<Utilisateur> Stagiaires { get; set; } = new List<Utilisateur>();
        
        /// <summary>
        /// List of virtual machines provisioned in this training room
        /// </summary>
        public ICollection<MachineVirtuelle> Machines { get; set; } = new List<MachineVirtuelle>();
    }
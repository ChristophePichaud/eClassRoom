using System;
using System.Collections.Generic;

    public class SalleDeFormation
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } // Navigation vers Client
        public int FormateurId { get; set; } // Ajout de la clé étrangère
        public Utilisateur Formateur { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public ICollection<Utilisateur> Stagiaires { get; set; } = new List<Utilisateur>();
        public ICollection<MachineVirtuelle> Machines { get; set; } = new List<MachineVirtuelle>();
    }
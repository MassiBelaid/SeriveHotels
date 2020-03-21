using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceHotels
{
    public class Agence
    {
        public Agence(string id, string mdp, int pourcentage)
        {
            this.identifiant = id;
            this.motDePasse = mdp;
            this.pourcentage = pourcentage;
        }

        public string identifiant { get; set; }
        public string motDePasse { get; set; }
        public int pourcentage { get; set; }
        public Dictionary<int,Chambre> offres { get; set; }
    }
}
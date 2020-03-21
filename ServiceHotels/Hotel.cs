using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceHotels
{
    
    public class Hotel
    {

        public Hotel() { }
        public Hotel(int id, string nomHotel, string categorie, string payes, string ville, string rue, int numRue, int nbEtoiles)
        {
            this.id = id;
            this.nomHotel = nomHotel;
            this.categorie = categorie;
            this.payes = payes;
            this.ville = ville;
            this.rue = rue;
            this.numeroRue = numRue;
            this.nbEtoiles = nbEtoiles;
        }




        public int id { get; }
        public string nomHotel { get; set; }
        public string categorie { get; set; }
        public string payes { get; set; }
        public string ville { get; set; }
        public string rue { get; set; }
        public int numeroRue { get; set; }
        public int nbEtoiles { get; set; }

        private List<Chambre> chambres = new List<Chambre>();

        public void AddChambre(Chambre c)
        {
            this.chambres.Add(c);
            c.hotel = this;
        }

        public Chambre getChambre(int i)
        {
            return this.chambres[i];
        }

        public List<Chambre> getListeChambres()
        {
            return this.chambres;
        }

        public List<Chambre> getChambresDispo(int nbP, DateTime dA, DateTime dD)
        {
            List<Chambre> lCh = new List<Chambre>();
            foreach(Chambre ch in chambres)
            {
                if (ch.getDisponibilite(dA,dD) && ch.nbLits >= nbP)
                {
                    lCh.Add(ch);
                }
            }
            return lCh;
        }

    }
}
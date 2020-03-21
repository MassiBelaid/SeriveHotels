using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceHotels
{
   
    public class Chambre
    {

        public Chambre(int num, int nbLits, long prix)
        {
            this.num = num;
            this.nbLits = nbLits;
            this.prix = prix;
            this.reservations = new Dictionary<string,Reservation>();
        }

        public int num { get; set; }
        public int nbLits { get; set; }
        public long prix { get; set; }
        public Dictionary<string,Reservation> reservations;

        private Hotel _hotel;
        public Hotel hotel
        {
            get
            {
                if (this._hotel == null)
                {
                    Console.WriteLine("Pas encore d'hotel");
                    return null;
                }
                else { return this._hotel; }
            }
            set
            {
                //this._hotel.AddChambre(this);
                this._hotel = value;
            }
        }

        public Boolean getDisponibilite(DateTime dA, DateTime dD)
        {
            Boolean dispo = true;
            foreach (KeyValuePair<string,Reservation> r in reservations)
            {
                if (DateTime.Compare(dA, r.Value.dateDepart) < 0 || DateTime.Compare(r.Value.dateArrivee, dD) <= 0)
                {
                    dispo = false;
                }
            }
            return dispo;
        }


    }
}
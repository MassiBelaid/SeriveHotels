using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServiceHotels
{
    /// <summary>
    /// Description résumée de ServiceHotel
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceHotel : System.Web.Services.WebService
    {

        private List<Hotel> listHotel = new List<Hotel>();
        private Dictionary<string, Agence> listeAgences = new Dictionary<string, Agence>();

        public ServiceHotel()
        {

            Hotel h1 = new Hotel(01, "hilton", "Luxe", "France", "paris", "rivoli", 35, 3);
            h1.AddChambre(new Chambre(1, 2, 75));
            h1.AddChambre(new Chambre(2, 3, 80));
            h1.AddChambre(new Chambre(3, 3, 80));
            h1.AddChambre(new Chambre(4, 3, 80));
            h1.AddChambre(new Chambre(5, 3, 80));
            h1.AddChambre(new Chambre(6, 3, 80));
            h1.AddChambre(new Chambre(7, 3, 80));

            Hotel h2 = new Hotel(02, "ibis", "Familliale", "France", "paris", "Lodeve", 25, 1);
            h2.AddChambre(new Chambre(1, 2, 35));
            h2.AddChambre(new Chambre(2, 3, 40));
            h2.AddChambre(new Chambre(3, 3, 40));
            h2.AddChambre(new Chambre(4, 3, 40));
            h2.AddChambre(new Chambre(5, 3, 55));
            h2.AddChambre(new Chambre(6, 3, 25));
            h2.AddChambre(new Chambre(7, 3, 47));

            Hotel h3 = new Hotel(03, "ibis", "Familliale", "France", "montpellier", "arceaux", 64, 2);
            h3.AddChambre(new Chambre(1, 2, 25));
            h3.AddChambre(new Chambre(2, 3, 15));
            h3.AddChambre(new Chambre(3, 3, 30));
            h3.AddChambre(new Chambre(4, 3, 30));
            h3.AddChambre(new Chambre(5, 3, 45));
            h3.AddChambre(new Chambre(6, 3, 15));
            h3.AddChambre(new Chambre(7, 3, 40));


            Hotel h4 = new Hotel(04, "f1", "bien", "France", "montpellier", "triolet", 21, 4);
            h4.AddChambre(new Chambre(1, 2, 45));
            h4.AddChambre(new Chambre(2, 3, 35));
            h4.AddChambre(new Chambre(3, 3, 50));
            h4.AddChambre(new Chambre(4, 3, 50));
            h4.AddChambre(new Chambre(5, 3, 65));
            h4.AddChambre(new Chambre(6, 3, 25));
            h4.AddChambre(new Chambre(7, 3, 60));


            listHotel.Add(h1);
            listHotel.Add(h2);
            listHotel.Add(h3);
            listHotel.Add(h4);

            listeAgences["Air BNB"] = new Agence("Air BNB", "012345",15);

        }


        [WebMethod]
        public List<string> ConsulterDisponibilité(string idAgence, string mdpAgence, int nbPers, DateTime dA, DateTime dD)
        {
            List<string> l = new List<string>();
            Dictionary<int, Chambre> d = new Dictionary<int, Chambre>();

            Agence a = listeAgences[idAgence];
            double numberDays = (dD - dA).TotalDays;

            if (a != null && a.motDePasse == mdpAgence)
            {
                int numero = 0;
                foreach (Hotel h in listHotel)
                {
                    foreach (Chambre c in h.getChambresDispo(nbPers, dA, dD))
                    {
                        double prixNuit = c.prix + (c.prix * (double)a.pourcentage / 100);
                        numero++;
                        l.Add("num : " + numero + ", Hotel : " + c.hotel.nomHotel + ", Ville : " + 
                            c.hotel.ville + ", places : " + c.nbLits + " places " + ", Prix : " +
                            prixNuit + " euros/nuit, "+"total pour "+numberDays+" nuits : "+(numberDays*prixNuit)+" euros.");
                        d.Add(numero, c);
                        
                    }
                }
                listeAgences[idAgence].offres = d;
                
            }
            else
            {
                l.Add("Compte agence incorrecte");
            }

            return l;
        }


        [WebMethod]
        public string Reserver(string idAgence, string mdpAgence, int numOffre, string nom,DateTime dA, DateTime dD, int nbPers, string prenom, string numCarte)
        {
            Agence a = listeAgences[idAgence];
            

            if (a != null && a.motDePasse == mdpAgence)
            {
                Dictionary<int, Chambre> d = new Dictionary<int, Chambre>();

                int numero = 0;
                foreach (Hotel h in listHotel)
                {
                    foreach (Chambre c in h.getChambresDispo(nbPers, dA, dD))
                    {
                        numero++;
                        d.Add(numero, c);

                    }
                }
                listeAgences[idAgence].offres = d;



                Chambre ch = listeAgences[idAgence].offres[numOffre];
                
                if (ch.getDisponibilite(dA, dD))
                {
                    string idReserv = numOffre+ch.hotel.nomHotel+ch.num+dA.Day+dA.Month;
                    ch.reservations.Add(idReserv,new Reservation(new Client(nom,prenom,numCarte),dA,dD));
                    return idReserv;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return "Compte Agence Incorrecte";
            }

        }

        [WebMethod]
        public string SendImage()
        {
            string imageStr = "";
            
            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("img")+"\\chambre1.png");
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageByte = stream.ToArray();
            imageStr = Convert.ToBase64String(imageByte);
            stream.Dispose();
            image.Dispose();

            return imageStr;
        }

    }
}

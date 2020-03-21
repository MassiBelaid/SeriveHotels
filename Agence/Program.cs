using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agence
{
    class Program
    {
        

        static void Main(string[] args)
        {
            ServiceHotel.ServiceHotelSoapClient serviceH = new ServiceHotel.ServiceHotelSoapClient();
            Boolean correcte = false;
            List<string> listOffres = new List<string>();
            int nbLits = 0;
            string input;
            DateTime dateArrive = new DateTime(), dateDepart = new DateTime();
            int numOffresCHoisie = 0;
            Boolean b = true;
            Console.WriteLine("Bienvenue dans notre agence de reservation de chambre d'hotel");
            


            while (true) {
                correcte = false;

                while (!correcte)
                {
                    b = true;
                    while (b) 
                { 
                    Console.WriteLine("Entrez le nombre de lits dans la chambre que vous souhaitez");
                    input = Console.ReadLine();
                    try
                    {
                        nbLits = Int32.Parse(input);
                        b = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine(input+" n'est pas correcte");
                    }
                }
                    int mA = 0, mD = 0, jA = 0, jD = 0;
                    //Entrée Date d'arrivée
                    b = true;
                    while (b) 
                    { 
                        Console.WriteLine("Entrez le numero du mois de votre arrivée ");
                        input = Console.ReadLine();
                        try
                        {
                            mA = Int32.Parse(input);
                            if (mA < 13 && mA > 0)
                            { 
                                b = false;
                            }
                            else { Console.WriteLine("Mois incorrecte"); }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(input + " n'est pas correcte");
                        }

                    }
                    b = true;
                    while (b)
                    {
                        Console.WriteLine("Entrez le numero du jour de votre arrivée ");
                        input = Console.ReadLine();
                        try
                        {
                            jA = Int32.Parse(input);
                            if (jA < 32 && jA > 0)
                            {
                                b = false;
                            }
                            else { Console.WriteLine("Jour incorrecte"); }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(input + " n'est pas correcte");
                        }

                    }

                    //Entrée date de depart
                    b = true;
                    while (b)
                    {
                        Console.WriteLine("Entrez le numero du mois de votre départ ");
                        input = Console.ReadLine();
                        try
                        {
                            mD = Int32.Parse(input);
                            if (mD < 13 && mD > 0)
                            {
                                b = false;
                            }
                            else { Console.WriteLine("Mois incorrecte"); }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(input + " n'est pas correcte");
                        }

                    }
                    b = true;
                    while (b)
                    {
                        Console.WriteLine("Entrez le numero du jour de votre Départ ");
                        input = Console.ReadLine();
                        try
                        {
                            jD = Int32.Parse(input);
                            if (jD < 32 && jD > 0)
                            {
                                b = false;
                            }
                            else { Console.WriteLine("Jour incorrecte"); }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(input + " n'est pas correcte");
                        }

                    }
                    dateArrive = new DateTime(2020,mA,jA);
                    dateDepart = new DateTime(2020,mD,jD);

                    if (DateTime.Compare(dateDepart,dateArrive) <= 0)
                    {
                        Console.WriteLine("Impossible car date depart <= date arrivé");
                    }
                    else
                    {
                        correcte = true;
                    }
                    
                }

                listOffres = serviceH.ConsulterDisponibilité("Air BNB", "012345", nbLits, dateArrive, dateDepart);
                if (listOffres.Count() > 0) { 
                    foreach(string off in listOffres)
                    {
                        Console.WriteLine(off);
                    }
                    correcte = false;
                    while (!correcte)
                    {
                        Console.WriteLine("Entrez le numero de l'offre que vous souhaitez");
                        input = Console.ReadLine();
                        try
                        {
                            numOffresCHoisie = Int32.Parse(input);
                            if (numOffresCHoisie <= listOffres.Count() && numOffresCHoisie > 0)
                            {
                                correcte = true;
                            }
                            else { Console.WriteLine("Offre inexistante"); }
                        }   
                        catch (FormatException)
                        {
                            Console.WriteLine(input + " n'est pas correcte");
                        }
                    }

                    Console.WriteLine("Entrez votre nom");
                    string nom = Console.ReadLine();
                    Console.WriteLine("Entrez votre prenom");
                    string prenom = Console.ReadLine();
                    Console.WriteLine("Entrez le numero de votre carte bancaire");
                    int numCarte = 0;
                    correcte = false;
                    while (!correcte)
                    {
                        input = Console.ReadLine();
                        try
                        {
                            numCarte = Int32.Parse(input);
                            correcte = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(input + " n'est pas correcte");
                        }
                    }

                    string reponse = serviceH.Reserver("Air BNB", "012345", numOffresCHoisie,nom,dateArrive,dateDepart,nbLits,prenom, numCarte.ToString());

                    if (reponse == null)
                    {
                        Console.WriteLine("Nous somme désolés, l'offre n'est plus disponible, veuillez réessayer");
                    }
                    else
                    {
                        Console.WriteLine("Chambre réservé avec le numéro de reservation " + reponse + " vous pourvez faire une autre reservation si vous le souhaitez");
                    }
                }
                else
                {
                    Console.WriteLine("Aucune offre disponible pour ces dates/nombre de lits, veuillez réessayer");
                }

            }

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientAgence
{
    public partial class Form2 : Form
    {
        public Form2(List<string> listOffres, DateTime dA, DateTime dD, int nbL)
        {
            InitializeComponent();
            listBoxOffres.DataSource = listOffres;
            this.dateArrivee = dA;
            this.dateDepart = dD;
            this.nbLits = nbL;
            
        }

        private DateTime dateArrivee;
        private DateTime dateDepart;
        private int nbLits;

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonReserver_Click(object sender, EventArgs e)
        {
            string nomClient = textBoxNom.Text;
            string prenomClient = textBoxPrenom.Text;
            string cbClient = textBoxCB.Text;

            if (nomClient == "" || prenomClient == "" || cbClient == "")
            {
                MessageBox.Show("Veuillez remplir tout les champs pour effectuer une reservation");
            }
            else
            {
                ServiceHotel.ServiceHotelSoapClient serviceH = new ServiceHotel.ServiceHotelSoapClient();
                string reponse = serviceH.Reserver("Air BNB", "012345", (listBoxOffres.SelectedIndex + 1), nomClient, dateArrivee, dateDepart, nbLits, prenomClient, cbClient);
                if (reponse == null)
                {
                    MessageBox.Show("Nous somme désolés, l'offre n'est plus disponible, veuillez réessayer");
                }
                else
                {
                    //MessageBox.Show("Chambre réservé avec le numéro de reservation " + reponse);
                    
                    string imageStr = serviceH.SendImage();
                    ImageForm imageForm = new ImageForm(imageStr,reponse);
                    imageForm.Show();
                    this.Hide();
                }
            }
            
        }
    }
}

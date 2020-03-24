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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelNbLits_Click(object sender, EventArgs e)
        {

        }

        private void buttonChercher_Click(object sender, EventArgs e)
        {
            int nbLits = comboBox1.SelectedIndex;
            string dateArriveString = dateTimePickerArrivee.Value.ToString("yyyy/MM/dd");
            string dateDepartString = dateTimePickerDepart.Value.ToString("yyyy-MM-dd");
            DateTime dateArrive = DateTime.Parse(dateArriveString);
            DateTime dateDepart = DateTime.Parse(dateDepartString);
            DateTime dateNow = DateTime.Now;



            if (nbLits == -1)
            {
                MessageBox.Show("Veuillez choisir un nombre de lits");
            }
            else
            {
                if (dateArrive.Date < dateNow.Date)
                {
                    MessageBox.Show("Date Arrivée incorrecte");
                }
                else if(dateArrive.Date >= dateDepart.Date)
                {
                    MessageBox.Show("Date Depart ne doit pas etre inferieur a la date d'arriveé");
                }
                else
                {
                    nbLits++;
                    ServiceHotel.ServiceHotelSoapClient serviceH = new ServiceHotel.ServiceHotelSoapClient();
                    List<string> listOffres = new List<string>();
                    listOffres = serviceH.ConsulterDisponibilité("Air BNB", "012345", nbLits, dateArrive, dateDepart);
                    //MessageBox.Show(listOffres[0]);
                    Form2 f2 = new Form2(listOffres,dateArrive,dateDepart, nbLits);
                    f2.Show();
                    this.Hide();
                }
                
            }
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

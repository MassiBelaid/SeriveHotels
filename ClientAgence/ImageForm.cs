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
    public partial class ImageForm : Form
    {
        public ImageForm(String image, string idOffre)
        {
            InitializeComponent();
            this.img = image;
            this.labelIdOffre.Text = "Chambre réservé avec le numéro de reservation " + idOffre;
        }

        private string img;

        private void ImageForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonAfficher_Click(object sender, EventArgs e)
        {
            byte[] imageByte = Convert.FromBase64String(this.img);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(imageByte, 0, imageByte.Length);
            stream.Write(imageByte, 0, imageByte.Length);
            Image image = Image.FromStream(stream, true);
            Graphics g = this.CreateGraphics();
            g.DrawImage(image, 0, 80, this.Width, this.Height-80);
        }
    }
}

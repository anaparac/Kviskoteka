using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kviskoteka
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            this.Text = "Postavke";
        }

        public int[] protivnik1 = new int[4];
        public int[] protivnik2 = new int[4];

        private void buttonOdustani_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            //-----------------------------------
            //PROTIVNIK 1
            //-----------------------------------
            //ABCpitalica 
            if (protivnik1[0] == 1) this.radioButton11ABCpitalica.Checked = true;
            if (protivnik1[0] == 2) this.radioButton12ABCpitalica.Checked = true;
            if (protivnik1[0] == 3) this.radioButton13ABCpitalica.Checked = true;

            //Asocijacija
            if (protivnik1[1] == 1) this.radioButton11Asocijacija.Checked = true;
            if (protivnik1[1] == 2) this.radioButton12Asocijacija.Checked = true;
            if (protivnik1[1] == 3) this.radioButton13Asocijacija.Checked = true;
            
            //Detekcija
            if (protivnik1[2] == 1) this.radioButton11Detekcija.Checked = true;
            if (protivnik1[2] == 2) this.radioButton12Detekcija.Checked = true;
            if (protivnik1[2] == 3) this.radioButton13Detekcija.Checked = true;

            //Zavrsna
            if (protivnik1[3] == 1) this.radioButton11Zavrsna.Checked = true;
            if (protivnik1[3] == 2) this.radioButton12Zavrsna.Checked = true;
            if (protivnik1[3] == 3) this.radioButton13Zavrsna.Checked = true;

            //-----------------------------------
            //PROTIVNIK 2
            //-----------------------------------
            //ABCpitalica 
            if (protivnik2[0] == 1) this.radioButton21ABCpitalica.Checked = true;
            if (protivnik2[0] == 2) this.radioButton22ABCpitalica.Checked = true;
            if (protivnik2[0] == 3) this.radioButton23ABCpitalica.Checked = true;

            //Asocijacija
            if (protivnik2[1] == 1) this.radioButton21Asocijacija.Checked = true;
            if (protivnik2[1] == 2) this.radioButton22Asocijacija.Checked = true;
            if (protivnik2[1] == 3) this.radioButton23Asocijacija.Checked = true;

            //Detekcija
            if (protivnik2[2] == 1) this.radioButton21Detekcija.Checked = true;
            if (protivnik2[2] == 2) this.radioButton22Detekcija.Checked = true;
            if (protivnik2[2] == 3) this.radioButton23Detekcija.Checked = true;

            //Zavrsna
            if (protivnik2[3] == 1) this.radioButton21Zavrsna.Checked = true;
            if (protivnik2[3] == 2) this.radioButton22Zavrsna.Checked = true;
            if (protivnik2[3] == 3) this.radioButton23Zavrsna.Checked = true;

        }

       
        private void buttonPostavi_Click(object sender, EventArgs e)
        {
            //-----------------------------------
            //PROTIVNIK 1
            //-----------------------------------
            //ABCpitalica 
            bool postavljeno = false; 
            if (this.radioButton11ABCpitalica.Checked)
            {
                protivnik1[0] = 1;
                postavljeno = true; 
            }
            if (this.radioButton12ABCpitalica.Checked)
            {
                protivnik1[0] = 2;
                postavljeno = true;
            }
            if (this.radioButton13ABCpitalica.Checked)
            {
                protivnik1[0] = 3;
                postavljeno = true; 
            }

            if (!postavljeno)
            {
                MessageBox.Show("Sve razine moraju biti postavljene", "Upozorenje", MessageBoxButtons.OK);
                return;
            }

            //Asocijacija
            postavljeno = false;
            if (this.radioButton11Asocijacija.Checked)
            {
                protivnik1[1] = 1;
                postavljeno = true;
            }
            if (this.radioButton12Asocijacija.Checked)
            {
                protivnik1[1] = 2;
                postavljeno = true;
            }
            if (this.radioButton13Asocijacija.Checked)
            {
                protivnik1[1] = 3;
                postavljeno = true;
            }

            if (!postavljeno)
            {
                MessageBox.Show("Sve razine moraju biti postavljene", "Upozorenje", MessageBoxButtons.OK);
                return;
            }

            //Detekcija
            postavljeno = false;
            if (this.radioButton11Detekcija.Checked)
            {
                protivnik1[2] = 1;
                postavljeno = true;
            }
            if (this.radioButton12Detekcija.Checked)
            {
                protivnik1[2] = 2;
                postavljeno = true;
            }
            if (this.radioButton13Detekcija.Checked)
            {
                protivnik1[2] = 3;
                postavljeno = true;
            }

            if (!postavljeno)
            {
                MessageBox.Show("Sve razine moraju biti postavljene", "Upozorenje", MessageBoxButtons.OK);
                return;
            }

            //Zavrsna
            postavljeno = false;
            if (this.radioButton11Zavrsna.Checked)
            {
                protivnik1[3] = 1;
                postavljeno = true;
            }
            if (this.radioButton12Zavrsna.Checked)
            {
                protivnik1[3] = 2;
                postavljeno = true;
            }
            if (this.radioButton13Zavrsna.Checked)
            {
                protivnik1[3] = 3;
                postavljeno = true;
            }

            if (!postavljeno)
            {
                MessageBox.Show("Sve razine moraju biti postavljene", "Upozorenje", MessageBoxButtons.OK);
                return;
            }

            Console.WriteLine("Protivnik1--> " + protivnik1[0] + ", " + protivnik1[1] + ", " + protivnik1[2] + ", " + protivnik1[3]);

            //-----------------------------------
            //PROTIVNIK 2
            //-----------------------------------
            //ABCpitalica 
            postavljeno = false; 
            if (this.radioButton21ABCpitalica.Checked)
            {
                protivnik2[0] = 1;
                postavljeno = true;
            }
            if (this.radioButton22ABCpitalica.Checked)
            {
                protivnik2[0] = 2;
                postavljeno = true;
            }
            if (this.radioButton23ABCpitalica.Checked)
            {
                protivnik2[0] = 3;
                postavljeno = true;
            }

            if (!postavljeno)
            {
                MessageBox.Show("Sve razine moraju biti postavljene", "Upozorenje", MessageBoxButtons.OK);
                return;
            }

            //Asocijacija
            postavljeno = false; 
            if (this.radioButton21Asocijacija.Checked)
            {
                protivnik2[1] = 1;
                postavljeno = true;
            }
            if (this.radioButton22Asocijacija.Checked)
            {
                protivnik2[1] = 2;
                postavljeno = true;
            }
            if (this.radioButton23Asocijacija.Checked)
            {
                protivnik2[1] = 3;
                postavljeno = true;
            }

            if (!postavljeno)
            {
                MessageBox.Show("Sve razine moraju biti postavljene", "Upozorenje", MessageBoxButtons.OK);
                return;
            }
            //Detekcija
            postavljeno = false; 
            if (this.radioButton21Detekcija.Checked)
            {
                protivnik2[2] = 1;
                postavljeno = true;
            }
            if (this.radioButton22Detekcija.Checked)
            {
                protivnik2[2] = 2;
                postavljeno = true;
            }
            if (this.radioButton23Detekcija.Checked)
            {
                protivnik2[2] = 3;
                postavljeno = true;
            }

            if (!postavljeno)
            {
                MessageBox.Show("Sve razine moraju biti postavljene", "Upozorenje", MessageBoxButtons.OK);
                return;
            }
            //Zavrsna
            postavljeno = false;
            if (this.radioButton21Zavrsna.Checked)
            {
                protivnik2[3] = 1;
                postavljeno = true;
            }
            if (this.radioButton22Zavrsna.Checked)
            {
                protivnik2[3] = 2;
                postavljeno = true;
            }
            if (this.radioButton23Zavrsna.Checked)
            {
                protivnik2[3] = 3;
                postavljeno = true;
            }

            if (!postavljeno)
            {
                MessageBox.Show("Sve razine moraju biti postavljene", "Upozorenje", MessageBoxButtons.OK);
                return;
            }
            Console.WriteLine("Protivnik1--> " + protivnik2[0] + ", " + protivnik2[1] + ", " + protivnik2[2] + ", " + protivnik2[3]);

            this.Close();
        }

    }
}

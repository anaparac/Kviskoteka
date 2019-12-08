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
    public partial class FormScore : Form
    {
        public FormScore()
        {
            InitializeComponent();
            this.Text = "Moji rezultati";
        }

        internal Klase.ClassBodovi klasaBodovi;

        private void buttonVrati_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormScore_Load(object sender, EventArgs e)
        {
            string[,] tablica = klasaBodovi.dohvatiImenaBodove(); 

            //ocekujem tablicu sa 10redaka tj ispisujemo 10najboljih
            for(int i = 0; i < 10; i++)
            {
                for( int j = 0; j < 6; j++)
                {
                    Label labela = new Label();
                    labela.Text = tablica[i,j];
                    labela.Parent = this.tableLayoutPanel1;
                    labela.Dock = DockStyle.Fill;
                    labela.TextAlign = ContentAlignment.MiddleCenter;
                    this.tableLayoutPanel1.Controls.Add(labela, j+1, i+2);
                }
            }

            //klasaBodovi.noviRezultat("NOVI REZULTAT", 400, 300, 500, 100);
            
        }
    }
}

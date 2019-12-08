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
    public partial class Administrator : Form
    {
        private int brojDetekcija = 0;
        private string[,] detekcija = new string[10, 5];
        private string osoba = "";
        public Administrator()
        {
            this.BackColor = System.Drawing.Color.SkyBlue;
            InitializeComponent();

            prvi.Select();
        }

        private void spremiABC_Click(object sender, EventArgs e)
        {
            if(pitanjeABC.Text == "" || aABC.Text == "" || bABC.Text == "" || cABC.Text == "" )
            {
                MessageBox.Show("Ispunite sva polja kako biste dodali novo pitanje.");
            }
            else
            {
                using (System.IO.StreamWriter fileABC = new System.IO.StreamWriter(@"ABC.txt", true, System.Text.Encoding.Default))
                {
                    fileABC.WriteLine(pitanjeABC.Text);
                    fileABC.WriteLine(aABC.Text);
                    fileABC.WriteLine(bABC.Text);
                    fileABC.WriteLine(cABC.Text);

                    pitanjeABC.Text = "";
                    aABC.Text = "";
                    bABC.Text = "";
                    cABC.Text = "";
                }
                MessageBox.Show("Uspješno ste dodali novo pitanje!");
            }
        }

        private void spremiAsocijaciju_Click(object sender, EventArgs e)
        {
            List<string> asocijacije = new List<string>();
            bool zastava = false;
            for (int i = 1; i < 21; ++i)
            {
                TextBox t = this.Controls.Find("asocijacija" + i.ToString(), true)[0] as TextBox;
                if (t.Text.Length == 0)
                {
                    zastava = true;
                    break;
                }
                asocijacije.Add(t.Text);
            }

            if (asocijacijaRjesenje.Text.Length == 0)
                zastava = true;

            if (zastava)
                MessageBox.Show("Ispunite sva polja kako biste dodali novu asocijaciju.");
            else
            {
                using (System.IO.StreamWriter fileAsocijacije = new System.IO.StreamWriter(@"Asocijacija.txt", true, System.Text.Encoding.Default))
                {
                    fileAsocijacije.WriteLine(asocijacijaRjesenje.Text);
                    for (int i = 0; i < 20; ++i)
                        fileAsocijacije.WriteLine(asocijacije[i]);
                }
                for (int i = 1; i < 21; ++i)
                {
                    TextBox t = this.Controls.Find("asocijacija" + i.ToString(), true)[0] as TextBox;
                    t.Text = "";
                }
                asocijacijaRjesenje.Text = "";
                MessageBox.Show("Uspješno ste kreirali novu igru asocijacije!");
            }
        }

        private void sljedeceDetekcija_Click(object sender, EventArgs e)
        {
            if (pitanjeDetekcija.Text == "" || detekcija1.Text == "" || detekcija2.Text == "" || detekcija3.Text == "" || slavna_osoba.Text == "")
            {
                MessageBox.Show("Ispunite sva polja kako biste dodali novo pitanje.");
            }
            else
            {

                detekcija[brojDetekcija, 0] = pitanjeDetekcija.Text;
                detekcija[brojDetekcija, 1] = detekcija1.Text;
                detekcija[brojDetekcija, 2] = detekcija2.Text;
                detekcija[brojDetekcija, 3] = detekcija3.Text;
                if (osobno.Checked)
                    detekcija[brojDetekcija, 4] = "osobno";
                else
                    detekcija[brojDetekcija, 4] = "struka";

                osobno.Checked = true;
                pitanjeDetekcija.Text = "";
                detekcija1.Text = "";
                detekcija2.Text = "";
                detekcija3.Text = "";

                brojDetekcija++;

                ime_pitanja.Text = "Pitanje " + (brojDetekcija + 1) + ":";

                if (brojDetekcija == 1)
                {
                    osoba = slavna_osoba.Text;
                    slavna_osoba.Enabled = false;
                }

                if (brojDetekcija == 9)
                {
                    sljedeceDetekcija.Text = "Spremi detekciju";
                }

                if (brojDetekcija == 10)
                {
                    using (System.IO.StreamWriter fileDetekcija = new System.IO.StreamWriter(@"DetekcijaBez.txt", true, System.Text.Encoding.Default))
                    {
                        fileDetekcija.WriteLine(osoba);
                        for (int i = 0; i < 10; ++i)
                        {
                            fileDetekcija.WriteLine(detekcija[i, 0]);
                            fileDetekcija.WriteLine(detekcija[i, 1]);
                            fileDetekcija.WriteLine(detekcija[i, 2]);
                            fileDetekcija.WriteLine(detekcija[i, 3]);
                            fileDetekcija.WriteLine(detekcija[i, 4]);
                        }
                    }

                    brojDetekcija = 0;
                    sljedeceDetekcija.Text = "Sljedeće pitanje";
                    MessageBox.Show("Uspješno ste kreirali novu igru detekcije!");
                }
            }
        }

        private void odustani_Click(object sender, EventArgs e)
        {
            brojDetekcija = 0;
            sljedeceDetekcija.Text = "Sljedeće pitanje";
            ime_pitanja.Text = "Pitanje 1:";
            slavna_osoba.Enabled = true;
            slavna_osoba.Text = "";
            osobno.Checked = true;
            pitanjeDetekcija.Text = "";
            detekcija1.Text = "";
            detekcija2.Text = "";
            detekcija3.Text = "";
        }

        private void spremiZavrsna_Click(object sender, EventArgs e)
        {
            if (pitanjeZavrsna.Text == "" || odgovorZavrsna.Text == "" || uputaZavrsna.Text == "")
            {
                MessageBox.Show("Ispunite sva polja kako biste dodali novo pitanje.");
            }
            else
            {
                using (System.IO.StreamWriter fileZavrsna = new System.IO.StreamWriter(@"Zavrsna.txt", true, System.Text.Encoding.Default))
                {
                    fileZavrsna.WriteLine(pitanjeZavrsna.Text);
                    fileZavrsna.WriteLine(uputaZavrsna.Text);
                    fileZavrsna.WriteLine(odgovorZavrsna.Text);

                    pitanjeZavrsna.Text = "";
                    uputaZavrsna.Text = "";
                    odgovorZavrsna.Text = "";
                }
                MessageBox.Show("Uspješno ste dodali novo pitanje!", "Upozorenje");
            }
        }

        private void SpremiLozinku_Click(object sender, EventArgs e)
        {
            if (NovaLozinka.Text == "" || PotvrdiLozinku.Text == "")
            {
                MessageBox.Show("Ispunite sva polja kako biste promijenili lozinku.");
            }
            else if(!NovaLozinka.Text.Equals(PotvrdiLozinku.Text))
            {
                MessageBox.Show("Potvrda lozinke ne odgovara novoj lozinci.", "Upozorenje");
            }
            else
            {
                using (System.IO.StreamWriter fileLozinka = new System.IO.StreamWriter(@"Lozinka.txt", false, System.Text.Encoding.Default))
                {
                    fileLozinka.WriteLine(NovaLozinka.Text);
                    
                    NovaLozinka.Text = "";
                    PotvrdiLozinku.Text = "";
                  
                }
                MessageBox.Show("Uspješno ste promijenili lozinku!", "Obavijest");
            }
        }

    }
}

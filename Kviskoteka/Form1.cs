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
    public partial class Form1 : Form
    {
        int broj_igre = 1;
        int ukupno_bodova;
        int abc_bodovi, asoc_bodovi, det_bodovi, zavrsna_bodovi;
        Label lpassword;
        TextBox tpassword;
        Form loginForm;
        Button kviskoGumb;
        Button izadi = new Button();
        int[] protivnik1_bodovi;
        int[] protivnik2_bodovi;
        bool igracNaRedu;

        int var8 = 0;
        Timer timer8  = new Timer();
        private Timer timer1 = new Timer();
        private Timer timer2 = new Timer();
        private int broj_pitanja = 0;
        int rb_igraca=0;
        int[] izbor_botova = new int[2];
       
        
        //povezivanje sa bazom
        static DataSet baza = new DataSet();
        
        //povezivanje sa bazom
        Klase.ClassLozinka klasaLozinka = new Klase.ClassLozinka(baza);
        Klase.ClassAsocijacija klasaAsocijacija;
        Klase.ClassDetekcija klasaDetekcija;
        Klase.ClassABC klasaABC;
        Klase.ClassZavrsna klasaZavrsna;
        Klase.ClassBodovi klasaBodovi = new Klase.ClassBodovi(baza);

        int protivnik1Kvisko;
        int protivnik2Kvisko;
        public int[] protivnik1 = new int[4] { 2, 2, 2, 2 };
        public int[] protivnik2 = new int[4] { 2, 2, 2, 2 };


        // Glavni izbornik
        public Form1()
        {
            InitializeComponent();
            this.Text = "Kviskoteka";
            ime_igre.Left = ime_igre.Parent.Width / 2 - ime_igre.Width / 2;
            ime_igre.Top = 60;
            Igraliste.Controls.Add(ime_igre);
            Igraliste.Parent = this;
            Detekcija.Parent = Igraliste;
            Zavrsna.Parent = Igraliste;
            zavrsniPanel.Parent = Igraliste;
            kviskoGumb = new Button();
            kviskoGumb.Name = "kviskoGumb";
            ispisIgraca_.Visible = false;
            pitanjaDetekcija.DrawMode = DrawMode.OwnerDrawFixed;

            kviskoGumb.MouseClick += new MouseEventHandler(iskoristiKviska);
            for (int i = 0; i < 9; i++)
            {
                Asocijacija.Controls.Add(new Panel());
            }
            var kontrola = Asocijacija.GetControlFromPosition(1, 1);
            kontrola.Dock = DockStyle.Fill;

            kontrola = Asocijacija.GetControlFromPosition(1, 0);
            kontrola.Dock = DockStyle.Fill;

            for ( int i=0; i <16; i++)
            {
                tablicaAsocijacija.Controls.Add(new Label());               
            }

            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    tablicaAsocijacija.GetControlFromPosition(i, j).Dock = DockStyle.Fill;
                    tablicaAsocijacija.GetControlFromPosition(i, j).BackColor = Color.CornflowerBlue;
                    tablicaAsocijacija.GetControlFromPosition(i, j).MouseClick += new MouseEventHandler(otvori_polje); 
                }
            }

            Button gumbPreskoci = new Button();
            gumbPreskoci.Text = "Preskoči asocijaciju";
            gumbPreskoci.Font = new Font("Microsoft Sans Serif", 10);
            gumbPreskoci.AutoSize = true;
            gumbPreskoci.Click += preskociAsocijaciju_Click;
            gumbPreskoci.BackColor = Color.PowderBlue;
            gumbPreskoci.FlatStyle = FlatStyle.Flat;
            gumbPreskoci.FlatAppearance.BorderColor = Color.RoyalBlue; 

            Label ispisIgraca = new Label();

            ispisIgraca.AutoSize = true;
            Asocijacija.Controls.Add(ispisIgraca, 1, 3);

            ispisIgraca.Visible = true;
            ispisIgraca.BackColor = Color.Transparent;
            ispisIgraca.Font = new Font("Microsoft Sans Serif", 11);
            TableLayoutPanel panel = new TableLayoutPanel();
            panel.ColumnCount = 3;
            panel.RowCount = 2;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));


            panel.Controls.Add(ispisIgraca, 1, 0);
            panel.Controls.Add(gumbPreskoci, 2, 1);
            panel.Dock = DockStyle.Top;
            gumbPreskoci.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            ispisIgraca.Anchor = AnchorStyles.None;
            Asocijacija.Controls.Add(panel, 1, 3);
            panel.BackColor = Color.SkyBlue;

            kviskoGumb.BackColor = Color.PowderBlue;
            kviskoGumb.FlatStyle = FlatStyle.Flat;
            kviskoGumb.FlatAppearance.BorderColor = Color.RoyalBlue;
            kviskoGumb.AutoSize = true;
            kviskoGumb.Font = new Font("Microsoft Sans Serif", 10);

            izadi.Text = "Izađi iz igre";
            izadi.Font = new Font("Microsoft Sans Serif", 10);
            izadi.AutoSize = true;
            izadi.Click += izadi_Click;
            izadi.BackColor = Color.PowderBlue;
            izadi.FlatStyle = FlatStyle.Flat;
            izadi.FlatAppearance.BorderColor = Color.RoyalBlue;
            izadi.Left = Igraliste.Width - 160;
            izadi.Top = 60;
            Igraliste.Controls.Add(izadi);

            ABCodg_A.MouseClick += new MouseEventHandler(radioButton_checkedChanged);
            ABCodg_B.MouseClick += new MouseEventHandler(radioButton_checkedChanged);
            ABCodg_C.MouseClick += new MouseEventHandler(radioButton_checkedChanged);
        }

        /****************************************************** IZLAZ ************************************************/

        private void izadi_Click(object sender, EventArgs e)
        {
            Igraliste.Visible = false;
            Glavni_izbornik.Visible = true;
        }

        private void Kraj_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /****************************************************** NNAJBOLJI REZULTATI *********************************/
        private void BestScore_Click(object sender, EventArgs e)
        {
            FormScore formScore = new FormScore();
            formScore.klasaBodovi = klasaBodovi;
            formScore.ShowDialog();
        }

        /****************************************************** POSTAVKE *********************************************/

        private void Postavke_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.protivnik1 = protivnik1;
            formSettings.protivnik2 = protivnik2;
            formSettings.ShowDialog();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ime_igre.Left = ime_igre.Parent.Width / 2 - ime_igre.Width / 2;
            ime_igre.Top = 60;

            Igraliste.Controls.Add(izadi);
            izadi.Left = Igraliste.Width - 160;
            izadi.Top = 60;
        }

        private void Zapocni_igru_Click(object sender, EventArgs e)
        {
            ukupno_bodova = 0;
            abc_bodovi = 0;
            asoc_bodovi = 0;
            det_bodovi = 0;
            zavrsna_bodovi = 0;
        
            broj_igre = 1;
            protivnik1_bodovi = new int[4] { 0, 0, 0, 0 };
            protivnik2_bodovi = new int[4] { 0, 0, 0, 0 };
            var8 = 0;
            broj_pitanja = 0;
            rb_igraca = 1;
            kviskoGumb.Name = "kviskoGumb";
            kviskoGumb.Visible = false;
        
            Glavni_izbornik.Visible = false;
            ime_igre.Text = "ABC pitalica";
            upute_za_igru.Text = "Prva igra je ABC pitalica. Odgovarat ćete na deset pitanja na koja su ponuđena tri odgovora, a samo je jedan odgovor točan. Vrijeme za odgovaranje je ograničeno. Točan odgovor donosi 10 bodova. Igra ABC pitalica nudi mogućnost osvajanja Kviska kojeg kasnije možete uložiti u neku igru i time udvostručiti dobivene bodove. \r\n Kviska možete osvojiti ukoliko skupite bar 70 bodova. \r\n Sretno!  ";
            ABC_igra.Visible = false;
            Detekcija.Visible = false;
            Asocijacija.Visible = false;
            Rezultati.Visible = false;
            Zavrsna.Visible = false;
            Igraliste.Visible = true;    
            Upute.Visible = true;
            ime_igre.Visible = true;
            izadi.Visible = true;
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            timer4.Stop();
            timer5.Stop();
            timer8.Stop();
            timer9.Stop();
            timer10.Stop();
            timer11.Stop();
            timer12.Stop();
            protivnik1Asoc.Stop();
            protivnik2Asoc.Stop();
        }

        /************************************** ADMINISTRATOR *******************/
        private void Administrator_Click(object sender, EventArgs e)
        {

            string password = klasaLozinka.vratiLozinku(baza);

            loginForm = new Form();
            loginForm.BackColor = System.Drawing.Color.SkyBlue;
            loginForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            loginForm.MaximizeBox = false;
            loginForm.MinimizeBox = false;

            //labela i texbox za  password
            lpassword = new Label();
            tpassword = new TextBox();
            tpassword.PasswordChar = '*';
            lpassword.Text = "Lozinka:";
            lpassword.Font = new Font("Microsoft Sans Serif", 9);

            lpassword.Location = new Point(loginForm.ClientSize.Width / 2 - 100, 100);
            tpassword.Location = new Point(loginForm.ClientSize.Width / 2, 100);
            Button ok = new Button() { Text = "U redu", Left = loginForm.ClientSize.Width / 2 - 100, Width = 95, Top = loginForm.ClientSize.Height / 2 + 50, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Odbaci", Left = loginForm.ClientSize.Width / 2 + 5, Width = 95, Top = loginForm.ClientSize.Height / 2 + 50, DialogResult = DialogResult.Cancel };
            ok.BackColor = Color.PowderBlue;
            ok.FlatStyle = FlatStyle.Flat;
            ok.FlatAppearance.BorderColor = Color.RoyalBlue;
            ok.Font = new Font("Microsoft Sans Serif", 9);

            cancel.BackColor = Color.PowderBlue;
            cancel.FlatStyle = FlatStyle.Flat;
            cancel.FlatAppearance.BorderColor = Color.RoyalBlue;
            cancel.Font = new Font("Microsoft Sans Serif", 9);

            loginForm.Controls.Add(lpassword);
            loginForm.Controls.Add(tpassword);
            loginForm.Controls.Add(ok);
            loginForm.Controls.Add(cancel);
            loginForm.Text = "Login";
            tpassword.KeyDown += new KeyEventHandler(spremiPASS);
            DialogResult d = loginForm.ShowDialog();
            if (d == DialogResult.OK)
            {
                //provjeri podatke
                if (tpassword.Text == password)
                {
                    Administrator administrator = new Administrator();
                    administrator.ShowDialog();
                    loginForm.Close();
                }
                else
                {
                    MessageBox.Show("Unijeli ste pogrešnu lozinku.", "Upozorenje");
                }
            }
            else
            {
                loginForm.Close();
            }

        }

        private void spremiPASS(object sender, KeyEventArgs e)
        {
            string password = klasaLozinka.vratiLozinku(baza);
            if (e.KeyData == Keys.Enter)
            {
                //provjeri podatke
                if (tpassword.Text == password)
                {
                    loginForm.Close();
                    Administrator administrator = new Administrator();
                    administrator.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Unijeli ste pogrešnu lozinku.", "Upozorenje");
                }
            }

        }

        /************************************** UPUTE *****************************/

        private void dalje_Click(object sender, EventArgs e)
        {

            switch (broj_igre)
            {
                case 2:
                    ime_igre.Text = "Asocijacije";
                    upute_za_igru.Text = "Pred vama je igra asocijacija. Iscratno je polje od četiri stupca s četiri elementa. Svaki stupac sadrži asocijacije na manje riješenje, a dobivena četiri manja riješenja su asocijacije za konačno riješenje. Za vrijeme Vašeg poteza dozvoljeno je otkrivanje jedne ćelije te je obavezno pogađanje bilo kojeg riješenja. Ukoliko ste pogodili možete nastaviti pogađati, no ukoliko niste, vaši protivnici preuzimaju red. Manja riješenja nose 15 bodova, a konačno riješenje nosi 40. Ukoliko ne znate riješenje asocijacije možete odustati od igre pritiskom na gumb. \r\n Sretno!"; 
                    Rezultati.Visible = false;
                    Igraliste.Visible = true;
                    Upute.Visible = true;
                    break;
                case 3:
                    ime_igre.Text = "Detekcija";
                    ime_igre.Left = Igraliste.Width / 2 - ime_igre.Width/2;

                    upute_za_igru.Text = "Slijedi igra detekcije. Pred vama će se nalaziti tri tajanstvene osobe koje se predstavljaju kao ista poznata ličnost. Postavljajući pitanja sa liste osobama A, B i C morate saznati koja osoba uistinu predstavlja poznatu ličnost. Ograničeno je vrijeme postavljanja pitanja pa to vrijeme iskristite pametno. Igrači 1 i 2 igaju prvi te ste potom Vi. Ukoliko ne želite gledati kako protivnici postavljaju pitanja možete odmah doći na red pritiskom na gumb. Nakon što svim igračima istekne vrijeme svi glasaju za osobu za koju smatraju da je prava. \r\n Sretno!";
                    Rezultati.Visible = false;
                    Igraliste.Visible = true;
                    Upute.Visible = true;
                    break;
                case 4:
                    ime_igre.Text = "Završna igra";
                    ime_igre.Left = Igraliste.Width / 2 - ime_igre.Width / 2; ;
                    upute_za_igru.Text = "U završnoj igri bitno je znanje, brzina i spretnost. Jedno za drugim otvarat će se deset pitanja od kojih svako nosi po 20 bodova. Kako biste pokušali osvojiti bodove morate se prvi javiti na taster te potom točno odgovoriti na pitanje u slobodnoj formi. Vrijeme za odgovaranje na pitanje je ograničeno, a kao pomoć napisana je napomena iznad pitanja o strukturi riješenja. \r\n Sretno!";
                    Rezultati.Visible = false;
                    Igraliste.Visible = true;
                    Upute.Visible = true;
                    break;
                case 5:
                    zavrsniPanel.Visible = true;
                    Rezultati.Visible = false;
                    ime_igre.Visible = false;
                    izadi.Visible = false;
                    kviskoGumb.Visible = false;
                    kviskoGumb.Enabled = true;

                    ukupno_bodova = abc_bodovi + asoc_bodovi + det_bodovi + zavrsna_bodovi;
                    int ukupno_protivnik1 = protivnik1_bodovi[0] + protivnik1_bodovi[1] + protivnik1_bodovi[2] + protivnik1_bodovi[3];
                    int ukupno_protivnik2 = protivnik2_bodovi[0] + protivnik2_bodovi[1] + protivnik2_bodovi[2] + protivnik2_bodovi[3];
                    string pocetak1 = "";

                    if (ukupno_bodova > ukupno_protivnik1 && ukupno_bodova > ukupno_protivnik2)
                        pocetak1 = "Pobijedili ste!";
                    else if (ukupno_protivnik1 > ukupno_bodova && ukupno_protivnik1 > ukupno_protivnik2)
                        pocetak1 = "Pobjednik je Protivnik 1!";
                    else if (ukupno_protivnik2 > ukupno_bodova && ukupno_protivnik2 > ukupno_protivnik1)
                        pocetak1 = "Pobjednik je Protivnik 2!";
                    else if (ukupno_bodova == ukupno_protivnik1 && ukupno_bodova > ukupno_protivnik2)
                        pocetak1 = "Pobjedu dijelite s Protivnikom 1!";
                    else if (ukupno_bodova == ukupno_protivnik2 && ukupno_bodova > ukupno_protivnik1)
                        pocetak1 = "Pobjedu dijelite s Protivnikom 2!";
                    else if (ukupno_protivnik2 == ukupno_protivnik1 && ukupno_bodova < ukupno_protivnik1)
                        pocetak1 = "Pobjedu dijele Protivnik1 i Protivnik 2!";
                    else pocetak1 = "Nema pobjednika.";

          
                    string ABC = abc_bodovi + " na ABC pitalici";
                    string asoc = asoc_bodovi + " na asocijacijama";
                    string det = det_bodovi + " na detekciji";
                    string zavr = zavrsna_bodovi + " na završnoj igri";
                    string ukupno = "Ukupno ste osvojili " + ukupno_bodova + " bodova:";

                    //za protivnika 1
                    string ABCp1 = protivnik1_bodovi[0] + " na ABC pitalici";
                    string asocp1 = protivnik1_bodovi[1] + " na asocijacijama";
                    string detp1 = protivnik1_bodovi[2] + " na detekciji";
                    string zavrp1 = protivnik1_bodovi[3] + " na završnoj igri";
                    string ukupnop1 = "Protivnik 1 je osvojio " + ukupno_protivnik1 + " bodova:";

                    //za protivnika 2
                    string ABCp2 = protivnik2_bodovi[0] + " na ABC pitalici"; ;
                    string asocp2 = protivnik2_bodovi[1] + " na asocijacijama";
                    string detp2 = protivnik2_bodovi[2] + " na detekciji";
                    string zavrp2 = protivnik2_bodovi[3] + " na završnoj igri";
                    string ukupnop2 = "Protivnik 2 je osvojio " + ukupno_protivnik2 + " bodova:";

                    ZavrsnaCestitka.Text =  pocetak1 + "\r\n" + "\r\n" + ukupno + "\r\n " + "\r\n " + ABC + "\r\n " + asoc + "\r\n " + det + "\r\n " + zavr + "\r\n" + "\r\n" + 
                        ukupnop1 + "\r\n " + "\r\n" + ABCp1 + "\r\n " + asocp1 + "\r\n " +  detp1 + "\r\n " + zavrp1 + "\r\n" + "\r\n" +
                        ukupnop2 + "\r\n " + "\r\n " + ABCp2 + "\r\n " + asocp2 + "\r\n " + detp2 + "\r\n " + zavrp2 + "\r\n" + "\r\n" +
                        "\r\n" +  "Upišite ime pod kojim želite pamtiti rezultat ove igre:";
                    break;
                   
            }
        }

        private void unos_HighScorea(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                TextBox imeBodovi = sender as TextBox;
                klasaBodovi.noviRezultat(imeBodovi.Text, abc_bodovi, asoc_bodovi, det_bodovi, zavrsna_bodovi, ukupno_bodova);
                Igraliste.Visible = false;
                zavrsniPanel.Visible = false;
                Glavni_izbornik.Visible = true;
            }
        }



        /***************************************** POČETAK IGRE ********************************/

        private void start_Click(object sender, EventArgs e)
        {

            igracNaRedu = true;
            Random rand = new Random();
            protivnik1Kvisko = kviskoBot(rand, protivnik1);
            protivnik2Kvisko = kviskoBot(rand, protivnik2);
            kviskoGumb.Visible = false;
            Upute.Visible = false;
            HighScore.Text = "";
            switch (broj_igre)
            {
                case 1:
                    // ABCgeneriraj()
                    klasaABC = new Klase.ClassABC(baza);
                    ukupno_bodova = 0;
                    ABC_igra.Visible = true;
                    broj_pitanja = 0;
                    novoPitanje();
                    abc_bodovi = 0;
                    break;
                case 2:
                    generirajAsocijaciju();
                    Asocijacija.Visible = true;
                    break;
                case 3:
                    rb_igraca = 0;
                    generirajDetekciju();              
                    a1.Visible = true;
                    b1.Visible = true;
                    c1.Visible = true;                    
                    Detekcija.Visible = true;
                    break;
                case 4:
                    Igraliste.Visible = true;
                    generirajZavrsnu();
                    Zavrsna.Visible = true;
                    break;
                
            }
        }
        /********************************************** KVISKO BOTOVI *************************************/


        private int kviskoBot(Random rand, int[] protivnik)
        {
            int max = protivnik.Max();

            List<int> maximumi = new List<int>();
            for (int i = 1; i < 4; i++)
            {
                if (protivnik[i] == max)
                    maximumi.Add(i + 1);
            }
            return maximumi[rand.Next(maximumi.Count)];
        }


        /********************************************** ABC PITALICA **************************************/




        private void novoPitanje()
        {
            Console.WriteLine(broj_pitanja);
            if (broj_pitanja < 10)
            {
                broj_pitanja++;

                List<string> infoABC = klasaABC.novoPitanje(broj_pitanja);
                timer1.Enabled = true;
                timer1.Interval = 1000;
                timer1.Start();
                progressBar1.Maximum = 10;
                timer1.Tick -= new EventHandler(timer1_Tick);
                timer1.Tick += new EventHandler(timer1_Tick);
                ABCodg_A.Checked = false;
                ABCodg_B.Checked = false;
                ABCodg_C.Checked = false;

                ABC_pitanje.Text = infoABC[0];
                ABCodg_A.Text = infoABC[1];
                ABCodg_B.Text = infoABC[2];
                ABCodg_C.Text = infoABC[3];

                ABCodg_A.Enabled = true;
                ABCodg_B.Enabled = true;
                ABCodg_C.Enabled = true;

            }
            else
            {
                ABC_igra.Visible = false;
                protivnik1_bodovi[0] = ABCprotivnik(protivnik1[0]);
                protivnik2_bodovi[0] = ABCprotivnik(protivnik2[0]);

                string kviskoCestitka = "";
                if (abc_bodovi >= 70)
                {
                    kviskoCestitka = "\r\nČestitamo, osvojili ste Kviska! Iskoristite ga mudro! \r\nNa igri u koju ga uložite bodovi će se udvostručiti.";
                    kviskoGumb.Visible = true;
                    kviskoGumb.Text = "Iskoristi Kviska!";
                    Upute.Controls.Add(kviskoGumb,0,2);
                    kviskoGumb.AutoSize = true; 
                    kviskoGumb.Anchor = AnchorStyles.None;
                }
                else
                {
                    kviskoCestitka = "\r\nNažalost niste osvojili Kviska.";
                }
               
                cestitka.Text = "Osvojili ste " + abc_bodovi + " bodova." + kviskoCestitka+"\nProtivnik 1 je osvojio "+protivnik1_bodovi[0]+" bodova."+ "\nProtivnik 2 je osvojio " + protivnik2_bodovi[0]+" bodova." ;
                if (protivnik1_bodovi[0] >= 70) { cestitka.Text += "\nProtivnik 1 osvojio je kviska."; }
                else { protivnik1Kvisko = -1; }
                if (protivnik2_bodovi[0] >= 70) { cestitka.Text += "\nProtivnik 2 osvojio je kviska."; }
                else { protivnik2Kvisko = -1; }
                broj_igre = 2;
                Rezultati.Visible = true;
            }
        }
        
        private int ABCprotivnik(int tezina)
        {
            Random rand = new Random();
            int rez = 0;
            switch (tezina){
                case 1:
                    rez = rand.Next(2, 5);
                    break;
                case 2:
                    rez = rand.Next(5, 9);
                    break;
                case 3:
                    rez = rand.Next(7, 10);
                    break;
            }
            return rez*10;

        }

        private void iskoristiKviska(object sender, MouseEventArgs e)
        {
            kviskoGumb.Name = broj_igre + "";
            kviskoGumb.Enabled = false;
        }

        void timer1_reset()
        {
            progressBar1.Value = 0;
        }

        

        void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 10)
            {
                progressBar1.Value++;
            }
            else
            {   //isteklo je vrijeme - prebacimo se na iduce pitanje
                timer1.Stop();
                timer1_reset();
                novoPitanje();
            }
        }

        void timer2_Tick(object sender, System.EventArgs e)
        {
            ABCodg_A.BackColor = Color.Transparent;
            ABCodg_B.BackColor = Color.Transparent;
            ABCodg_C.BackColor = Color.Transparent;
            timer2.Stop();
            timer1_reset();
            novoPitanje();
        }


        private void radioButton_checkedChanged(object sender, EventArgs e)
        {
            //provjeriti odgovor i pribrojiti bodove
            if (!(ABCodg_A.Checked == false && ABCodg_B.Checked == false && ABCodg_C.Checked == false))
            {
                timer1.Stop();
                ABCodg_A.Enabled = false;
                ABCodg_B.Enabled = false;
                ABCodg_C.Enabled = false;
                timer2.Interval = 500;
                timer2.Tick -= timer2_Tick;
                timer2.Tick += timer2_Tick;
                RadioButton r = (sender as RadioButton);               
                
                timer2.Start();

                if (klasaABC.provjeriOdgovor(r.Text, broj_pitanja))
                {
                    abc_bodovi += 10;
                    r.BackColor = Color.LightGreen;
                }
                else
                {
                    r.BackColor = Color.Tomato;
                }
            }
     }

        /****************************************************** ASOCIJACIJA ************************************/

        int ASOCpobjednik = 0;
        private Timer protivnik1Asoc = new Timer();
        private Timer protivnik2Asoc = new Timer();
        int waitTime = 1;
        private void generirajAsocijaciju()
        {
            klasaAsocijacija = new Klase.ClassAsocijacija(baza);

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    tablicaAsocijacija.GetControlFromPosition(i, j).BackColor = Color.CornflowerBlue; //boja

            postaviIgraca(0);

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    var l = tablicaAsocijacija.GetControlFromPosition(i, j) as Label;
                    l.Dock = DockStyle.Fill;
                    l.TextAlign = ContentAlignment.MiddleCenter;
                    int sizeFont = 16;
                    l.Font = new Font(l.Font.FontFamily, sizeFont);
                    l.Text = (char)('A' + i) + "" + (j + 1);
                    Asocijacija.GetControlFromPosition(1, 1).BackColor = Color.SkyBlue;
                }

        }

        private void preskociAsocijaciju_Click(object sender, EventArgs e)
        {
            int zatvoreni_stupci = 0;
            int konačno_rjesenje = 0;
            Random rand = new Random();
            // Podijeli bodove:
            for (int m = 0; m < 4; m++)
            {
                var l1 = rezultatiAsocijacije.GetControlFromPosition(m, 0) as TextBox;
                if (l1.BackColor != Color.Aquamarine)
                    zatvoreni_stupci++;
            }
            var l = rezultatiAsocijacije.GetControlFromPosition(0, 1) as TextBox;
            if (l.BackColor == Color.Aquamarine)
                konačno_rjesenje++;


            for (int i = 0; i < zatvoreni_stupci; i++)
            {
                switch (rand.Next(1, 4))
                {
                    case 1:
                        protivnik1_bodovi[1] += 15;
                        break;
                    case 2:
                        protivnik2_bodovi[1] += 15;
                        break;
                    default:
                        break;
                }
            }

            if (konačno_rjesenje == 0)
            {
                switch (rand.Next(1, 4))
                {
                    case 1:
                        protivnik1_bodovi[1] += 40;
                        break;
                    case 2:
                        protivnik2_bodovi[1] += 40;
                        break;
                    default:
                        break;
                }
            }

            zavrsiAsocijaciju();
        }

        private void otvori_polje(object sender, EventArgs e)
        {
            if (igracNaRedu)
            {
                // Klik - otvara polje
                var lab = (sender as Label);
                var position = tablicaAsocijacija.GetPositionFromControl(lab);
                lab.BackColor = Color.Aquamarine;
                int sizeFont = 16;
                lab.Font = new Font(lab.Font.FontFamily, sizeFont);
                Console.WriteLine("otvaram: ", position.Row, position.Column);
                lab.Text = klasaAsocijacija.otvori(position.Row, position.Column);

                // vise nije na redu i polja se zasive
                zasiviPolja();
                igracNaRedu = false;

                // pogađanje
            }
        }

        private void zasiviPolja()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    var l = tablicaAsocijacija.GetControlFromPosition(i, j) as Label;
                    if (l.BackColor != Color.Aquamarine)
                        l.BackColor = Color.PowderBlue;
                }
        }

        private void onemoguciPisanje()
        {
            for (int j = 0; j < 4; j++)
            {
                var l = rezultatiAsocijacije.GetControlFromPosition(j, 0) as TextBox;
                l.Enabled = false;
            }
            var l1 = rezultatiAsocijacije.GetControlFromPosition(0, 1) as TextBox;
            l1.Enabled = false;
        }

        private void omoguciPisanje()
        {
            for (int j = 0; j < 4; j++)
            {
                var l = rezultatiAsocijacije.GetControlFromPosition(j, 0) as TextBox;
                if (l.BackColor != Color.Aquamarine)
                    l.Enabled = true;
            }
            var l1 = rezultatiAsocijacije.GetControlFromPosition(0, 1) as TextBox;
            if (l1.BackColor != Color.Aquamarine)
                l1.Enabled = true;
        }

        private void obojiPolja()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    var l = tablicaAsocijacija.GetControlFromPosition(i, j) as Label;
                    if (l.BackColor != Color.Aquamarine)
                        l.BackColor = Color.CornflowerBlue; //boja
                }
        }

        // Odbrojavanje za otvaranje 
        private void protivnik1Asoc_Tick(object sender, EventArgs e)
        {
            if (waitTime > 0)
            {
                waitTime = waitTime - 1;
            }

            else
            {
                protivnik1Asoc.Stop();

                if (ASOCpobjednik == 1)
                    zavrsiAsocijaciju();

                else
                { 
                    randOtvoriPolje();
                    if (pogodakRijeci(protivnik1[1], protivnik1_bodovi, 1))
                    {
                        waitTime = 2;
                        protivnik1Asoc.Tick -= new EventHandler(protivnik1Asoc_Tick);
                        protivnik1Asoc.Start();
                        protivnik1Asoc.Interval = 1000;
                        protivnik1Asoc.Tick += new EventHandler(protivnik1Asoc_Tick);
                    }
                    else
                    {
                        postaviIgraca(2);
                        waitTime = 1;
                        protivnik2Asoc.Tick -= new EventHandler(protivnik2Asoc_Tick);
                        protivnik2Asoc.Start();
                        protivnik2Asoc.Interval = 1000;
                        protivnik2Asoc.Tick += new EventHandler(protivnik2Asoc_Tick);
                    }
                }
            }
        }

        private void protivnik2Asoc_Tick(object sender, EventArgs e)
        {
            if (waitTime > 0)
            {
                waitTime = waitTime - 1;
            }
            else
            {
                protivnik2Asoc.Stop();

                if (ASOCpobjednik == 2)
                    zavrsiAsocijaciju();

                else
                { 
                    randOtvoriPolje();
                    if (pogodakRijeci(protivnik2[1], protivnik2_bodovi, 2))
                    {
                        waitTime = 2;
                        protivnik2Asoc.Tick -= new EventHandler(protivnik2Asoc_Tick);
                        protivnik2Asoc.Start();
                        protivnik2Asoc.Interval = 1000;
                        protivnik2Asoc.Tick += new EventHandler(protivnik2Asoc_Tick);
                    }

                    else
                    {
                        postaviIgraca(0);
                        waitTime = 1;
                        igracNaRedu = true;
                        omoguciPisanje();
                        obojiPolja();
                    }
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            // Pogađanje rijesenja (manjih i konacnih)
            TextBox tx = sender as TextBox;

            // ako je stisnut enter -- pitaj bazu dal je to dobra rijec
            if (e.KeyData == Keys.Enter)
            {
                var position = rezultatiAsocijacije.GetPositionFromControl(tx);
                if (position.Row == 0)
                {
                    var odgovor = tx.Text;
                    bool tocno = klasaAsocijacija.provjeriUnos(position.Column + 1, odgovor); //+1 jer kolone krecu od nule

                    // je li pogodak odgovor za stupac position.Column točan?
                    // bool = posalji(odgovor, position.Column)
                    if (tocno)
                    {
                        tx.Enabled = false;
                        tx.BackColor = Color.Aquamarine;
                        asoc_bodovi += 15;
                        obojiPolja();
                        igracNaRedu = true;
                    }
                    else
                    {
                        tx.Clear();
                        igracNaRedu = false;
                        zasiviPolja();
                        onemoguciPisanje();


                        // Neprijatelj
                        postaviIgraca(1);
                        waitTime = 1;
                        protivnik1Asoc.Tick -= new EventHandler(protivnik1Asoc_Tick);
                        protivnik1Asoc.Start();
                        protivnik1Asoc.Interval = 1000;
                        protivnik1Asoc.Tick += new EventHandler(protivnik1Asoc_Tick);

                        // simuliraj neprijatelja
                    }
                }
                else if (position.Row == 1)
                {
                    var odgovor = tx.Text;
                    bool tocno = klasaAsocijacija.provjeriUnos(0, odgovor);
                    // je li pogodak odgovor za stupac position.Column točan?
                    // bool = posalji(odgovor, position.Column)
                    if (tocno)
                    {
                        tx.Enabled = false;
                        tx.BackColor = Color.Aquamarine;
                        // dodati bodove
                        asoc_bodovi += 40;
                        // Igra je gotova!
                        zavrsiAsocijaciju();
                    }
                    else
                    {
                        tx.Clear();
                        igracNaRedu = false;
                        zasiviPolja();

                        // Neprijatelj
                        postaviIgraca(1);
                        waitTime = 1;
                        postaviIgraca(1);
                        protivnik1Asoc.Tick -= new EventHandler(protivnik1Asoc_Tick);
                        protivnik1Asoc.Start();
                        protivnik1Asoc.Interval = 1000;
                        protivnik1Asoc.Tick += new EventHandler(protivnik1Asoc_Tick);

                    }
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void postaviIgraca(int i)
        {

            TableLayoutPanel t = (TableLayoutPanel) Asocijacija.GetControlFromPosition(1, 3);
            Label ispisIgraca = (Label) t.GetControlFromPosition(1, 0);
            switch (i)
            {
                case 0:
                    ispisIgraca.Text = "Vi ste na redu.";
                    break;
                case 1:
                    ispisIgraca.Text = "Protivnik 1 je na redu.";
                    break;
                case 2:
                    ispisIgraca.Text = "Protivnik 2 je na redu.";
                    break;
            }
        }

        private void randOtvoriPolje()
        {
            Random rand = new Random();
            int i;
            var tupleList = new List<Tuple<int, int>>
            {
                Tuple.Create(0, 0), Tuple.Create(0, 1), Tuple.Create(0, 2), Tuple.Create(0, 3),
                Tuple.Create(1, 0), Tuple.Create(1, 1), Tuple.Create(1, 2), Tuple.Create(1, 3),
                Tuple.Create(2, 0), Tuple.Create(2, 1), Tuple.Create(2, 2), Tuple.Create(2, 3),
                Tuple.Create(3, 0), Tuple.Create(3, 1), Tuple.Create(3, 2), Tuple.Create(3, 3)
            };

            List<int> lista = new List<int>();
            List<int> otvoreni_stupci = new List<int>();
            Label l;

            for (int k = 0; k < 4; k++)
            {
                var rjesenje = rezultatiAsocijacije.GetControlFromPosition(k, 0) as TextBox;
                if (rjesenje.BackColor == Color.Aquamarine)
                {
                    // sva polja u stupcu koji je pogođen
                    Tuple<int, int> izbaciti = new Tuple<int, int>(k, 0);
                    tupleList.Remove(izbaciti);
                    izbaciti = new Tuple<int, int>(k, 1);
                    tupleList.Remove(izbaciti);
                    izbaciti = new Tuple<int, int>(k, 2);
                    tupleList.Remove(izbaciti);
                    izbaciti = new Tuple<int, int>(k, 3);
                    tupleList.Remove(izbaciti);
                }
                else
                {
                    for (int m = 0; m < 4; m++)
                    {
                        // pogođene celije
                        l = tablicaAsocijacija.GetControlFromPosition(k, m) as Label;
                        if (l.BackColor == Color.Aquamarine)
                        {
                            Tuple<int, int> izbaciti = new Tuple<int, int>(k, m);
                            tupleList.Remove(izbaciti);
                        }
                    }
                }
            }
            if (tupleList.Count != 0)
            {
                i = rand.Next(tupleList.Count);
                l = tablicaAsocijacija.GetControlFromPosition(tupleList[i].Item1, tupleList[i].Item2) as Label;
                l.Text = klasaAsocijacija.otvori(tupleList[i].Item2, tupleList[i].Item1);
                l.BackColor = Color.Aquamarine;
            }
        }


        private int[] otvorenaPolja()
        {
            int[] otvorena_polja = new int[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var l = tablicaAsocijacija.GetControlFromPosition(i, j) as Label;
                    if (l.BackColor == Color.Aquamarine)
                        otvorena_polja[i]++;
                }
                var rjesenje = rezultatiAsocijacije.GetControlFromPosition(i, 0) as TextBox;
                if (rjesenje.BackColor == Color.Aquamarine)
                {
                    otvorena_polja[i] = -1;
                }
            }
            return otvorena_polja;
        }


        private bool pogodakRijeci(int razina, int[] bodovi, int protivnik)
        {
            Random rand = new Random();
            int vj = 0;
            // saznati koliko je rijeci u nekom stupcu otvoreno,a da nije pogodjen
            int[] otvorena_polja = otvorenaPolja();
            int max_stupac = 0, num_polja = 0;
            int broj_otvorenih = 0;
            if (otvorena_polja[0] == -1)
                broj_otvorenih++;
            for (int i = 1; i < 4; i++)
            {
                if (otvorena_polja[i] == -1)
                    broj_otvorenih++;
                if (otvorena_polja[i] > otvorena_polja[i - 1])
                {
                    num_polja = otvorena_polja[i];
                    max_stupac = i;
                }
            }
            vj = rand.Next(0, 100);
            String rj;
            switch (razina)
            {
                case 1:
                    if (broj_otvorenih >= 3 && vj < 40)
                    {
                        // Konačno riješenje
                        rj = klasaAsocijacija.vratiRiješenje(0);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(0, 1) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;
                        bodovi[1] += 40;

                        ASOCpobjednik = protivnik;
                        return true;
                    }
                    else if (num_polja == 3 && vj < 50)
                    {   // POGAĐA RIJEČ 
                        rj = klasaAsocijacija.vratiRiješenje(max_stupac + 1);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(max_stupac, 0) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 15;
                        return true;
                    }
                    else if (num_polja == 4 && vj < 70)
                    {   // POGAĐA RIJEČ
                        rj = klasaAsocijacija.vratiRiješenje(max_stupac + 1);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(max_stupac, 0) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 15;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if (broj_otvorenih >= 3 && vj < 50)
                    {
                        rj = klasaAsocijacija.vratiRiješenje(0);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(0, 1) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 40;
                        ASOCpobjednik = protivnik;
                        return true;
                    }
                    else if (num_polja == 2 && vj < 50)
                    {   // POGAĐA RIJEČ
                        rj = klasaAsocijacija.vratiRiješenje(max_stupac + 1);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(max_stupac, 0) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 15;
                        return true;
                    }
                    else if (num_polja == 3 && vj < 60)
                    {   // POGAĐA RIJEČ
                        rj = klasaAsocijacija.vratiRiješenje(max_stupac + 1);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(max_stupac, 0) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 15;
                        return true;
                    }
                    else if (num_polja == 4 && vj < 80)
                    {
                        rj = klasaAsocijacija.vratiRiješenje(max_stupac + 1);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(max_stupac, 0) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 15;
                        return true;
                    }
                    else
                        return false;
                case 3:
                    if (broj_otvorenih >= 3 && vj < 70)
                    {
                        rj = klasaAsocijacija.vratiRiješenje(0);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(0, 1) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 40;
                        ASOCpobjednik = protivnik;
                        return true;
                    }
                    else if (num_polja == 1 && vj < 30)
                    {   // POGAĐA RIJEČ
                        rj = klasaAsocijacija.vratiRiješenje(max_stupac + 1);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(max_stupac, 0) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 15;
                        return true;
                    }
                    if (num_polja == 2 && vj < 50)
                    {   // POGAĐA RIJEČ
                        rj = klasaAsocijacija.vratiRiješenje(max_stupac + 1);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(max_stupac, 0) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 15;
                        return true;
                    }
                    else if (num_polja == 3 && vj < 70)
                    {   // POGAĐA RIJEČ
                        rj = klasaAsocijacija.vratiRiješenje(max_stupac + 1);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(max_stupac, 0) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 15;
                        return true;
                    }
                    else if (num_polja == 4 && vj < 90)
                    {
                        rj = klasaAsocijacija.vratiRiješenje(max_stupac + 1);
                        var rjesenje = rezultatiAsocijacije.GetControlFromPosition(max_stupac, 0) as TextBox;
                        rjesenje.Text = rj;
                        rjesenje.Enabled = false;
                        rjesenje.BackColor = Color.Aquamarine;

                        bodovi[1] += 15;
                        return true;
                    }
                    else
                        return false;
            }
            return false;
        }


        private void zavrsiAsocijaciju()
        {

            Asocijacija.Visible = false;
            rezultatiAsocijacije.Visible = false;
            protivnik1Asoc.Stop();
            protivnik2Asoc.Stop();

            // varijable 
            textBox1.Clear();
            textBox1.Enabled = true;
            textBox1.Visible = true;
            textBox1.BackColor = Color.White;
            textBox2.Clear();
            textBox2.Enabled = true;
            textBox2.Visible = true;
            textBox2.BackColor = Color.White;
            textBox3.Clear();
            textBox3.Enabled = true;
            textBox3.Visible = true;
            textBox3.BackColor = Color.White;
            textBox4.Clear();
            textBox4.Enabled = true;
            textBox4.Visible = true;
            textBox4.BackColor = Color.White;
            textBox5.Clear();
            textBox5.Enabled = true;
            textBox5.Visible = true;
            textBox5.BackColor = Color.White;

            rezultatiAsocijacije.Visible = true;

            ime_igre.Left = Igraliste.Width / 2 - ime_igre.Width / 2;

            string kviskoCest = "";
            if (Convert.ToInt32(kviskoGumb.Name[0] - '0') == broj_igre)
            {
                asoc_bodovi = asoc_bodovi * 2;
                kviskoCest = "\r\nBodovi su udvostručeni zbog uloženog kviska.";
            }
            cestitka.Text = "Osvojili ste " + asoc_bodovi + " bodova." + kviskoCest;


            // jesu li neprijatelji iskoristili kviska ?
            if (protivnik1Kvisko == broj_igre)
                cestitka.Text += "\n Protivnik 1 je osvojio " + protivnik1_bodovi[1] * 2 + " bodova. \nBodovi su mu udvostručeni zbog uloženog kviska.";
            else
                cestitka.Text += "\n Protivnik 1 je osvojio " + protivnik1_bodovi[1] + " bodova.";
            if (protivnik2Kvisko == broj_igre)
                cestitka.Text += "\n Protivnik 2 je osvojio " + protivnik2_bodovi[1] * 2 + " bodova. \nBodovi su mu udvostručeni zbog uloženog kviska.";
            else
                cestitka.Text += "\n Protivnik 2 je osvojio " + protivnik2_bodovi[1] + " bodova.";

            Rezultati.Visible = true;

            broj_igre = 3;

        }


        /*************************************** DETEKCIJA *********************************************/
        Button preskoci = new Button();
        private Timer timer3 = new Timer();
        Label igrac = new Label();

        private List<int> pitanjaOsobeA = new List<int>();
        private List<int> pitanjaOsobeB = new List<int>();
        private List<int> pitanjaOsobeC = new List<int>();
        private List<string> odgovoriOsobeA = new List<string>();
        private List<string> odgovoriOsobeB = new List<string>();
        private List<string> odgovoriOsobeC = new List<string>();


        void timer3_reset()
        {
            progressBar2.Value = 0;
        }

        void timer3_Tick(object sender, EventArgs e)
        {
            if (progressBar2.Value != 40)
            {
                progressBar2.Value++;
            }
            else
            {   //isteklo je vrijeme - pogadjanje tocne osobe
                timer3.Stop();
                timer3_reset();
                timer3.Tick -= new EventHandler(timer3_Tick);

                pitanjaDetekcija.Enabled = false;
                a.Enabled = false;
                b.Enabled = false;
                c.Enabled = false;
               
                progressBar2.Visible = false;
                if (rb_igraca == 2)
                    preskoci.Enabled = false;
            }
        }

    
        void timer8_Tick(object sender, EventArgs e)
        {   
            Random rand = new Random();
            if (var8 != 40)
            {
                var8+=4;
                
                int pitanje = rand.Next(1, 10);
                int osoba = rand.Next(1, 4);
                
                pitanjaDetekcija.SelectedIndex = pitanje;
                odgovori_na_pitanje((char)('a' - 1 + osoba));
                label2.Text = "Osoba "+ (char)('A' - 1 + osoba) + ":";
               
            }
            else
            {  //zaustavi timer i ako je drugi bot na redu simuliraj igru ponovno
                //ako je drugi bot vec odigrao pokreni igru za stvarnog igraca
                timer8.Stop();
                timer8_reset();
                timer8.Tick -= new EventHandler(timer8_Tick);
                timer3.Stop();
                timer3_reset();
                timer3.Tick -= new EventHandler(timer3_Tick);

                if (rb_igraca == 1)
                {
                    igrac.Text = "Sada Protivnik 2 postavlja pitanja.";
                    rb_igraca++;
                    simulirajDetekciju(2);

                }
                else
                {
                    //krece igra za stvarnog igraca  
                     igrac.Text = "Sada Vi postavljate pitanja.";
                    
                     timer3.Enabled = true;
                     timer3.Interval = 1000;
                     timer3.Start();
                     progressBar2.Maximum = 40;
                     timer3.Tick += new EventHandler(timer3_Tick);
                     progressBar2.Visible = true;
                     pitanjaDetekcija.Enabled = true;
                     a1.Enabled = true;
                     b1.Enabled = true;
                     c1.Enabled = true;
                     a.Enabled = true;
                     b.Enabled = true;
                     c.Enabled = true;

                }
            }
        }

        private void timer8_reset()
        {
            var8 = 0;
        }

    

        private void generirajDetekciju()
        {
            klasaDetekcija = new Klase.ClassDetekcija(baza);
            cestitka.Text = "";
            label2.Text = "Osoba";
            label3.Text = "Odgovor na pitanje";
            pitanjaOsobeA.Clear();
            pitanjaOsobeB.Clear();
            pitanjaOsobeC.Clear();
            odgovoriOsobeA.Clear();
            odgovoriOsobeB.Clear();
            odgovoriOsobeC.Clear();
            pitanjaDetekcija.Items.Clear();

            preskoci.Text = "Preskoči protivnike";
            preskoci.Font = new Font("Microsoft Sans Serif", 10);
            preskoci.AutoSize = true;
            preskoci.Enabled = true;
            preskoci.Click += preskoci_Click; 
            preskoci.BackColor = Color.PowderBlue;
            preskoci.FlatStyle = FlatStyle.Flat;
            preskoci.FlatAppearance.BorderColor = Color.RoyalBlue;
            preskoci.Anchor = AnchorStyles.Left;
            Detekcija.Controls.Add(preskoci, 1, 6);
            igrac.Font = new Font("Microsoft Sans Serif", 10);
            igrac.AutoSize = true;
            Detekcija.Controls.Add(igrac, 1, 4);
            igrac.Anchor =  AnchorStyles.None;

            List<string> infoDetekcija = klasaDetekcija.vratiPitanja();
            string ime = infoDetekcija[0];
            List<string> pitanja = new List<string>();// ((od 1 na dalje)
            
            for (int i = 1; i < infoDetekcija.Count; i++){
                pitanja.Add(infoDetekcija[i]);  
            }
            
            for( int i = 0; i < pitanja.Count(); i++){
             pitanjaDetekcija.Items.Add(pitanja[i]);
            }

            pitanjaDetekcija.SelectedIndex = 0;
            labelPitanja.Text = "Pogodite iza koje se osobe nalazi " + ime + " postavljajući pitanja s liste.";

            //prvi bot

           
            rb_igraca++; //rb_igraca = 1 -> bot1
            igrac.Text = "Sada Protivnik 1 postavlja pitanja.";
            
            simulirajDetekciju(1);                 
        }

        private void preskoci_Click(object sender, EventArgs e)
        {
            preskoci.Enabled = false;
            timer8.Stop();
            timer8_reset();
            timer8.Tick -= new EventHandler(timer8_Tick);
            timer3.Stop();
            timer3_reset();
            timer3.Tick -= new EventHandler(timer3_Tick);
            rb_igraca = 0;
            igrac.Text = "Sada Vi postavljate pitanja.";

            timer3.Enabled = true;
            timer3.Interval = 1000;
            timer3.Start();
            progressBar2.Maximum = 40;
            timer3.Tick += new EventHandler(timer3_Tick);
            progressBar2.Visible = true;
            pitanjaDetekcija.Enabled = true;
            a1.Enabled = true;
            b1.Enabled = true;
            c1.Enabled = true;
            a.Enabled = true;
            b.Enabled = true;
            c.Enabled = true;
        }

        //dohvati odgovor i prikaze ga u labeli 3
        private void odgovori_na_pitanje(char odg)
        {

            switch (odg)
            {
                case 'a':
                    if (pitanjaOsobeA.Contains(pitanjaDetekcija.SelectedIndex))
                    {
                        label3.Text = odgovoriOsobeA[pitanjaOsobeA.IndexOf(pitanjaDetekcija.SelectedIndex)];
                    }
                    else
                    {
                        pitanjaOsobeA.Add(pitanjaDetekcija.SelectedIndex);
                        odgovoriOsobeA.Add(klasaDetekcija.vratiOdgovor(odg, pitanjaDetekcija.SelectedIndex));
                        label3.Text = odgovoriOsobeA[pitanjaOsobeA.IndexOf(pitanjaDetekcija.SelectedIndex)];
                    }
                    break;
                case 'b':
                    if (pitanjaOsobeB.Contains(pitanjaDetekcija.SelectedIndex))
                    {
                        label3.Text = odgovoriOsobeB[pitanjaOsobeB.IndexOf(pitanjaDetekcija.SelectedIndex)];
                    }
                    else
                    {
                        pitanjaOsobeB.Add(pitanjaDetekcija.SelectedIndex);
                        odgovoriOsobeB.Add(klasaDetekcija.vratiOdgovor(odg, pitanjaDetekcija.SelectedIndex));
                        label3.Text = odgovoriOsobeB[pitanjaOsobeB.IndexOf(pitanjaDetekcija.SelectedIndex)];
                    }
                    break;
                case 'c':
                    if (pitanjaOsobeC.Contains(pitanjaDetekcija.SelectedIndex))
                    {
                        label3.Text = odgovoriOsobeC[pitanjaOsobeC.IndexOf(pitanjaDetekcija.SelectedIndex)];
                    }
                    else
                    {
                        pitanjaOsobeC.Add(pitanjaDetekcija.SelectedIndex);
                        odgovoriOsobeC.Add(klasaDetekcija.vratiOdgovor(odg, pitanjaDetekcija.SelectedIndex));
                        label3.Text = odgovoriOsobeC[pitanjaOsobeC.IndexOf(pitanjaDetekcija.SelectedIndex)];
                    }
                    break;
            }
        }

        //fja na odabir osobe koju cemo pitati odabrano pitanje klik -> dohvati odg iz baze i ispise u labele
        private void pitanje_osobi(object sender, EventArgs e)
        {
            Button dolazniGumb = sender as Button;

            if (pitanjaDetekcija.SelectedIndex == -1){
                MessageBox.Show("Molim vas, označite pitanje koje želite pitati.");
                return;
            }

            odgovori_na_pitanje(dolazniGumb.Name[0]);
            label2.Text = "Osoba " + dolazniGumb.Name.ToUpper() + ":";           
        }


        //ovo se poziva kad covjek klikne na gumb (kod odabira osobe)
        private void glasanje_za_osobu(object sender, EventArgs e)
        {
            Button glas = sender as Button;
            char pogodak = glas.Name[0];

            if (klasaDetekcija.rezultatPogadanja(pogodak))
            {
                det_bodovi += 50;
                string kviskoCest = "";
                if (Convert.ToInt32(kviskoGumb.Name[0] - '0') == broj_igre)
                {
                    det_bodovi = det_bodovi * 2;
                    kviskoCest = "\r\nBodovi su udvostručeni zbog uloženog kviska.";
                }
                
                cestitka.Text += "\nČestitamo, detektirali ste pravu osobu i osvojili 50 bodova!" + kviskoCest;
            }
            else
            {
                string a;
                int i = klasaDetekcija.vratiTocnuOsobu();
                if (i == 0)
                    a = "A";
                else if (i == 1)
                    a = "B";
                else
                    a = "C";
                cestitka.Text += "\nNažalost, niste uspijeli detektirati točnu osobu i niste osvojili bodove. \nOsoba " + klasaDetekcija.vratiPitanja()[0] + " se krila iza " + a + " osobe."; 
            }
          
            zavrsiDetekciju();
                         
        }

        private void odrediPogodak(int osoba) //odreduje koliko su protivnici skupili bodova u detekciji
        {
            int vjerojatnost_pogotka;
            int razina;
            Random rand = new Random();
            bool daj_tocan_odgovor = false;

            if (osoba == 1)
                razina = protivnik1[2];
            else
                razina = protivnik2[2];
            vjerojatnost_pogotka = rand.Next(1, 100);
            switch (razina)
            {
                case 1:
                    if (vjerojatnost_pogotka < 40)
                        daj_tocan_odgovor = true;
                    else daj_tocan_odgovor = false;
                    break;
                case 2:
                    if (vjerojatnost_pogotka < 60)
                        daj_tocan_odgovor = true;
                    else daj_tocan_odgovor = false;
                    break;
                case 3:
                    if (vjerojatnost_pogotka < 80)
                        daj_tocan_odgovor = true;
                    else daj_tocan_odgovor = false;
                    break;
            }

            if (daj_tocan_odgovor)
            {
                if (osoba == 1)
                {
                    protivnik1_bodovi[2] = 50;
                    if (protivnik1Kvisko == broj_igre)
                        protivnik1_bodovi[2] *= 2;
                }
                else
                {
                    protivnik2_bodovi[2] = 50;
                    if (protivnik2Kvisko == broj_igre)
                        protivnik2_bodovi[2] *= 2;
                }
            }
        }


        private void simulirajDetekciju(int osoba) //simulira postavljanje pitanja za bota
        {      
            pitanjaDetekcija.Enabled = false;
            a1.Enabled = false;
            b1.Enabled = false;
            c1.Enabled = false;
            a.Enabled = false;
            b.Enabled = false;
            c.Enabled = false;
            
            
            progressBar2.Visible = true;
            progressBar2.Maximum = 40;
            timer3.Interval = 1000;
            timer3.Enabled = true;
            timer3.Start();
            timer3.Tick += new EventHandler(timer3_Tick);
           
           
            timer8.Interval = 4000;
            timer8.Enabled = true;
            timer8.Start();
            timer8.Tick += new EventHandler(timer8_Tick);           
        }

        public void zavrsiDetekciju()
        {
            Detekcija.Visible = false;
            Rezultati.Visible = true;
            dalje.Visible = true;
            ime_igre.Left = ime_igre.Parent.Width / 2 - ime_igre.Width / 2;
            //ime_igre.Top = 20;
            //Varijable 
            pitanjaDetekcija.Enabled = true;
            a1.Visible = false;
            b1.Visible = false;
            c1.Visible = false;

            broj_igre = 4;
            a.Enabled = true;
            b.Enabled = true;
            c.Enabled = true;
            progressBar2.Visible = true;

            odrediPogodak(1);
            odrediPogodak(2);
            cestitka.Text += "\nProtivnik 1 je osvojio " + protivnik1_bodovi[2] + " bodova.";
            if (protivnik1Kvisko == broj_igre)
                cestitka.Text += "\n Bodovi su mu udvostručeni zbog uloženog Kviska.";
            cestitka.Text += "\nProtivnik 2 je osvojio " + protivnik2_bodovi[2] + " bodova.";
            if (protivnik2Kvisko == broj_igre)
                cestitka.Text += "\n Bodovi su mu udvostručeni zbog uloženog Kviska.";

        }


        private void pitanjaDetekcija_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            e.ItemHeight = listBox.Font.Height;
            e.ItemWidth = pitanjaDetekcija.Width;
        }

        private void pitanjaDetekcija_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                myBrush = Brushes.Black;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 204, 255)), e.Bounds);
            }

            else
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            }

            e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds);
            e.DrawFocusRectangle();
        }



        /********************************************** ZAVRSNA ***************************************/
        private Timer timer4 = new Timer();
        private Timer timer5 = new Timer();
        private Timer timer9 = new Timer();
        private Timer timer10 = new Timer();
        private Timer timer11 = new Timer();
        private Timer timer12 = new Timer();

        int[] vremena = { 0, 0, 0 }; //na nultom mjestu je vrijeme igraca, a na ostala dva su vremena protivnika
        Label ispisIgraca_ = new Label();

        string[,] pitanjaIupute;
        private int timeLeft = 3;

        public void generirajZavrsnu()
        {
            broj_pitanja = 0;
            klasaZavrsna = new Klase.ClassZavrsna(baza);
            pitanjaIupute = klasaZavrsna.vratiPitanjaUpute();

            postaviZavrsnoPitanje();
            Zavrsna.Controls.Add(odgovorZavrsna, 1, 7);
            Zavrsna.Controls.Add(progressBar3, 1, 8);
        }

        public void postaviZavrsnoPitanje()
        {
            ispisIgraca_.Visible = false;
            if (broj_pitanja < 10)
            {
                progressBar3.Visible = false;
                countDown.Visible = true;
                taster.Visible = false;
                odgovorZavrsna.Visible = false;
                timeLeft = 3;
                countDown.Text = "3 sekunde";
                timer4.Tick -= new EventHandler(timer4_Tick);
                timer4.Start();
                timer4.Interval = 1000;
                timer4.Tick += new EventHandler(timer4_Tick);
                zavrsnoPitanje.Text = (broj_pitanja + 1) + ". pitanje: " + pitanjaIupute[0, broj_pitanja];
                napomena.Text = "[" + pitanjaIupute[1, broj_pitanja] + "]";
                broj_pitanja++;
                Random rand = new Random();
                vremena[1] = zavrsnaProtivnik(rand, protivnik1[3]);
                vremena[2] = zavrsnaProtivnik(rand, protivnik2[3]);
                Console.WriteLine("1:  " + vremena[1]);
                Console.WriteLine("2:  " + vremena[2]);
                vremena[0] = 0;
            }
            else
            {
                zavrsiZavrsnu();
            }
        }

        void zavrsiZavrsnu()
        {
            Zavrsna.Visible = false;

            string kviskoCest = "";
            if (Convert.ToInt32(kviskoGumb.Name[0] - '0') == broj_igre)
            {
                zavrsna_bodovi = zavrsna_bodovi * 2;
                kviskoCest = "\r\nBodovi su udvostručeni zbog uloženog kviska.";
            }

            cestitka.Text = "Osvojili ste " + zavrsna_bodovi + " bodova." + kviskoCest;
            if (protivnik1Kvisko == broj_igre)
            {
                protivnik1_bodovi[3] *= 2;
                cestitka.Text += "\nProtivnik 1 je osvojio " + protivnik1_bodovi[3] + " bodova." + "\r\nBodovi su mu udvostručeni zbog uloženog kviska.";
            }
            else
            {
                cestitka.Text += "\nProtivnik 1 je osvojio " + protivnik1_bodovi[3] + " bodova.";
            }
            if (protivnik2Kvisko == broj_igre)
            {
                protivnik2_bodovi[3] *= 2;
                cestitka.Text += "\nProtivnik 2 je osvojio " + protivnik2_bodovi[3] + " bodova." + "\r\nBodovi su mu udvostručeni zbog uloženog kviska.";
            }
            else
            {
                cestitka.Text += "\nProtivnik 2 je osvojio " + protivnik2_bodovi[3] + " bodova.";
            }
            ime_igre.Left = ime_igre.Parent.Width / 2 - ime_igre.Width / 2;
            broj_igre = 5;
            Rezultati.Visible = true;
        }

        private void timer9_Tick(object sender, EventArgs e) //timer tastera
        {
            timer9.Stop();
            taster.Visible = false;
            countDown.Visible = false;

            if (vremena[0] <= vremena[1] && vremena[0] <= vremena[2])
            {
                odgovorZavrsna.BackColor = Color.White;
                prijediNaPitanje(0);
            }
            else if (vremena[1] <= vremena[2])
            {
                prijediNaPitanje(1);
            }
            else
            {
                prijediNaPitanje(2);
            }
        }

        private void taster_Click(object sender, EventArgs e)
        {
            timer11.Stop();
            taster.Enabled = false;
        }

        private void prijediNaPitanje(int igrac)
        {
            odgovorZavrsna.Clear();
            odgovorZavrsna.Visible = true;
            ispisiIgraca(igrac);
            progressBar3.Visible = true;
            odgovorZavrsna.BackColor = Color.White;
            timer5.Enabled = true;
            timer5.Interval = 1000;
            timer5.Start();
            progressBar3.Maximum = 5;
            timer5.Tick += new EventHandler(timer5_Tick);
            if (igrac != 0)
            {
                odgovorZavrsna.Enabled = false;
                simulirajIgraca(igrac);
            }
            else
            {
                odgovorZavrsna.Enabled = true;
                odgovorZavrsna.Focus();
            }
        }

        private void simulirajIgraca(int igrac)
        {
            progressBar3.Visible = false;
            int tezina = 2;
            if (igrac == 1)
                tezina = protivnik1[3];
            else
                tezina = protivnik2[3];

            int i = new Random().Next(0, 100);
            switch (tezina)
            {
                case 1:
                    if (i < 30)
                        odgovoriTocno(igrac); //prima igraca da zna kome treba povecati bodove
                    else
                        odgovoriKrivo(igrac);
                    break;
                case 2:
                    if (i < 50)
                        odgovoriTocno(igrac);
                    else
                        odgovoriKrivo(igrac);
                    break;
                case 3:
                    if (i < 70)
                        odgovoriTocno(igrac);
                    else
                        odgovoriKrivo(igrac);
                    break;
            }
        }

        private void odgovoriTocno(int igrac)
        {
            string odgovor = klasaZavrsna.vratiTocan(broj_pitanja - 1);
            odgovorZavrsna.Text = odgovor;
            odgovorZavrsna.BackColor = Color.LightGreen;

            timer5.Stop();
            timer5_reset();
            timer5.Tick -= new EventHandler(timer5_Tick);

            if (igrac == 1)
                protivnik1_bodovi[3] += 20;
            else
                protivnik2_bodovi[3] += 20;

            timer10.Tick -= new EventHandler(timer10_Tick);
            timer10.Start();
            timer10.Interval = 2500;
            timer10.Tick += new EventHandler(timer10_Tick);
        }

        private void odgovoriKrivo(int igrac)
        {
            odgovorZavrsna.Text = "xxxxx";
            odgovorZavrsna.BackColor = Color.LightSalmon;

            timer5.Stop();
            timer5_reset();
            timer5.Tick -= new EventHandler(timer5_Tick);

            timer10.Tick -= new EventHandler(timer10_Tick);
            timer10.Start();
            timer10.Interval = 2500;
            timer10.Tick += new EventHandler(timer10_Tick);

        }

        private void sljedeciIgrac(int trenutni)
        {
            int sljedeci = -1;

            switch (trenutni)
            {
                case 0:
                    if (vremena[0] == vremena.Min())
                    {
                        if (vremena[1] <= vremena[2])
                            sljedeci = 1;
                        else
                            sljedeci = 2;
                    }
                    else
                    {
                        if (vremena[1] > vremena[0])
                            sljedeci = 1;
                        else if (vremena[2] > vremena[0])
                            sljedeci = 2;
                    }
                    break;
                case 1:
                    if (vremena[1] == vremena.Min())
                    {
                        if (vremena[0] <= vremena[2])
                            sljedeci = 0;
                        else
                            sljedeci = 2;
                    }
                    else
                    {
                        if (vremena[0] > vremena[1])
                            sljedeci = 0;
                        else if (vremena[2] > vremena[1])
                            sljedeci = 2;
                    }
                    break;
                case 2:
                    if (vremena[2] == vremena.Min())
                    {
                        if (vremena[0] <= vremena[1])
                            sljedeci = 0;
                        else
                            sljedeci = 1;
                    }
                    else
                    {
                        if (vremena[1] > vremena[2])
                            sljedeci = 1;
                        else if (vremena[0] > vremena[2])
                            sljedeci = 0;
                    }
                    break;
            }

            if (sljedeci != -1 && vremena[sljedeci] < 2000)
                prijediNaPitanje(sljedeci);
            else
                postaviZavrsnoPitanje();
        }

        private void timer10_Tick(object sender, EventArgs e)
        {
            timer10.Stop();
            int igrac;
            if (odgovorZavrsna.Text != "xxxxx")
                postaviZavrsnoPitanje();
            else
            {
                if (ispisIgraca_.Text == "Igrač koji odgovara: protivnik 1")
                    igrac = 1;
                else
                    igrac = 2;
                sljedeciIgrac(igrac);
            }

        }
        private void ispisiIgraca(int i)
        {
            Zavrsna.Controls.Add(ispisIgraca_, 1, 6);
            ispisIgraca_.Anchor = AnchorStyles.None;
            ispisIgraca_.AutoSize = true;
            ispisIgraca_.Visible = true;

            switch (i)
            {
                case 0:
                    ispisIgraca_.Text = "Igrač koji odgovara: Vi";
                    break;
                case 1:
                    ispisIgraca_.Text = "Igrač koji odgovara: protivnik 1";
                    break;
                case 2:
                    ispisIgraca_.Text = "Igrač koji odgovara: protivnik 2";
                    break;
            }

            ispisIgraca_.Left = ispisIgraca_.Parent.Width / 2 - ispisIgraca_.Width / 2;
        }


        // Odbrojavanje za prijavu
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                countDown.Text = timeLeft + "";
            }
            else // odbrojavanje je zavrsilo
            {
                timer4.Stop();
                countDown.Text = "Prijava";
                taster.Visible = true;

                timer11.Tick -= new EventHandler(timer11_Tick);
                timer11.Enabled = true;
                timer11.Interval = 10;
                timer11.Start();
                timer11.Tick += new EventHandler(timer11_Tick);

                taster.Enabled = true;
                timer9.Tick -= new EventHandler(timer9_Tick);
                timer9.Enabled = true;
                timer9.Interval = 2000;
                timer9.Start();
                timer9.Tick += new EventHandler(timer9_Tick);
            }
        }

        private void timer11_Tick(object sender, EventArgs e)
        {
            vremena[0] += 10;
        }

        void timer5_reset()
        {
            progressBar3.Value = 0;
        }

        private int zavrsnaProtivnik(Random rand, int tezina) //vraca broj milisekunda potrebnih protivniku da se prijavi tasterom
        {

            int rez = 0;
            switch (tezina)
            {
                case 1:
                    rez = rand.Next(300, 2000);
                    break;
                case 2:
                    rez = rand.Next(150, 1500);
                    break;
                case 3:
                    rez = rand.Next(50, 800);
                    break;
            }
            return rez;

        }

        // Odbrojavanje za odgovor na pitanje
        void timer5_Tick(object sender, EventArgs e)
        {
            if (progressBar3.Value != 5)
            {
                progressBar3.Value++;
            }
            else
            {   //isteklo je vrijeme - pogadjanje tocne osobe
                timer5.Stop();
                timer5_reset();
                timer5.Tick -= new EventHandler(timer5_Tick);

                sljedeciIgrac(0);
                progressBar3.Visible = false;
            }
        }


        private void odgovorZavrsna_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox odgovorPitanja = sender as TextBox;

            if (odgovorPitanja.Visible == true && ispisIgraca_.Text == "Igrač koji odgovara: Vi")
            {

                // ako je stisnut enter -- pitaj bazu dal je to dobra rijec
                if (e.KeyData == Keys.Enter)
                {
                    timer5.Stop();
                    timer5_reset();
                    timer5.Tick -= new EventHandler(timer5_Tick);

                    if (klasaZavrsna.provjeriUnos(broj_pitanja - 1, odgovorPitanja.Text))
                    {
                        zavrsna_bodovi += 20;


                        odgovorZavrsna.BackColor = Color.LightGreen;

                        timer12.Tick -= new EventHandler(timer12_Tick);
                        timer12.Enabled = true;
                        timer12.Interval = 1500;
                        timer12.Start();
                        timer12.Tick += new EventHandler(timer12_Tick);
                        // kad 12 zavrsi ako je bilo tocno() : postaviZavrsnoPitanje();
                    }
                    else
                    {
                        // kad 12 zavrsi ( ako je bilo krivo) sljedeciIgrac(0);
                        odgovorZavrsna.BackColor = Color.LightSalmon;

                        timer12.Tick -= new EventHandler(timer12_Tick);
                        timer12.Enabled = true;
                        timer12.Interval = 1500;
                        timer12.Start();
                        timer12.Tick += new EventHandler(timer12_Tick);
                    }

                }
            }
        }

        void timer12_Tick(object sender, EventArgs e)
        {
            timer12.Stop();
            if (odgovorZavrsna.BackColor == Color.LightSalmon)
                sljedeciIgrac(0);
            else
                postaviZavrsnoPitanje();
        }


        /////// Završna igra - druga verzija ///////
        /*
        private Timer timer4 = new Timer();
        private Timer timer5 = new Timer();
        private Timer timer9 = new Timer();
        private Timer timer10 = new Timer();
        int vrijeme_tastera = 0; // sluzi za brojanje vremena u milisekundama od pocetka mogucnosti prijave tasterom u svrhu provjere prijave protivnika
        int vrijeme_protivnik1, vrijeme_protivnik2; //vrijeme nakon kojeg ce protivnici stisnuti taster, odabire se na random način prije svakog pitanja
        Label ispisIgraca_ = new Label();
        
        string[,] pitanjaIupute;
        private int timeLeft = 3;

        public void generirajZavrsnu()
        {
            broj_pitanja = 0;
            klasaZavrsna = new Klase.ClassZavrsna(baza);
            pitanjaIupute = klasaZavrsna.vratiPitanjaUpute();
            ime_igre.Top = 20;
            postaviZavrsnoPitanje();

        }

        public void postaviZavrsnoPitanje()
        {

            ispisIgraca_.Visible = false;
            if (broj_pitanja < 10)
            {
                progressBar3.Visible = false;
                taster.Visible = false;
                odgovorZavrsna.Visible = false;
                timeLeft = 3;
                countDown.Text = "3 sekunde";
                timer4.Tick -= new EventHandler(timer4_Tick);
                timer4.Start();
                timer4.Interval = 1000;
                timer4.Tick += new EventHandler(timer4_Tick);
                zavrsnoPitanje.Text = (broj_pitanja + 1) + ". pitanje : " + pitanjaIupute[0, broj_pitanja];
                napomena.Text = "[" + pitanjaIupute[1, broj_pitanja] + "]";
                broj_pitanja++;
                Random rand = new Random();
                vrijeme_protivnik1 = zavrsnaProtivnik(rand, protivnik1[3]);
                vrijeme_protivnik2 = zavrsnaProtivnik(rand, protivnik2[3]);
                Console.WriteLine("1:  " + vrijeme_protivnik1);
                Console.WriteLine("2:  " + vrijeme_protivnik2);
            }
            else
            {
                zavrsiZavrsnu();
            }
        }

        void zavrsiZavrsnu()
        {
            Zavrsna.Visible = false;

            string kviskoCest = "";
            if (Convert.ToInt32(kviskoGumb.Name[0] - '0') == broj_igre)
            {
                zavrsna_bodovi = zavrsna_bodovi * 2;
                kviskoCest = "\r\nBodovi su udvostručeni zbog uloženog kviska!";
            }

            cestitka.Text = "Uspijeli ste ostvariti  " + zavrsna_bodovi + " bodova u završnoj igri!" + kviskoCest;
            if (protivnik1Kvisko == broj_igre)
            {
                protivnik1_bodovi[3] *= 2;
                cestitka.Text += "\nProtivnik 1 osvojio je " + protivnik1_bodovi[3] + " bodova." + "\r\nBodovi su udvostručeni zbog uloženog kviska!";
            }
            else
            {
                cestitka.Text += "\nProtivnik 1 osvojio je " + protivnik1_bodovi[3] + " bodova." ;
            }
            if (protivnik2Kvisko == broj_igre)
            {
                protivnik2_bodovi[3] *= 2;
                cestitka.Text += "\nProtivnik 2 osvojio je " + protivnik2_bodovi[3] + " bodova." + "\r\nBodovi su udvostručeni zbog uloženog kviska!";
            }
            else
            {
                cestitka.Text += "\nProtivnik 2 osvojio je " + protivnik2_bodovi[3] + " bodova.";
            }
            Igraliste.Controls.Add(ime_igre);
            ime_igre.Left = ime_igre.Parent.Width / 2 - ime_igre.Width / 2;
            ime_igre.Top = 20;
            broj_igre = 5;
            Rezultati.Visible = true;
        }

        private void timer9_Tick(object sender, EventArgs e) //protivnik je prvi reagirao
        {
            if (vrijeme_tastera < vrijeme_protivnik1 && vrijeme_tastera < vrijeme_protivnik2)
                vrijeme_tastera += 10;
            else
            {
                if (vrijeme_tastera >= vrijeme_protivnik1)
                    prijediNaPitanje(1);
                else
                    prijediNaPitanje(2);
            }
        }



        private void taster_Click(object sender, EventArgs e)
        {
            odgovorZavrsna.BackColor = Color.White;
            prijediNaPitanje(0);
        }

        private void prijediNaPitanje(int igrac)
        {
            odgovorZavrsna.Clear();
            taster.Visible = false;
            odgovorZavrsna.Visible = true;
            ispisiIgraca(igrac);
            progressBar3.Visible = true;
            vrijeme_tastera = 0;
            timer9.Stop();
            timer5.Enabled = true;
            timer5.Interval = 1000;
            timer5.Start();
            progressBar3.Maximum = 5;
            timer5.Tick += new EventHandler(timer5_Tick);
            if (igrac != 0)
            {
                odgovorZavrsna.Enabled = false;
                simulirajIgraca(igrac);
            }
            else
            {
                odgovorZavrsna.Enabled = true;
                odgovorZavrsna.Focus();
            }
        }

        private void simulirajIgraca(int igrac)
        {
            int tezina = 2;
            if (igrac == 1)
                tezina = protivnik1[3];
            else
                tezina = protivnik2[3];

            int i = new Random().Next(0, 100);
            switch (tezina)
            {
                case 1:
                    if (i < 30)
                        odgovoriTocno(igrac); //prima igraca da zna kome treba povecati bodove
                    else
                        odgovoriKrivo();
                    break;
                case 2:
                    if (i < 50)
                        odgovoriTocno(igrac);
                    else
                        odgovoriKrivo();
                    break;
                case 3:
                    if (i < 70)
                        odgovoriTocno(igrac);
                    else
                        odgovoriKrivo();
                    break;
            }
        }

        private void odgovoriTocno(int igrac)
        {
            string odgovor = klasaZavrsna.vratiTocan(broj_pitanja - 1);
            odgovorZavrsna.Text = odgovor;
            odgovorZavrsna.BackColor = Color.LightGreen;

            timer5.Stop();
            timer5_reset();
            timer5.Tick -= new EventHandler(timer5_Tick);

            if (igrac == 1)
                protivnik1_bodovi[3] += 20;
            else
                protivnik2_bodovi[3] += 20;

            timer10.Tick -= new EventHandler(timer10_Tick);
            timer10.Start();
            timer10.Interval = 3000;
            timer10.Tick += new EventHandler(timer10_Tick);
        }

        private void odgovoriKrivo()
        {
            odgovorZavrsna.Text = "xxxxx";
            odgovorZavrsna.BackColor = Color.LightSalmon;

            timer5.Stop();
            timer5_reset();
            timer5.Tick -= new EventHandler(timer5_Tick);

            timer10.Tick -= new EventHandler(timer10_Tick);
            timer10.Start();
            timer10.Interval = 3000;
            timer10.Tick += new EventHandler(timer10_Tick);
        }

        private void timer10_Tick(object sender, EventArgs e)
        {
            timer10.Stop();
            postaviZavrsnoPitanje();
        }
        private void ispisiIgraca(int i)
        {
            ispisIgraca_.Parent = Igraliste;
            ispisIgraca_.BringToFront();
            ispisIgraca_.AutoSize = true;
            ispisIgraca_.Top = progressBar3.Location.Y + progressBar3.Height + 50;
            ispisIgraca_.Visible = true;
            ispisIgraca_.BackColor = Color.Transparent;

            switch (i)
            {
                case 0:
                    ispisIgraca_.Text = "Igrač koji odgovara: Vi";
                    break;
                case 1:
                    ispisIgraca_.Text = "Igrač koji odgovara: protivnik 1";
                    break;
                case 2:
                    ispisIgraca_.Text = "Igrač koji odgovara: protivnik 2";
                    break;
            }

            ispisIgraca_.Left = ispisIgraca_.Parent.Width / 2 - ispisIgraca_.Width / 2;
        }


        // Odbrojavanje za prijavu
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                countDown.Text = timeLeft + "";
            }
            else // odbrojavanje je zavrsilo
            {
                timer4.Stop();
                countDown.Text = "Prijava";
                taster.Visible = true;
                taster.Enabled = true;
                timer9.Tick -= new EventHandler(timer9_Tick);
                timer9.Enabled = true;
                timer9.Interval = 10;
                timer9.Start();
                timer9.Tick += new EventHandler(timer9_Tick);
            }
        }

        void timer5_reset()
        {
            progressBar3.Value = 0;
        }

        private int zavrsnaProtivnik(Random rand, int tezina) //vraca broj milisekunda potrebnih protivniku da se prijavi tasterom
        {

            int rez = 0;
            switch (tezina)
            {
                case 1:
                    rez = rand.Next(300, 2000);
                    break;
                case 2:
                    rez = rand.Next(150, 1500);
                    break;
                case 3:
                    rez = rand.Next(50, 800);
                    break;
            }
            return rez;

        }

        // Odbrojavanje za odgovor na pitanje
        void timer5_Tick(object sender, EventArgs e)
        {
            if (progressBar3.Value != 5)
            {
                progressBar3.Value++;
            }
            else
            {   //isteklo je vrijeme - pogadjanje tocne osobe
                timer5.Stop();
                timer5_reset();
                timer5.Tick -= new EventHandler(timer5_Tick);

                postaviZavrsnoPitanje();
                progressBar3.Visible = false;
            }
        }


        private void odgovorZavrsna_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox odgovorPitanja = sender as TextBox;

            if (odgovorPitanja.Visible == true && ispisIgraca_.Text == "Igrač koji odgovara: Vi")
            {

                // ako je stisnut enter -- pitaj bazu dal je to dobra rijec
                if (e.KeyData == Keys.Enter)
                {
                    timer5.Stop();
                    timer5_reset();
                    timer5.Tick -= new EventHandler(timer5_Tick);

                    if (klasaZavrsna.provjeriUnos(broj_pitanja - 1, odgovorPitanja.Text))
                    {
                        zavrsna_bodovi += 20;
                    }

                    // odgovorPitanja.Clear();
                    postaviZavrsnoPitanje();
                }
            }
        }
         */
    }


}
 
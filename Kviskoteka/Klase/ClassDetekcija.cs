using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviskoteka.Klase
{
    class ClassDetekcija
    {
        private string[,] pitanja = new string[10, 2]; //pitanje i vrsta
        private string[,] odgovori = new string[10, 3];
        private string osoba;
        private int prava; // 0/1/2

        public ClassDetekcija(DataSet baza_)
        {
            baza_.Detekcija.ucitaj();

            int redniBroj = new Random().Next(baza_.Detekcija.Count() / 10);

            //preskacemo prvih redniBroj igara
            //redni broj redaka krece od nule
            osoba = baza_.Detekcija.FindByid(redniBroj * 10).osoba;

            for (var i = 0; i < 10; i++)
            {
                odgovori[i, 0] = baza_.Detekcija.FindByid(redniBroj * 10 + i).odgovor1;
                odgovori[i, 1] = baza_.Detekcija.FindByid(redniBroj * 10 + i).odgovor2;
                odgovori[i, 2] = baza_.Detekcija.FindByid(redniBroj * 10 + i).odgovor3;
                pitanja[i, 0] = baza_.Detekcija.FindByid(redniBroj * 10 + i).pitanje;
                pitanja[i, 1] = baza_.Detekcija.FindByid(redniBroj * 10 + i).vrsta;
            }

            prava = new Random().Next(3); // 0/1/2

        }

        public List<string> vratiPitanja() //nulti string je osoba, ostali su pitanja
        {
            List<string> listaPitanja = new List<string>();
            listaPitanja.Add(osoba);
            for (int i = 0; i < 10; ++i)
                listaPitanja.Add(pitanja[i, 0]);
            return listaPitanja;
        }

        public string vratiOdgovor(char id, int pitanje) // osoba a/b/c, pitanje - redni broj od nula do 9
        {
            string tocan, krivi1, krivi2;
            tocan = odgovori[pitanje, 0];
            krivi1 = odgovori[pitanje, 1];
            krivi2 = odgovori[pitanje, 2];
            bool vrsta_;
            int i, j;
            string prava_osoba, kriva_osoba;

            if (pitanja[pitanje, 1] == "osobno")
                vrsta_ = true;
            else
                vrsta_ = false;

            if (vrsta_)
            {
                prava_osoba = tocan;
                i = new Random().Next(1, 100);
                if (i > 50) // vece od 50 -> krivi odgovor
                {
                    j = new Random().Next(2);
                    if (j == 0)
                        kriva_osoba = krivi1;
                    else
                        kriva_osoba = krivi2;
                }
                else
                    kriva_osoba = tocan;
            }
            else
            {
                i = new Random().Next(1, 100);
                if (i < 80) //manje od 80 -> tocan odgovor
                    prava_osoba = tocan;
                else
                {
                    j = new Random().Next(2);
                    if (j == 0)
                        prava_osoba = krivi1;
                    else
                        prava_osoba = krivi2;
                }

                i = new Random().Next(1, 100);
                if (i < 40) //manje od 40 -> tocan odgovor
                    kriva_osoba = tocan;
                else
                {
                    j = new Random().Next(2);
                    if (j == 0)
                        kriva_osoba = krivi1;
                    else
                        kriva_osoba = krivi2;
                }
            }
            switch (id)
            {
                case 'a':
                    if (prava == 0) return prava_osoba;
                    break;
                case 'b':
                    if (prava == 1) return prava_osoba;
                    break;
                case 'c':
                    if (prava == 2) return prava_osoba;
                    break;
                default:
                    Console.WriteLine("rezultatPogadanja --> Ta osoba ne postoji");
                    return null;
            }
            return kriva_osoba;
        }

        public bool rezultatPogadanja(char id)
        {
            switch (id)
            {
                case 'a':
                    if (prava == 0) return true;
                    break;
                case 'b':
                    if (prava == 1) return true;
                    break;
                case 'c':
                    if (prava == 2) return true;
                    break;
                default:
                    Console.WriteLine("rezultatPogadanja --> Ta osoba ne postoji");
                    return false;
            }
            return false;
        }

        public int vratiTocnuOsobu()
        {
            return prava;
        }
    }
}

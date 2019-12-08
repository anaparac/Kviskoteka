using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviskoteka.Klase
{
    class ClassZavrsna
    {
        private int[] nizPitanja = new int[10];
        private string[,] pitanjaUpute = new string[2, 10]; //deset elemenata sa pitanje+uputa
        private string[] odgovori = new string[10];

        public ClassZavrsna(DataSet baza_)
        {
            baza_.Zavrsna.ucitaj();
            //--------------------------------
            //popuni rendom niz 
            //--------------------------------
            int kolicina = 0; //doci ce do vrijednosti 9
            int num;
            Random rand = new Random();
            bool postoji = false;
            while (kolicina < 10)
            {
                num = rand.Next(baza_.Zavrsna.Rows.Count);
                //postoji li on vec u nizu
                postoji = false;
                for (int i = 0; i < kolicina; i++)
                    if (nizPitanja[i] == num)
                    {
                        postoji = true;
                        break;
                    }
                //ako ne postoji
                if (!postoji)
                {
                    nizPitanja[kolicina] = num;
                    kolicina++;
                }
            }

            //--------------------------------
            //dohvati pitanja
            //--------------------------------
            for (int j = 0; j < kolicina; j++)
            {
                pitanjaUpute[0, j] = baza_.Zavrsna.FindByid(nizPitanja[j]).pitanje;
                pitanjaUpute[1, j] = baza_.Zavrsna.FindByid(nizPitanja[j]).uputa;
                odgovori[j] = baza_.Zavrsna.FindByid(nizPitanja[j]).odgovor;
            }
        }

        public string[,] vratiPitanjaUpute()
        {
            return pitanjaUpute;
        }

        public bool provjeriUnos(int redniBr, string unos) //redniBr ocekuje [0,9]
        {
            if (String.Equals(unos, odgovori[redniBr], StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        public string vratiTocan(int redniBr)
        {
            return odgovori[redniBr];
        }
    }
}

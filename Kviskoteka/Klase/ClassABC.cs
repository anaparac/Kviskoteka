using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviskoteka.Klase
{
    class ClassABC
    {

        int broj_pitanja;
        private string[] pitanja = new string[10];
        private string[,] odgovori = new string[10, 3];
        private string[] tocno = new string[10];
        private int[] id_pitanja = new int[10];

        public ClassABC(DataSet baza_)
        {
            //sva abc pitanja se spreme u bazu
            baza_.ABCpitalica.ucitaj();
            broj_pitanja = baza_.ABCpitalica.Count();

            //randomizirati 10 brojeva od 0 do broj pitanja 

            id_pitanja = randomiziraj(broj_pitanja, 10);

            //izvaditi 10 pitanja s odgovorima iz baze
            for (int j = 0; j < 10; j++)
            {
                pitanja[j] = baza_.ABCpitalica.FindByid(id_pitanja[j]).pitanje;
                tocno[j] = baza_.ABCpitalica.FindByid(id_pitanja[j]).opcija1;
                odgovori[j, 0] = baza_.ABCpitalica.FindByid(id_pitanja[j]).opcija1;
                odgovori[j, 1] = baza_.ABCpitalica.FindByid(id_pitanja[j]).opcija2;
                odgovori[j, 2] = baza_.ABCpitalica.FindByid(id_pitanja[j]).opcija3;

            }

        }
        //pomocna fja za randomiziranje razlicitih brojeva u zadanom razmjeru
        private int[] randomiziraj(int max_mogucih, int broj_potrebnih)
        {

            int[] nizBrojeva = new int[broj_potrebnih];

            int kolicina = 0;
            int num;
            Random rand = new Random();
            bool postoji = false;
            while (kolicina < broj_potrebnih)
            {
                num = rand.Next(max_mogucih);
                //postoji li on vec u nizu
                postoji = false;
                for (int i = 0; i < kolicina; i++)
                    if (nizBrojeva[i] == num)
                    {
                        postoji = true;
                        break;
                    }
                //ako ne postoji
                if (!postoji)
                {
                    nizBrojeva[kolicina] = num;
                    kolicina++;
                }
            }
            return nizBrojeva;
        }

        public List<string> novoPitanje(int rb_pitanja)
        {
            //slozi u listu pitanje, A,B,C
            List<string> pitanje = new List<string>();
            pitanje.Add(pitanja[rb_pitanja - 1]);
            //randomiziraj niz brojeva 0,1,2
            var pom_niz = randomiziraj(3, 3);
            for (int i = 0; i < 3; i++)
                pitanje.Add(odgovori[rb_pitanja - 1, pom_niz[i]]);
            return pitanje;
        }
        public bool provjeriOdgovor(string odgovor, int rb_pitanja)
        {
            //dobijemo string s odgovorom i brojem pitanja sto je korisnik 
            //kliknuo i provjerimo je li tocan[i] == odg
            if (tocno[rb_pitanja - 1] == odgovor)
                return true;
            else return false;
        }
    }
}

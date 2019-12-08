using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviskoteka.Klase
{
    class ClassBodovi
    {
        DataSet baza; 

        string[] ime = new string[10];
        int[,] tablicaBodova = new int[10, 5];

        const int kolicinaRezultata = 10;

        public ClassBodovi(DataSet baza_)
        {
            baza = baza_;
            baza.Bodovi.ucitaj();
            ucitajPodatke();
        }

        public string[,] dohvatiImenaBodove()
        {
            string[,] tablica = new string[kolicinaRezultata, 6];

            for(int i = 0; i < kolicinaRezultata; i++)
            {
                for(int j = 0; j< 6; j++)
                {
                    tablica[i, 0] = ime[i];
                    tablica[i, 1] = tablicaBodova[i, 0].ToString();
                    tablica[i, 2] = tablicaBodova[i, 1].ToString();
                    tablica[i, 3] = tablicaBodova[i, 2].ToString();
                    tablica[i, 4] = tablicaBodova[i, 3].ToString();
                    tablica[i, 5] = tablicaBodova[i, 4].ToString();
                }
            }

            return tablica;
        }

        public void noviRezultat(string ime, params int[] bodovi) 
            //u params ocekuje ABCpitalica, Asocijacija, Detekcija, Zavrsna
        {
            int ukupno = 0;
            foreach (var x in bodovi)
                ukupno += x;

            //pogledaj u klasi ima li tko manje
            for(int i = 0; i < kolicinaRezultata; i++)
            {
                if (tablicaBodova[i, 4] < ukupno)
                {
                    baza.Bodovi.noviRezultat(ime, bodovi);
                    ucitajPodatke();
                    
                    return;
                }
            } 
            return;
        }

        void ucitajPodatke()
        {
            for (int i = 0; i < kolicinaRezultata; i++)
            {
                ime[i] = baza.Bodovi.FindByid(i).ime;
                tablicaBodova[i, 0] = baza.Bodovi.FindByid(i).ABCpitalica;
                tablicaBodova[i, 1] = baza.Bodovi.FindByid(i).asocijacija;
                tablicaBodova[i, 2] = baza.Bodovi.FindByid(i).detekcija;
                tablicaBodova[i, 3] = baza.Bodovi.FindByid(i).zavrsna;
                tablicaBodova[i, 4] = baza.Bodovi.FindByid(i).ukupno;
            }
        }
    }
}

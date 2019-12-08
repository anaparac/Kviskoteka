using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviskoteka.Klase
{
    class ClassAsocijacija
    {
        private string rjesenje;
        private string[,] natuknice = new string[4, 4];
        private string[] rezultati = new string[4];

        public ClassAsocijacija(DataSet baza_)
        {
            baza_.Asocijacija.ucitaj();

            int redniBroj = new Random().Next(baza_.Asocijacija.Count() / 4);

            //preskacemo prvih redniBroj igara
            //redni broj redaka krece od nule
            rjesenje = baza_.Asocijacija.FindByid(redniBroj * 4).rjesenje;

            for (var i = 0; i < 4; i++)
            {
                natuknice[0, i] = baza_.Asocijacija.FindByid(redniBroj * 4 + i).natuknica1;
                natuknice[1, i] = baza_.Asocijacija.FindByid(redniBroj * 4 + i).natuknica2;
                natuknice[2, i] = baza_.Asocijacija.FindByid(redniBroj * 4 + i).natuknica3;
                natuknice[3, i] = baza_.Asocijacija.FindByid(redniBroj * 4 + i).natuknica4;
                rezultati[i] = baza_.Asocijacija.FindByid(redniBroj * 4 + i).rezultat;
            }

        }

        public string otvori(int red, int stupac)
        {
            return natuknice[red, stupac];
        }

        public string vratiRiješenje(int id)
        {
            switch (id)
            {
                case 0: //rjesenje
                    return rjesenje;
                case 1: //prva natuknica
                    return rezultati[0];
                case 2:
                    return rezultati[1];
                case 3:
                    return rezultati[2];
                case 4:
                    return rezultati[3];
            }
            return rjesenje;
        }


        public bool provjeriUnos(int id, string pokusaj)
        {
            switch (id)
            {
                case 0: //rjesenje
                    if (String.Equals(pokusaj, rjesenje, StringComparison.OrdinalIgnoreCase))
                        return true;
                    break;
                case 1: //prva natuknica
                    if (String.Equals(pokusaj, rezultati[0], StringComparison.OrdinalIgnoreCase))
                        return true;
                    break;
                case 2:
                    if (String.Equals(pokusaj, rezultati[1], StringComparison.OrdinalIgnoreCase))
                        return true;
                    break;
                case 3:
                    if (String.Equals(pokusaj, rezultati[2], StringComparison.OrdinalIgnoreCase))
                        return true;
                    break;
                case 4:
                    if (String.Equals(pokusaj, rezultati[3], StringComparison.OrdinalIgnoreCase))
                        return true;
                    break;
                default:
                    Console.WriteLine("Nepoznati ID");
                    return false;

            }
            return false;
        }
    }
}

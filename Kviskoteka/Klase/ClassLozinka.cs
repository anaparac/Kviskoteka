using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviskoteka.Klase
{
    class ClassLozinka
    {
        private string loz;
        public ClassLozinka(DataSet baza_)
        {
            baza_.Lozinka.dodajRed();

            loz = baza_.Lozinka.FindByid(0).lozinka;
            Console.WriteLine(loz);
        }

        public string vratiLozinku(DataSet baza_)
        {
            baza_.Lozinka.dodajRed();
            return baza_.Lozinka.FindByid(0).lozinka;
        }
    }
}
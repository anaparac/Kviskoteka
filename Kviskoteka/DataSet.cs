using System;
using System.Data;
using System.IO;

namespace Kviskoteka
{


    partial class DataSet
    {
        partial class BodoviDataTable
        {
            public void ucitaj()
            {
                TextReader file = new StreamReader(@"Bodovi.txt", System.Text.Encoding.Default, true);
                string line;
                int i = 0;
                object[] niz;
                string ime, ABCpitalica, asocijacija, detekcija, zavrsna, ukupno;
                while ((line = file.ReadLine()) != null)
                {
                    ime = line;
                    ABCpitalica = file.ReadLine();
                    asocijacija = file.ReadLine();
                    detekcija = file.ReadLine();
                    zavrsna = file.ReadLine();
                    ukupno = file.ReadLine();

                    niz = new object[] { i, ime, ABCpitalica, asocijacija, detekcija, zavrsna, ukupno };
                    this.LoadDataRow(niz, true);
                    i++;
                }
            }

            public void noviRezultat(string ime, params int[] bodovi)
            {
                int ukupno = 0;
                foreach (var x in bodovi)
                    ukupno += x;

                int numRow = this.Rows.Count;
                for (int i = 0; i < numRow; i++)
                {
                    if (this.FindByid(i).ukupno < ukupno)
                    {
                        for (int j = numRow - 1; j > i; j--)
                        {
                            this.FindByid(j).ukupno = this.FindByid(j - 1).ukupno;
                            this.FindByid(j).ABCpitalica = this.FindByid(j - 1).ABCpitalica;
                            this.FindByid(j).asocijacija = this.FindByid(j - 1).asocijacija;
                            this.FindByid(j).detekcija = this.FindByid(j - 1).detekcija;
                            this.FindByid(j).zavrsna = this.FindByid(j - 1).zavrsna;
                            this.FindByid(j).ime = this.FindByid(j - 1).ime;
                        }

                        this.FindByid(i).ukupno = ukupno;
                        this.FindByid(i).ime = ime;
                        this.FindByid(i).ABCpitalica = bodovi[0];
                        this.FindByid(i).asocijacija = bodovi[1];
                        this.FindByid(i).detekcija = bodovi[2];
                        this.FindByid(i).zavrsna = bodovi[3];

                        spremiPromijene();
                        return;
                    }
                }
            }

            void spremiPromijene()
            {
                int numRow = this.Rows.Count;

                /*
                //1.NACIN
                File.WriteAllText(@"Bodovi.txt", String.Empty);
                string[] noviText = new string[numRow * 6];
                for (int i = 0; i < numRow; i++)
                {
                    Console.WriteLine("ispisujem u dat");
                    //outputFile.WriteLine("ispisujem u dat");
                    noviText[i * 6 + 0] += this.FindByid(i).ime.ToString();
                    noviText[i * 6 + 1] += this.FindByid(i).ABCpitalica.ToString();
                    noviText[i * 6 + 2] += this.FindByid(i).asocijacija.ToString();
                    noviText[i * 6 + 3] += this.FindByid(i).detekcija.ToString();
                    noviText[i * 6 + 4] += this.FindByid(i).zavrsna.ToString();
                    noviText[i * 6 + 5] += this.FindByid(i).ukupno.ToString();
                }
                File.AppendAllLines(@"Bodovi.txt", noviText);
                */
                //File.WriteAllText(@"Bodovi.txt", String.Empty);
                StreamWriter outputFile = new StreamWriter(@"Bodovi.txt", false);
                for (int i = 0; i < numRow; i++)
                {
                    outputFile.WriteLine(this.FindByid(i).ime.ToString());
                    outputFile.WriteLine(this.FindByid(i).ABCpitalica.ToString());
                    outputFile.WriteLine(this.FindByid(i).asocijacija.ToString());
                    outputFile.WriteLine(this.FindByid(i).detekcija.ToString());
                    outputFile.WriteLine(this.FindByid(i).zavrsna.ToString());
                    outputFile.WriteLine(this.FindByid(i).ukupno.ToString());
                }
                outputFile.Close();
            }
        }

        partial class AsocijacijaDataTable
        {
            public void ucitaj()
            {
                TextReader file = new StreamReader(@"Asocijacija.txt", System.Text.Encoding.Default, true);
                string line;
                int i = 0;
                object[] niz;
                string rjesenje, natuknica1, natuknica2, natuknica3, natuknica4, rezultat;
                while ((line = file.ReadLine()) != null)
                {
                    rjesenje = line;
                    for (int j = 0; j < 4; j++)
                    {
                        natuknica1 = file.ReadLine();
                        natuknica2 = file.ReadLine();
                        natuknica3 = file.ReadLine();
                        natuknica4 = file.ReadLine();
                        rezultat = file.ReadLine();
                        niz = new object[] { i, rjesenje, natuknica1, natuknica2, natuknica3, natuknica4, rezultat };
                        this.LoadDataRow(niz, true);
                        i++;
                    }

                }
            }
        }

        partial class ZavrsnaDataTable
        {
            public void ucitaj()
            {
                TextReader file = new StreamReader(@"Zavrsna.txt", System.Text.Encoding.Default, true);
                string line;
                int i = 0;
                object[] niz;
                string pitanje, odgovor, uputa;
                while ((line = file.ReadLine()) != null)
                {
                    pitanje = line;
                    Console.WriteLine("pitanje  " + pitanje);
                    uputa = file.ReadLine();
                    odgovor = file.ReadLine();
                    Console.WriteLine(odgovor);

                    niz = new object[] { i, pitanje, uputa, odgovor };
                    this.LoadDataRow(niz, true);
                    i++;
                }
            }

        }

        partial class DetekcijaDataTable
        {
            public void ucitaj()
            {
                TextReader file = new StreamReader(@"DetekcijaBez.txt", System.Text.Encoding.Default, true);
                string line;
                object[] niz;
                int i = 0;
                string ime, pitanje, odg1, odg2, odg3, vrsta;
                while ((line = file.ReadLine()) != null)
                {
                    ime = line;
                    for (int j = 0; j < 10; j++)
                    {
                        Console.WriteLine("Ime:  " + ime);
                        pitanje = file.ReadLine();
                        Console.WriteLine("pitanje:  " + pitanje);
                        odg1 = file.ReadLine();
                        Console.WriteLine("Odg:  " + odg1);
                        odg2 = file.ReadLine();
                        Console.WriteLine("odg2:  " + odg2);
                        odg3 = file.ReadLine();
                        Console.WriteLine("odg3:  " + odg3);
                        vrsta = file.ReadLine();
                        Console.WriteLine("vrsta:  " + vrsta);
                        niz = new object[] { i, ime, pitanje, odg1, odg2, odg3, vrsta };
                        this.LoadDataRow(niz, true);
                        i++;
                    }
                }

            }

        }

        partial class ABCpitalicaDataTable
        {
            public void dodajRed()
            {

                object[] niz_ = new object[] { 0, "a", "s", "d", "f", 1 };

                this.LoadDataRow(niz_, true);

            }

        }

        partial class ABCpitalicaDataTable
        {
            public void ucitaj()
            {

                TextReader file = new StreamReader(@"ABC.txt", System.Text.Encoding.Default, true);
                string line;
                int i = 0;
                object[] niz;
                string pitanje, odg1, odg2, odg3;
                while ((line = file.ReadLine()) != null)
                {
                    pitanje = line;
                    Console.WriteLine("pitanje  " + pitanje);
                    odg1 = file.ReadLine();
                    odg2 = file.ReadLine();
                    odg3 = file.ReadLine();
                    Console.WriteLine(odg1);
                    Console.WriteLine(odg2);
                    Console.WriteLine(odg3);
                    niz = new object[] { i, pitanje, odg1, odg2, odg3 };
                    this.LoadDataRow(niz, true);
                    i++;
                }
            }
        }

        partial class LozinkaDataTable
        {
            public void dodajRed()
            {
                string line;
                // Pokusaj; 
                using (TextReader fileL = new System.IO.StreamReader(@"Lozinka.txt", System.Text.Encoding.Default, false))
                {
                    if ((line = fileL.ReadLine()) != null)
                    {
                        object[] niz_ = new object[] { line, 0 };
                        this.LoadDataRow(niz_, true);
                    }
                }
            }
        }

    }
}

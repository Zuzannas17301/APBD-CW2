using Cw2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath;
            string exportPath;
            string format;
            var lista = new List<Student>();

            if (args.Length == 3)
            {
                sourcePath = args[0];
                exportPath = args[1];
                format = args[2];
            }
            else
            {
                sourcePath = @"Data\dane.csv"; 
                exportPath = @"result.xml";
                format = "xml";
            }

            using (StreamWriter sw = File.AppendText("log.txt")) ;
            string path = @"Data\dane.csv";
            var fi = new FileInfo(path);
            try
            {
                using (var stream = new StreamReader(fi.OpenRead()))
                {
                    string line = null;
                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] kolumny = line.Split(',');
                        bool existsDuplicate = false;

                        foreach (Student s in lista)
                        {
                            //szukamy duplikatow po imieniu, nazwisku i indeksie
                            string index = "s" + kolumny[4];
                            if (string.Equals(s.Imie, kolumny[0]) && string.Equals(s.Nazwisko, kolumny[1]) &&
                                string.Equals(s.Indeks, index))
                            {
                                existsDuplicate = true;
                            }
                        }

                        //brakujące dane w rekordzie
                        bool missingItem = false;

                        for (int i = 0; i < kolumny.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(kolumny[i]))
                            {
                                missingItem = true;
                            }
                        }

                        StreamWriter sw;
                        //tych studentów, którzy nie są opiwysani przez 9 kolumn pomijamy
                        if (kolumny.Length != 9)
                        {
                            sw = File.AppendText("log.txt");
                            sw.WriteLine("Nieprawidlowa liczba danych. " + line);
                            sw.Close();
                        }
                        else
                        {
                            //jeśi jeden ze studentow posiada pustą wartosc
                            if (missingItem == true)
                            {
                                sw = File.AppendText("log.txt");
                                sw.WriteLine("Kolumna zawiera białe znaki. " + line);
                                sw.Close();
                            }
                            //jeśli występuje duplikat studentów
                            else if (existsDuplicate == true)
                            {
                                sw = File.AppendText("log.txt");
                                sw.WriteLine("Duplikat danych. " + line);
                                sw.Close();
                            }
                            else
                            {
                                Student s = new Student
                                {
                                    Imie = kolumny[0],
                                    Nazwisko = kolumny[1],
                                    DataUrodz = kolumny[5],
                                    Email = kolumny[6],
                                    ImieMatki = kolumny[7],
                                    ImieOjca = kolumny[8],
                                    Indeks = "s" + kolumny[4],
                                    studies = new Studies()
                                };
                                s.studies.Nazwa = kolumny[2];
                                s.studies.Tryb = kolumny[3];
                                lista.Add(s);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                var sw = File.AppendText("log.txt");
                sw.WriteLine("Plik nie istnieje!");
                sw.Close();
            }

            Uczelnia ucz = new Uczelnia
            {
                CreatedAt = DateTime.Now.ToShortDateString(),
                Author = "Zuzanna Komar",
                Students = lista
            };


            FileStream writer = null;
            try
            {
                writer = new FileStream(exportPath, FileMode.Create);
            }
            catch (ArgumentException e)
            {
                var sw = File.AppendText("log.txt");
                sw.WriteLine("Niepoprawna ścieżka!");
                sw.Close();
            }

            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);

            if (string.Equals("xml", format))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Uczelnia));
                xs.Serialize(writer, ucz, xns);
            }

          
        }
    }
}
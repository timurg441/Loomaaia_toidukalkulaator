using Loomaaia_toidukalkulaator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Loomaaia_toidukalkulaator
{
    class Program
    {
        static string failiTee = "loomad.txt";

        static void Main(string[] args)
        {
            List<ILoom> loomadeList = new List<ILoom>();

            // täiendus 2 andmete laadimine failist käivitumisel
            LaadiLoomadFailist(loomadeList);

            Console.WriteLine("LOOMAAIA TOIDUKALKULAATOR");

            while (true)
            {
                Console.WriteLine("\nPEAMENÜÜ");
                Console.WriteLine("1 - Lisa Lõvi");
                Console.WriteLine("2 - Lisa Elevant");
                Console.WriteLine("3 - Lisa Ahv");
                Console.WriteLine("4 - Kuva statistika"); // täiendus 3
                Console.WriteLine("0 - Lõpeta ja salvesta aruanne");
                Console.Write("Vali number: ");

                string valik = Console.ReadLine();

                if (valik == "0")
                {
                    // täiendus 2 salvestame andmed enne sulgemist faili
                    SalvestaLoomadFaili(loomadeList);
                    break;
                }

                if (valik == "4")
                {
                    KuvaStatistika(loomadeList);
                    continue;
                }

                if (valik == "1" || valik == "2" || valik == "3")
                {
                    int kogus = 1;
                    Console.Write("Mitu sellist looma loomaaias on? ");
                    if (!int.TryParse(Console.ReadLine(), out kogus) || kogus <= 0)
                    {
                        Console.WriteLine("Vigane sisend, määrati koguseks: 1");
                        kogus = 1;
                    }

                    int vanus = 5;
                    Console.Write("Mis on looma(de) vanus aastates? (0 = loomapoeg): ");
                    if (!int.TryParse(Console.ReadLine(), out vanus) || vanus < 0)
                    {
                        Console.WriteLine("Vigane sisend, määrati vanuseks: 5 aastat");
                        vanus = 5;
                    }

                    if (valik == "1")
                    {
                        double kaal;
                        Console.Write("Sisesta lõvi kaal (kg): ");
                        if (!double.TryParse(Console.ReadLine(), out kaal))
                        {
                            Console.WriteLine("Vigane tekst! Kaal seatakse vaikimisi väärtusele.");
                            kaal = -1;
                        }
                        loomadeList.Add(new Lõvi(kaal, kogus, vanus));
                        Console.WriteLine("Lõvi või lõvid edukalt lisatud!");
                    }
                    else if (valik == "2")
                    {
                        double kaal;
                        double kihvad;
                        Console.Write("Sisesta elevandi kaal (kg): ");
                        if (!double.TryParse(Console.ReadLine(), out kaal)) kaal = -1;

                        Console.Write("Sisesta elevandi kihvade pikkus (m): ");
                        if (!double.TryParse(Console.ReadLine(), out kihvad)) kihvad = 0;

                        loomadeList.Add(new Elevant(kaal, kihvad, kogus, vanus));
                        Console.WriteLine("Elevant/Elevandid edukalt lisatud!");
                    }
                    else if (valik == "3")
                    {
                        int banaanid;
                        Console.Write("Mitu banaani ahv päevas sööb? ");
                        if (!int.TryParse(Console.ReadLine(), out banaanid)) banaanid = 0;

                        loomadeList.Add(new Ahv(banaanid, kogus, vanus));
                        Console.WriteLine("Ahv või ahvid edukalt lisatud!");
                    }
                }
                else
                {
                    Console.WriteLine("Tundmatu valik, palun proovi uuesti.");
                }
            }

            // samm 4 lõpparuanne ja statistika
            Console.WriteLine("\nLÕPPARUANNE");

            double koguToiduvajadus = 0;
            double lihaVajadus = 0;      // täiendus 1
            double taimneVajadus = 0;    // täiendus 1

            foreach (ILoom loom in loomadeList)
            {
                loom.KuvaInfo();

                double toiduKulu = loom.ArvutaToiduvajadus();
                koguToiduvajadus += toiduKulu;

                // täiendus 1 sortimine toidu tüübi järgi
                if (loom.ToiduTuup == "Liha")
                {
                    lihaVajadus += toiduKulu;
                }
                else if (loom.ToiduTuup == "Taimne")
                {
                    taimneVajadus += toiduKulu;
                }
            }

            Console.WriteLine($"Kogu liha vajadus: {lihaVajadus} kg");
            Console.WriteLine($"Kogu taimse toidu vajadus: {taimneVajadus} kg");
            Console.WriteLine("-------------");
            Console.WriteLine($"Kogu loomaaia päevane toiduvajadus on: {koguToiduvajadus} kg.");
            Console.WriteLine("-------------");
            Console.WriteLine("Programm lõpetas töö edukalt. Vajuta Enter, et sulgeda.");
            Console.ReadLine();
        }

        static void KuvaStatistika(List<ILoom> loomad)
        {
            Console.WriteLine("\nLOOMAAIA STATISTIKA");

            if (loomad.Count == 0)
            {
                Console.WriteLine("Statistika kuvamiseks puuduvad andmed! Lisa esmalt loomi.");
                return;
            }

            var suurimSoodik = loomad.OrderByDescending(l => l.ArvutaToiduvajadus()).First();
            Console.Write("Suurima toiduvajadusega grupp või loom: ");
            suurimSoodik.KuvaInfo();

            var vaikseimSoodik = loomad.OrderBy(l => l.ArvutaToiduvajadus()).First();
            Console.Write("Väikseima toiduvajadusega grupp või loom: ");
            vaikseimSoodik.KuvaInfo();

            double keskmineKulu = loomad.Average(l => l.ArvutaToiduvajadus());
            Console.WriteLine($"Loomagruppide keskmine päevane toidukulu: {keskmineKulu} kg");
        }

        static void SalvestaLoomadFaili(List<ILoom> loomad)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(failiTee))
                {
                    foreach (ILoom loom in loomad)
                    {
                        sw.WriteLine(loom.VormindaFailiJaoks());
                    }
                }
                Console.WriteLine("\nAndmed edukalt faili 'loomad.txt' salvestatud!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Viga faili salvestamisel: " + ex.Message);
            }
        }

        static void LaadiLoomadFailist(List<ILoom> loomad)
        {
            if (!File.Exists(failiTee)) return;

            try
            {
                string[] read = File.ReadAllLines(failiTee);
                foreach (string rida in read)
                {
                    if (string.IsNullOrWhiteSpace(rida)) continue;

                    string[] osad = rida.Split(';');
                    string loomaTuup = osad[0];

                    if (loomaTuup == "Lõvi")
                    {
                        double kaal = double.Parse(osad[1]);
                        int kogus = int.Parse(osad[2]);
                        int vanus = int.Parse(osad[3]);
                        loomad.Add(new Lõvi(kaal, kogus, vanus));
                    }
                    else if (loomaTuup == "Elevant")
                    {
                        double kaal = double.Parse(osad[1]);
                        double kihvad = double.Parse(osad[2]);
                        int kogus = int.Parse(osad[3]);
                        int vanus = int.Parse(osad[4]);
                        loomad.Add(new Elevant(kaal, kihvad, kogus, vanus));
                    }
                    else if (loomaTuup == "Ahv")
                    {
                        int banaanid = int.Parse(osad[1]);
                        int kogus = int.Parse(osad[2]);
                        int vanus = int.Parse(osad[3]);
                        loomad.Add(new Ahv(banaanid, kogus, vanus));
                    }
                }
                Console.WriteLine($"Failist laeti edukalt {loomad.Count} looma või gruppi.");
            }
            catch (Exception)
            {
                Console.WriteLine("Varasemate andmete lugemisel tekkis viga. Alustatakse tühja andmebaasiga.");
            }
        }
    }
}
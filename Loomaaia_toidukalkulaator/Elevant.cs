using Loomaaia_toidukalkulaator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loomaaia_toidukalkulaator
{
    public class Elevant : ILoom
    {
        public double Kaal { get; set; }
        public double KihvadePikkus { get; set; }
        public string ToiduTuup => "Taimne";
        public int Kogus { get; set; }
        public int Vanus { get; set; }

        public Elevant(double kaal, double kihvadePikkus, int kogus, int vanus)
        {
            this.Kaal = kaal <= 0 ? 2000 : kaal;

            this.KihvadePikkus = kihvadePikkus < 0 ? 0 : kihvadePikkus;

            this.Kogus = kogus <= 0 ? 1 : kogus;

            if (vanus < 0 || vanus > 70)
            {
                Console.WriteLine("Viga! Elevandi vanus peab olema 0-70 aastat");
                this.Vanus = 10;
            }
            else
            {
                this.Vanus = vanus;
            }
        }

        public double ArvutaToiduvajadus()
        {
            double baasToiduvajadus = (Kaal * 0.10) + (KihvadePikkus * 20);

            // täiendus 4 loomapoeg sööb poole vähem
            if (Vanus < 1)
            {
                baasToiduvajadus *= 0.5;
            }

            return baasToiduvajadus * Kogus;
        }

        public void KuvaInfo()
        {
            string tüüpSõna = Vanus < 1 ? "Elevandipoeg" : "Elevant";
            Console.WriteLine($"- {tüüpSõna} (Kogus: {Kogus}, Ühe kaal: {Kaal} kg, Kihvad: {KihvadePikkus} m, Vanus: {Vanus} a). Toidutüüp: {ToiduTuup}. Kokku päevane toit: {ArvutaToiduvajadus()} kg.");
        }

        public string VormindaFailiJaoks()
        {
            return $"Elevant;{Kaal};{KihvadePikkus};{Kogus};{Vanus}";
        }
    }
}


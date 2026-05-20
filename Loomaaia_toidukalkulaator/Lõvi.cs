using Loomaaia_toidukalkulaator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loomaaia_toidukalkulaator
{
    public class Lõvi : ILoom
    {
        public double Kaal { get; set; }
        public string ToiduTuup => "Liha";
        public int Kogus { get; set; }
        public int Vanus { get; set; }

        public Lõvi(double kaal, int kogus, int vanus)
        {
            // valideerimine, siin lõvi kaal peab olema vahemikus 0 kuni 400 kg maksimum
            if (kaal <= 0 || kaal >= 400)
            {
                Console.WriteLine("Viga! Vigane lõvi kaal. Määramiseks valiti vaikimisi 150 kg.");
                this.Kaal = 150;
            }
            else
            {
                this.Kaal = kaal;
            }

            this.Kogus = kogus <= 0 ? 1 : kogus;
            this.Vanus = vanus < 0 ? 0 : vanus;
        }

        public double ArvutaToiduvajadus()
        {
            // 5% kaal
            double baasToiduvajadus = Kaal * 0.05;

            // täiendus 4 kui on loomapoeg alla 1 aasta, sööb poole vähem
            if (Vanus < 1)
            {
                baasToiduvajadus *= 0.5;
            }

            return baasToiduvajadus * Kogus;
        }

        public void KuvaInfo()
        {
            string tüüpSõna = Vanus < 1 ? "Lõvipoeg" : "Lõvi";
            Console.WriteLine($"- {tüüpSõna} (Kogus: {Kogus}, Ühe kaal: {Kaal} kg, Vanus: {Vanus} a). Toidutüüp: {ToiduTuup}. Kokku päevane toit: {ArvutaToiduvajadus()} kg.");
        }

        public string VormindaFailiJaoks()
        {
            return $"Lõvi;{Kaal};{Kogus};{Vanus}";
        }
    }
}

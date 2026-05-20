using Loomaaia_toidukalkulaator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loomaaia_toidukalkulaator
{
    public class Ahv : ILoom
    {
        public int BanaanideArvPäevas { get; set; }
        public string ToiduTuup => "Taimne";
        public int Kogus { get; set; }
        public int Vanus { get; set; }

        public Ahv(int banaanideArv, int kogus, int vanus)
        {
            // valideerimine
            this.BanaanideArvPäevas = banaanideArv < 0 ? 0 : banaanideArv;
            this.Kogus = kogus <= 0 ? 1 : kogus;
            this.Vanus = vanus < 0 ? 0 : vanus;
        }

        public double ArvutaToiduvajadus()
        {
            // üks banaan kaalub 0.2 kg
            double baasToiduvajadus = BanaanideArvPäevas * 0.2;

            // täienduss 4 Loomapoeg sööb poole vähem banaanitoitu
            if (Vanus < 1)
            {
                baasToiduvajadus *= 0.5;
            }

            return baasToiduvajadus * Kogus;
        }

        public void KuvaInfo()
        {
            string tüüpSõna = Vanus < 1 ? "Ahvipoeg" : "Ahv";
            Console.WriteLine($"- {tüüpSõna} (Kogus: {Kogus}, Banaane: {BanaanideArvPäevas} tk, Vanus: {Vanus} a). Toidutüüp: {ToiduTuup}. Kokku päevane toit: {ArvutaToiduvajadus()} kg.");
        }

        public string VormindaFailiJaoks()
        {
            return $"Ahv;{BanaanideArvPäevas};{Kogus};{Vanus}";
        }
    }
}


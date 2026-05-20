using System;
using System.Collections.Generic;
using System.Text;

namespace Loomaaia_toidukalkulaator
{
    public interface ILoom
    {
        string ToiduTuup { get; } // täiendus 1 liha või taimne
        int Kogus { get; } // boonus ülessane mitu samasugust looma on
        int Vanus { get; } // täeindus 4 looma vanus aastates

        double ArvutaToiduvajadus();
        void KuvaInfo();
        string VormindaFailiJaoks(); // faili salvestamine
    }
}

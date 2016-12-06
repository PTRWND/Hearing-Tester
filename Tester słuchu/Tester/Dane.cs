using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Dane
    {
        List<double> wyniki = new List<double>();

        public void zapisz(double wynik)
        {
            wynik /= 1000;
            wynik = 22 - wynik;

            wyniki.Add(wynik);
        }

        public double odczytaj()
        {
            return wyniki.Average();
        }

        public void kasuj_dane()
        {
            wyniki.Clear();
        }
    }
}

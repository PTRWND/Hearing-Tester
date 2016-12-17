using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Data
    {
        List<double> results = new List<double>();

        public void Save(double wynik)
        {
            wynik /= 1000;
            wynik = 22 - wynik;

            results.Add(wynik);
        }

        public double Read()
        {
            return results.Average();
        }

        public void EraseData()
        {
            results.Clear();
        }
    }
}

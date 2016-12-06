using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tester;

namespace Tester
{
    class Tester_sł
    {
        
        int start, stop;
        SoundPlayer dźwięk = new SoundPlayer(Tester.Properties.Resources.sweep_22000Hz_2000Hz_20s);
        Dane data = new Dane();

        public void wczytaj_plik()
        {
            dźwięk.Load();
        }

        public void odtwórz()
        {
            dźwięk.Play();
            start = Environment.TickCount & Int32.MaxValue;
        }

        public void zakończ()
        {                       
            dźwięk.Stop();
            stop = Environment.TickCount & Int32.MaxValue;
            data.zapisz(stop - start);
        }

        public string podaj_wynik()
        {
            System.Console.Beep(1000, 200);
            double wynik = Math.Round(data.odczytaj(), 2);
            data.kasuj_dane();
            Convert.ToString(wynik);
            return "\nWynik:\nMaksymalna słyszalna częstotliwość [kHz]: " + Convert.ToString(wynik);
        }
    }
}

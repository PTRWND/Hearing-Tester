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
    class HearingTest
    {
        
        int start, stop;
        SoundPlayer sound = new SoundPlayer(Tester.Properties.Resources.sweep_22000Hz_2000Hz_20s);
        Data data = new Data();

        public void LoadFile()
        {
            sound.Load();
        }

        public void Play()
        {
            sound.Play();
            start = Environment.TickCount & Int32.MaxValue;
        }

        public void Stop()
        {                       
            sound.Stop();
            stop = Environment.TickCount & Int32.MaxValue;
            data.Save(stop - start);
        }

        public string ShowResult()
        {
            System.Console.Beep(1000, 200);
            double result = Math.Round(data.Read(), 2);
            data.EraseData();
            Convert.ToString(result);
            return "\nWynik:\nMaksymalna słyszalna częstotliwość [kHz]: " + Convert.ToString(result);
        }
    }
}

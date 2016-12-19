using System;
using System.Media;
using Tester;
using System.Diagnostics;

namespace Tester
{
    class HearingTest
    {
        
        int start, stop;
        SoundPlayer sound = new SoundPlayer(Tester.Properties.Resources.sweep_22000Hz_2000Hz_20s);
        Data data = new Data();
        Stopwatch timeValue = new Stopwatch();

        public void LoadFile()
        {
            sound.Load();
        }

        public void Play()
        {
            timeValue.Reset();
            sound.Play();
            timeValue.Start();
        }

        public void Stop()
        {                       
            sound.Stop();
            timeValue.Stop();
            data.Save(timeValue.ElapsedMilliseconds);
            timeValue.Reset();
        }

        public void TimeOut()
        {
            data.EraseData();
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

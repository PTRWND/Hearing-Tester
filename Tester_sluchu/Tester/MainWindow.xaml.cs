using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tester;

namespace Tester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HearingTest test = new HearingTest();
        Timer time = new Timer(20000);
        bool ready = true;
        string displayMemory = string.Empty;
        int cycleCount;
        int cycleNumber = 0;
        int startingPosition;

        public MainWindow()
        {
            InitializeComponent();
            WriteLine("Wybierz ilość cykli i kliknij Start");
            Cycles();
            test.LoadFile();
        }

        private void Button_Start_Stop(object sender, RoutedEventArgs e)
        {
            if (cycleCount >= 0)
            {
                cycleCount--;

                if (ready == true)
                {
                    test.Play();
                    cycleNumber++;
                    WriteLine("Cykl nr: "+ cycleNumber +" w trakcie...");
                    time.Elapsed += time_Elapsed;
                    time.Start();
                    time.AutoReset = true;
                    ready = !ready;
                }
                else
                {
                    test.Stop();
                    time.Stop();
                    time.Elapsed -= time_Elapsed;
                    if (cycleCount > 0)
                    {
                        WriteLine("Cykl nr: " + cycleNumber + " zakończony.\nAby rozpocząć kolejny cykl kliknij Start!");
                    }
                    ready = !ready;

                    if (cycleCount <= 0)
                    {
                        WriteLine(test.ShowResult());
                        cycleCount = 2 * int.Parse(cycleQuantity.SelectedValue.ToString());
                        cycleNumber = 0;
                    }
                }
            }
        }

        private void time_Elapsed(object sender, ElapsedEventArgs e)
        {
            test.Stop();
            time.Stop();
            time.Elapsed -= time_Elapsed;
            ready = true;
            cycleNumber = 0;
            test.TimeOut();
            cycleCount = startingPosition;
            WriteLine("Brak reakcji użytkownika - test zakończył się.\nAby rozpocząć od nowa wciśnij start");
            MessageBox.Show("Brak reakcji użytkownika - test zakończył się.\nAby rozpocząć od nowa wciśnij start");
        }

        public void WriteLine(string text)
        {
            displayMemory += text;
            Dispatcher.Invoke(DisplayUpdate);
            displayMemory += Environment.NewLine;
        }

        public void DisplayUpdate()
        {
            display.Content = displayMemory;
            display.ScrollToEnd();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cycleCount = 2 * int.Parse(cycleQuantity.SelectedValue.ToString());
            SaveStartingPosition(cycleCount);
        }

        public void SaveStartingPosition(int position)
        {
            startingPosition = position;
        }

        public void Cycles()
        {
            for (int x = 0; x < 10; x++)
            {
                cycleQuantity.Items.Add(x + 1);
            }
        }
    }
}

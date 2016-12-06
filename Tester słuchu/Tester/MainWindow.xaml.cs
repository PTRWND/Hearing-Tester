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
        Tester_sł dźwięk = new Tester_sł();
        Dane data = new Dane();
        Timer czas = new Timer(20000);
        bool działanie = true;
        string stan_wyświetlacza = string.Empty;
        int ilość_powtórzeń;
        int numer_cyklu = 0;
        int stan_początkowy;

        public MainWindow()
        {
            InitializeComponent();
            Napisz_linię("Wybierz ilość cykli i kliknij Start");
            cykle();
            dźwięk.wczytaj_plik();
        }

        private void Button_Start_Stop(object sender, RoutedEventArgs e)
        {
            if (ilość_powtórzeń >= 0)
            {
                ilość_powtórzeń--;

                if (działanie == true)
                {
                    dźwięk.odtwórz();
                    numer_cyklu++;
                    Napisz_linię("Cykl nr: "+ numer_cyklu +" w trakcie...");
                    czas.Elapsed += czas_Elapsed;
                    czas.Start();
                    czas.AutoReset = true;
                    działanie = !działanie;
                }
                else
                {
                    dźwięk.zakończ();
                    czas.Stop();
                    czas.Elapsed -= czas_Elapsed;
                    if (ilość_powtórzeń > 0)
                    {
                        Napisz_linię("Cykl nr: " + numer_cyklu + " zakończony.\nAby rozpocząć kolejny cykl kliknij Start!");
                    }
                    działanie = !działanie;

                    if (ilość_powtórzeń <= 0)
                    {
                        Napisz_linię(dźwięk.podaj_wynik());
                        ilość_powtórzeń = 2 * int.Parse(ilość_cykli.SelectedValue.ToString());
                        numer_cyklu = 0;
                    }
                }
            }
        }

        private void czas_Elapsed(object sender, ElapsedEventArgs e)
        {
            czas.Stop();
            czas.Elapsed -= czas_Elapsed;
            działanie = true;
            numer_cyklu = 0;
            data.kasuj_dane();
            ilość_powtórzeń = stan_początkowy;
            MessageBox.Show("Brak reakcji użytkownika - test zakończył się.\nAby rozpocząć od nowa wciśnij start");
        }

        public void Napisz_linię(string napis)
        {
            stan_wyświetlacza += napis;
            Wyświetlacz.Content = stan_wyświetlacza;
            Wyświetlacz.ScrollToEnd();
            stan_wyświetlacza += Environment.NewLine;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ilość_powtórzeń = 2 * int.Parse(ilość_cykli.SelectedValue.ToString());
            stan_licznika_powtórzeń(ilość_powtórzeń);
        }

        public void stan_licznika_powtórzeń(int wartość)
        {
            stan_początkowy = wartość;
        }

        public void cykle()
        {
            for (int x = 0; x < 10; x++)
            {
                ilość_cykli.Items.Add(x + 1);
            }
        }
    }
}

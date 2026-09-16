using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Geometria_Bakilv_wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Eseménykezelők hozzárendelése a Rádiógombokhoz
            RbCircle.Checked += Shape_Checked;
            RbRectangle.Checked += Shape_Checked;
            RbTriangle.Checked += Shape_Checked;
        }

        // Váltás a panel-ek között a kiválasztott alakzat alapján
        private void Shape_Checked(object sender, RoutedEventArgs e)
        {
            if (PanelCircle == null || PanelRectangle == null || PanelTriangle == null)
                return;

            PanelCircle.Visibility = Visibility.Collapsed;
            PanelRectangle.Visibility = Visibility.Collapsed;
            PanelTriangle.Visibility = Visibility.Collapsed;

            if (RbCircle.IsChecked == true)
            {
                PanelCircle.Visibility = Visibility.Visible;
            }
            else if (RbRectangle.IsChecked == true)
            {
                PanelRectangle.Visibility = Visibility.Visible;
            }
            else if (RbTriangle.IsChecked == true)
            {
                PanelTriangle.Visibility = Visibility.Visible;
            }

            ResetResults();
        }

        // Kiszámítás gomb eseménykezelője
        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (RbCircle.IsChecked == true)
            {
                CalculateCircle();
            }
            else if (RbRectangle.IsChecked == true)
            {
                CalculateRectangle();
            }
            else if (RbTriangle.IsChecked == true)
            {
                CalculateTriangle();
            }
        }

        // Kör számítása
        private void CalculateCircle()
        {
            if (double.TryParse(TxtRadius.Text, out double r) && r > 0)
            {
                double terulet = Math.PI * r * r;
                double kerulet = 2 * Math.PI * r;

                DisplayResults(kerulet, terulet);
            }
            else
            {
                MessageBox.Show("Kérjük, adjon meg egy érvényes, pozitív számot a sugárhoz!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Téglalap számítása
        private void CalculateRectangle()
        {
            if (double.TryParse(TxtRectA.Text, out double a) && a > 0 &&
                double.TryParse(TxtRectB.Text, out double b) && b > 0)
            {
                double terulet = a * b;
                double kerulet = 2 * (a + b);

                DisplayResults(kerulet, terulet);
            }
            else
            {
                MessageBox.Show("Kérjük, adjon meg érvényes, pozitív számokat az oldalakhoz!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Háromszög számítása (Heron-képlet)
        private void CalculateTriangle()
        {
            if (double.TryParse(TxtTriA.Text, out double a) && a > 0 &&
                double.TryParse(TxtTriB.Text, out double b) && b > 0 &&
                double.TryParse(TxtTriC.Text, out double c) && c > 0)
            {
                // Háromszög-egyenlőtlenség ellenőrzése
                if (a + b > c && a + c > b && b + c > a)
                {
                    double kerulet = a + b + c;
                    double s = kerulet / 2;
                    double terulet = Math.Sqrt(s * (s - a) * (s - b) * (s - c));

                    DisplayResults(kerulet, terulet);
                }
                else
                {
                    MessageBox.Show("A megadott oldaljellemzőkkel nem képezhető háromszög!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Kérjük, adjon meg érvényes, pozitív számokat az oldalakhoz!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Eredmények kiíratása a felületre
        private void DisplayResults(double kerulet, double terulet)
        {
            LblPerimeter.Text = $"{kerulet:F2}";
            LblArea.Text = $"{terulet:F2}";
        }

        // Eredmények alaphelyzetbe állítása
        private void ResetResults()
        {
            if (LblPerimeter != null && LblArea != null)
            {
                LblPerimeter.Text = "0";
                LblArea.Text = "0";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Cbx_Haken.IsChecked = false;
        }

        private void Btn_KlickMich_Click(object sender, RoutedEventArgs e)
        {
            Lbl_Output.Content = Tbx_Input.Text;

            if(Cbx_Haken.IsChecked == true)
                Tbl_Output.Text = (Cbb_Items.SelectedItem as ComboBoxItem)?.Content.ToString();


            Btn_KlickMich.Content = Sdr_Wert.Value;

        }

        private void Sdr_Wert_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Btn_KlickMich.Content = Sdr_Wert.Value;
        }

        private void NeuesFenster_Click(object sender, RoutedEventArgs e)
        {
            Window wnd = new MainWindow();

            wnd.Title = "Neues Fenster";

            wnd.Show();
        }

        private void NeuerDialog_Click(object sender, RoutedEventArgs e)
        {
            Window wnd = new MainWindow();

            wnd.Title = "Neues Dialog-Fenster";

            if (wnd.ShowDialog() == true)
                Lbl_Output.Content = "OKAY wurde geklickt";
        }

        private void Schließen_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Möchtest du das Fenster wirklich schließen?", "Fenster schließen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                this.Close();

            //Application.Current.Shutdown();
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

namespace DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Person> Personenliste { get; set; } = new ObservableCollection<Person>()
        {
            new Person(){Name = "Anna Nass", Alter=56},
            new Person(){Name = "Rainer Zufall", Alter=16},
            new Person(){Name = "Maria Schmidt", Alter=87},
        };

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Btn_Show_Click(object sender, RoutedEventArgs e)
        {
            Person person = (Spl_DataContextBeispiel.DataContext as Person);

            MessageBox.Show($"{person.Name} ({person.Alter})");
        }

        private void Btn_Altern_Click(object sender, RoutedEventArgs e)
        {
            Person person = (Spl_DataContextBeispiel.DataContext as Person);

            person.Alter++;
            //person.OnPropertyChanged("Alter");

            Debug.WriteLine(person.Alter.ToString());
        }

        private void Btn_NeuesDatum_Click(object sender, RoutedEventArgs e)
        {
            Person person = (Spl_DataContextBeispiel.DataContext as Person);

            person.WichtigeTage.Add(new DateTime(2022, 4, 6));
            person.OnPropertyChanged("LastObject");
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Personenliste.Add(new Person() { Name = "Hugo Meier", Alter = 67 });
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Personenliste)));
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Lstb_Personen.SelectedItem != null)
                Personenliste.Remove(Lstb_Personen.SelectedItem as Person);
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Personenliste)));
        }
    }
}

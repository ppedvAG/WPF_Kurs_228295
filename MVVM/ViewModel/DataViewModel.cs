using MVVM.Model;
using MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.ViewModel
{
    public class DataViewModel
    {
        public CustomCommand NewCmd { get; set; }
        public CustomCommand ÄndernCmd { get; set; }
        public CustomCommand LöschenCmd { get; set; }
        public CustomCommand BeendenCmd { get; set; }

        public ObservableCollection<Person> Personenliste { get { return Model.Person.Personenliste; } }


        public DataViewModel()
        {
            NewCmd = new CustomCommand
                (
                    p =>
                    {
                        DetailView dialog = new DetailView();

                        if (dialog.ShowDialog() == true)
                            Personenliste.Add((dialog.DataContext as DetailViewModel).NeuePerson);
                    },
                    p => true
                );
            ÄndernCmd = new CustomCommand
                (
                    p =>
                    {
                        DetailView dialog = new DetailView();

                        (dialog.DataContext as DetailViewModel).NeuePerson = new Model.Person(p as Model.Person);

                        dialog.Title = (dialog.DataContext as DetailViewModel).NeuePerson.Vorname + " " + (dialog.DataContext as DetailViewModel).NeuePerson.Nachname;

                        if (dialog.ShowDialog() == true)
                            Personenliste[Personenliste.IndexOf(p as Person)] = (dialog.DataContext as DetailViewModel).NeuePerson;
                    },
                    p => p != null
                );
            LöschenCmd = new CustomCommand
                (
                    p =>
                    {
                        Person person = p as Person;
                        if (MessageBox.Show($"Soll {person.Vorname} {person.Nachname} wirklich gelöscht werden?", $"{person.Vorname} {person.Nachname} löschen?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            Personenliste.Remove(person);
                    },
                    p => p != null
                );
            BeendenCmd = new CustomCommand
                (
                    p => Application.Current.Shutdown(),
                    p => true
                );
        }
    }
}

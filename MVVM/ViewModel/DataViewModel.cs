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
        //Command-Properties
        public CustomCommand NewCmd { get; set; }
        public CustomCommand ÄndernCmd { get; set; }
        public CustomCommand LöschenCmd { get; set; }
        public CustomCommand BeendenCmd { get; set; }

        //Listen-Property, welche auf die Liste des Models verlinkt
        public ObservableCollection<Person> Personenliste { get { return Model.Person.Personenliste; } }


        public DataViewModel()
        {
            //Command-Definitionen
            //Hinzufügen einer neuen Person
            NewCmd = new CustomCommand
                (
                   //Exe:
                   p =>
                    {
                        //Instanzieren eines neuen DetailViews
                        DetailView dialog = new DetailView();

                        //Aufruf des DetailViews mit Überprüfung auf dessen DialogResult(wird true, wenn der Benutzer OK klickt)
                        if (dialog.ShowDialog() == true)
                            //Hinzufügen der neuen Person zu Liste
                            Personenliste.Add((dialog.DataContext as DetailViewModel).NeuePerson);
                    },
                    p => true
                );
            //Ändern einer bestehenden Person
            ÄndernCmd = new CustomCommand
                (
                   //Exe:
                   p =>
                    {
                        //Vgl. NeuCmd (s.o.)
                        DetailView dialog = new DetailView();

                        //Zuweisung einer Kopie der ausgewählten Person in die 'AktuellePerson'-Property des neuen DetailViewModels
                        (dialog.DataContext as DetailViewModel).NeuePerson = new Model.Person(p as Model.Person);

                        //Ändern des Titels des neuen DetailViews
                        dialog.Title = (dialog.DataContext as DetailViewModel).NeuePerson.Vorname + " " + (dialog.DataContext as DetailViewModel).NeuePerson.Nachname;

                        if (dialog.ShowDialog() == true)
                            //Austausch der (veränderten) Person-Kopie mit dem Original in der Liste
                            Personenliste[Personenliste.IndexOf(p as Person)] = (dialog.DataContext as DetailViewModel).NeuePerson;
                    },
                   //CanExe: Kann ausgeführt werden, wenn der Parameter (der im DataGrid ausgewählte Eintrag) eine Person ist.
                   //Fungiert als Null-Prüfung
                   p => p != null
                );
            //Löschen einer Person
            LöschenCmd = new CustomCommand
                (
                    //Exe: Löschen der ausgewählten Person (nach Rückfrage per MessageBox)
                    p =>
                    {
                        Person person = p as Person;
                        if (MessageBox.Show($"Soll {person.Vorname} {person.Nachname} wirklich gelöscht werden?", $"{person.Vorname} {person.Nachname} löschen?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            Personenliste.Remove(person);
                    },
                    //CanExe: s.o.
                    p => p != null
                );
            //Schließen des Programms
            BeendenCmd = new CustomCommand
                (
                    //Exe: Schließen der Applikation
                    p => Application.Current.Shutdown(),
                    p => true
                );
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MVVM.ViewModel
{
    public class DetailViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        //Command-Properties
        public event PropertyChangedEventHandler? PropertyChanged;
        public CustomCommand OkCmd { get; set; }
        public CustomCommand AbbruchCmd { get; set; }

        //Property, welche die neue oder zu bearbeitende Person beinhaltet
        private Model.Person neuePerson;
        public Model.Person NeuePerson
        {
            get { return neuePerson; }
            set
            {
                neuePerson = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NeuePerson)));
            }
        }

        //Properties der einzlenen Eigenschaften
        public string Vorname { get => NeuePerson.Vorname; set => NeuePerson.Vorname = value; }
        public string Nachname { get => NeuePerson.Nachname; set => NeuePerson.Nachname = value; }
        public DateTime Geburtsdatum { get => NeuePerson.Geburtsdatum; set => NeuePerson.Geburtsdatum = value; }
        public bool Verheiratet { get => NeuePerson.Verheiratet; set => NeuePerson.Verheiratet = value; }
        public Color Lieblingsfarbe { get => NeuePerson.Lieblingsfarbe; set => NeuePerson.Lieblingsfarbe = value; }
        public int Kinder { get => NeuePerson.Kinder; set => NeuePerson.Kinder = value; }


        public DetailViewModel()
        {
            NeuePerson = new Model.Person();

            //OK-Command (Bestätigung)
            OkCmd = new CustomCommand
                (
                    //Exe:
                    p =>
                    {
                        //Nachfrage auf Korrektheit der Daten per MessageBox
                        string ausgabe = NeuePerson.Vorname + " " + NeuePerson.Nachname + " (" + NeuePerson.Geschlecht + ")\n" + NeuePerson.Geburtsdatum.ToShortDateString() + "\n" + NeuePerson.Lieblingsfarbe.ToString();
                        if (NeuePerson.Verheiratet) ausgabe = ausgabe + "\nIst verheiratet";
                        if (NeuePerson.Kinder > 0) ausgabe = ausgabe + $"\nHat {NeuePerson.Kinder} {(NeuePerson.Kinder == 1 ? "Kind" : "Kinder")}";
                        if (MessageBox.Show(ausgabe + "\nAbspeichern?", NeuePerson.Vorname + " " + NeuePerson.Nachname, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            //Setzen des DialogResults des Views (welches per Parameter übergeben wurde) auf true, damit das ListView weiß, dass es weiter
                            //machen kann (d.h. die neuen Person einfügen bzw. austauschen)
                            (p as Window).DialogResult = true;
                            //Schließen des Views
                            (p as Window).Close();
                        }
                    },
                    //CanExe: Validierungs-Check
                    p => HasNoError
                );

            //Abbruch-Cmd
            AbbruchCmd = new CustomCommand
                (
                    //Exe: Schließen des Fensters
                    p => (p as Window).Close(),
                    p => true
                );
        }

        public string Error => null;

        public Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();

        private void CollectErrors()
        {
            Errors.Clear();

            if (Vorname.Length <= 0 || Vorname.Length > 50) Errors.Add(nameof(Vorname), "Bitte geben Sie Ihren Vornamen ein.");
            else if (!Vorname.All(x => Char.IsLetter(x))) Errors.Add(nameof(Vorname), "Der Vorname darf nur Buchstaben enthalten.");
            else if (Char.IsLower(Vorname.First())) Errors.Add(nameof(Vorname), "Der Vorname muss mit einem Großbuchstaben beginnen");

            if (Nachname.Length <= 0 || Nachname.Length > 50) Errors.Add(nameof(Nachname), "Bitte geben Sie Ihren Nachnamen ein.");
            else if (!Nachname.All(x => Char.IsLetter(x))) Errors.Add(nameof(Nachname), "Der Nachname darf nur Buchstaben enthalten.");
            else if (Char.IsLower(Nachname.First())) Errors.Add(nameof(Nachname), "Der Nachname muss mit einem Großbuchstaben beginnen");

            if (Geburtsdatum > DateTime.Now) Errors.Add(nameof(Geburtsdatum), "Das Geburtsdatum darf nicht in der Zukunft liegen.");
            else if (DateTime.Now.Year - Geburtsdatum.Year > 150) Errors.Add(nameof(Geburtsdatum), "Das Geburtsdatum darf nicht mehr als 150 Jahre in der Vergangenheit liegen.");

            if (Lieblingsfarbe.ToString().Equals("#00000000")) Errors.Add(nameof(Lieblingsfarbe), "Wählen Sie Ihre Lieblingsfarbe aus.");

            if (Kinder < 0) Errors.Add(nameof(Kinder), "Dieser Wert muss mindestens '0' sein.");

        }

        public string this[string columnName]
        {
            get
            {
                CollectErrors();
                return Errors.ContainsKey(columnName) ? Errors[columnName] : string.Empty;
            }
        }

        public bool HasError{ get => Errors.Count > 0; }
        public bool HasNoError{ get => Errors.Count == 0; }

    }
}

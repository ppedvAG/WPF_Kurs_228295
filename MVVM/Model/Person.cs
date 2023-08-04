using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MVVM.Model
{
    //Der Model-Teil beinhaltet alle Modelklassen und evtl. Klassen, welche nur mit diesen interagieren.
    //Keine Model-Klasse darf einen Referenz auf den ViewModel- oder den Model-Teil beinhalten
    public class Person //: INotifyPropertyChanged , IDataErrorInfo
    {
        #region Statische Member
        //Statische Liste zum Speichern der Personenobjekte
        public static ObservableCollection<Person> Personenliste { get; set; } = new ObservableCollection<Person>();

        //Statische Methode zum Laden der Personenobjekte (ruft StartViewModel auf)
        public static void LadePersonenAusDb()
        {
            Personenliste.Add(new Person() { Vorname = "Anna", Nachname = "Nass", Geburtsdatum = new DateTime(1999, 5, 23), Geschlecht = Gender.Weiblich, Verheiratet = true, Lieblingsfarbe = Colors.CornflowerBlue });
            Personenliste.Add(new Person() { Vorname = "Rainer", Nachname = "Zufall", Geburtsdatum = new DateTime(1977, 4, 2), Geschlecht = Gender.Männlich, Verheiratet = false, Lieblingsfarbe = Colors.IndianRed });
        }
        #endregion

        //Durch INotifyPropertyChanged verlangtes Event
        public event PropertyChangedEventHandler? PropertyChanged;

        //Properties (Setter rufen PropertyChanged-Event zur Information der GUI auf)
        private string vorname;
        public string Vorname { get => vorname; set { vorname = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vorname))); } }

        private string nachname;
        public string Nachname { get => nachname; set { nachname = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nachname))); } }

        private DateTime geburtsdatum;
        public DateTime Geburtsdatum { get => geburtsdatum; set { geburtsdatum = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Geburtsdatum))); } }

        private bool verheiratet;
        public bool Verheiratet { get => verheiratet; set { verheiratet = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Verheiratet))); } }

        private Color lieblingsfarbe;
        public Color Lieblingsfarbe { get => lieblingsfarbe; set { lieblingsfarbe = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Lieblingsfarbe))); } }

        private Gender geschlecht;
        public Gender Geschlecht { get => geschlecht; set { geschlecht = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Geschlecht))); } }

        private int kinder;
        public int Kinder { get => kinder; set { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Kinder))); kinder = value; } }

        //Parameterloser Konstruktor (für Standart-Vorbelegung)
        public Person()
        {
            this.vorname = String.Empty;
            this.nachname = String.Empty;
            this.geburtsdatum = DateTime.Now;
        }
        //Kopierkonstruktor (für 1-zu-1-Kopien)
        public Person(Person altePerson)
        {
            this.vorname = altePerson.Vorname;
            this.nachname = altePerson.Nachname;
            this.geschlecht = altePerson.Geschlecht;
            this.verheiratet = altePerson.Verheiratet;
            this.lieblingsfarbe = altePerson.Lieblingsfarbe;
            this.kinder = altePerson.Kinder;

            this.geburtsdatum = new DateTime(altePerson.Geburtsdatum.Year, altePerson.Geburtsdatum.Month, altePerson.Geburtsdatum.Day);
        }

       
    }
}


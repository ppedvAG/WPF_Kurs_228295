using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MVVM.Model
{   
    public class Person //: INotifyPropertyChanged , IDataErrorInfo
    {
        public static ObservableCollection<Person> Personenliste = new ObservableCollection<Person>();

        public static void LadePersonenAusDb()
        {
            Personenliste.Add(new Person() { Vorname = "Anna", Nachname = "Nass", Geburtsdatum = new DateTime(1999, 5, 23), Geschlecht = Gender.Weiblich, Verheiratet = true, Lieblingsfarbe = Colors.CornflowerBlue, Kinder = 1 });
            Personenliste.Add(new Person() { Vorname = "Rainer", Nachname = "Zufall", Geburtsdatum = new DateTime(1977, 4, 2), Geschlecht = Gender.Männlich, Verheiratet = false, Lieblingsfarbe = Colors.IndianRed, Kinder = 2 });
        }



        public event PropertyChangedEventHandler? PropertyChanged;

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

        public Person()
        {
            this.vorname = String.Empty;
            this.nachname = String.Empty;
            this.geburtsdatum = DateTime.Now;
        }
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


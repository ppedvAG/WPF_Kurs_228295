using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding
{
    public class Person : INotifyPropertyChanged
    {
        private int alter;

        public string Name { get; set; }
        public int Alter { get => alter; set { alter = value; OnPropertyChanged(nameof(Alter)); } }

        public List<DateTime> WichtigeTage { get; set; } = new List<DateTime>()
        {
            new DateTime(2003, 12, 3),
            new DateTime(1999, 4, 15),
            DateTime.Now,
            new DateTime(2006,1,1)
        };

        public DateTime LastObject { get => WichtigeTage.Last(); }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public override string ToString()
        {
            return $"{Name} ({Alter})";
        }
    }
}

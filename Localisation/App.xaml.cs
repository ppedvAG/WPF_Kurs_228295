using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Localisation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("settings.txt"))
            {
                sw.WriteLine(Thread.CurrentThread.CurrentUICulture.Name);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                using (StreamReader sw = new StreamReader("settings.txt"))
                {
                    string language = sw.ReadLine();

                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
                }
            }
            catch
            {

            }
        }
    }
}

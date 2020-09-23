using kakashi.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace kakashi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        protected override void OnStartup(StartupEventArgs e)
        {
             base.OnStartup(e);
            LoginWindown LWindow = new Views.LoginWindown();
            LWindow.Title = "kakashi";
            App.Current.MainWindow = LWindow;
            App.Current.MainWindow.Show();
        }
    }
}

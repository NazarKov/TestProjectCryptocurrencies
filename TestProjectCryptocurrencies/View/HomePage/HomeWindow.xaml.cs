using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestProjectCryptocurrencies.ViewModel.HomePage;

namespace TestProjectCryptocurrencies.View.HomePage
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Properties.Settings.Default.languageCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.languageCode);
            InitializeComponent();
            DataContext = new HomeViewModel(frame);
        }
    }
}

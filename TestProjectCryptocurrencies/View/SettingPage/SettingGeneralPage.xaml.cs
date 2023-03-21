using System;
using System.Collections.Generic;
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
using TestProjectCryptocurrencies.Properties;

namespace TestProjectCryptocurrencies.View.SettingPage
{
    /// <summary>
    /// Interaction logic for SettingGeneralPage.xaml
    /// </summary>
    public partial class SettingGeneralPage : Page
    {
        public SettingGeneralPage()
        {
            InitializeComponent();
            //SetThemes();
        }
        private void SetThemes()
        {
            ResourceDictionary myResourceDictionary = new ResourceDictionary();
            if (Properties.Settings.Default.Themes == "light")
            {
                myResourceDictionary.Source = new Uri("/Properties/Themes/light.xaml", UriKind.RelativeOrAbsolute);
            }
            else
            {
                myResourceDictionary.Source = new Uri("/Properties/Themes/dark.xaml", UriKind.RelativeOrAbsolute);
            }
            this.Resources.MergedDictionaries.Add(myResourceDictionary);
        }
    }
}

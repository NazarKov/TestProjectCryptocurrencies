using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestProjectCryptocurrencies.Properties;

namespace TestProjectCryptocurrencies.ViewModel.SettingPage
{
    internal class SettingGeneralViewModel : ViewModel<SettingGeneralViewModel>
    {
        private ICommand _changeLocalizationComman;
        public SettingGeneralViewModel() 
        {

            _changeLocalizationComman = new DelegateCommand(ChangeLocalization);
            _selectedIndex = 0;
            _languageList = new List<string>();

            LanguageList.Add("Англійська");
            LanguageList.Add("Українька");
            
            if (Properties.Settings.Default.languageCode == "es")
            {
                SelectedIdex = 0;
            }
            else
            {
                SelectedIdex = 1;
            }
        }

        private int _selectedIndex;
        public int SelectedIdex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; OnPropertyChanged("SelectedIdex"); }
        }

        private List<string> _languageList;
        public List<string> LanguageList
        {
            get { return _languageList; }
            set { _languageList = value; OnPropertyChanged("LanguageList"); }
        }

        public ICommand ChangeLocalizationComman => _changeLocalizationComman;
        private void ChangeLocalization()
        {
            switch(SelectedIdex)
            {
                case 0:
                    {

                        Thread.CurrentThread.CurrentCulture = new CultureInfo("es");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
                        Properties.Settings.Default.languageCode = "es";
                        Properties.Settings.Default.Save();
                        
                        break;
                    }
                case 1:
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("uk-UA");
                        Properties.Settings.Default.languageCode = "uk-UA";
                        Properties.Settings.Default.Save();
                        break;
                    }
            }
        }
    }
}

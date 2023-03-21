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
        private ICommand _changeThemesComman;
        public SettingGeneralViewModel() 
        {

            _changeLocalizationComman = new DelegateCommand(ChangeLocalization);
            _changeThemesComman = new DelegateCommand(ChangeThemes);
            
            _selectedIndex = 0;
            _selectedIndexThemes = 0;

            SetField();
        }
        private void SetField()
        {
            if (Properties.Settings.Default.languageCode == "es")
            {
                SelectedIdex = 0;
            }
            else
            {
                SelectedIdex = 1;
            }
            if (Properties.Settings.Default.languageCode == "light")
            {
                SelectedIndexThemes = 0;
            }
            else
            {
                SelectedIndexThemes = 1;
            }
        }


        private int _selectedIndexThemes;
        public int SelectedIndexThemes
        {
            get { return _selectedIndexThemes; }
            set { _selectedIndexThemes = value;OnPropertyChanged("SelectedIndexThemes"); }
        }

        private int _selectedIndex;
        public int SelectedIdex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; OnPropertyChanged("SelectedIdex"); }
        }


        public ICommand ChangeLocalizationComman => _changeLocalizationComman;
        private void ChangeLocalization()
        {
            switch(SelectedIdex)
            {
                case 0:
                    {
                        Properties.Settings.Default.languageCode = "es";
                        Properties.Settings.Default.Save();
                        MessageBox.Show("To change the language, restart the program", "Inform",MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case 1:
                    {
                        Properties.Settings.Default.languageCode = "uk-UA";
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Для зміни мови перезагрузіть програму", "Inform", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
            }
        }
        public ICommand ChangeThemesComman => _changeThemesComman;
        private void ChangeThemes()
        {
            switch (SelectedIndexThemes)
            {
                case 0:
                    {
                        Properties.Settings.Default.Themes = "light";
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Для зміни теми перезагрузіть програму", "Inform", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case 1:
                    {
                        Properties.Settings.Default.Themes = "dark";
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Для зміни теми перезагрузіть програму", "Inform", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
            }
        }
    }
}

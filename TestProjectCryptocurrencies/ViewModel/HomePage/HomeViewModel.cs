using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TestProjectCryptocurrencies.Model;
using TestProjectCryptocurrencies.View.InfomationPage;
using TestProjectCryptocurrencies.View.SettingPage;

namespace TestProjectCryptocurrencies.ViewModel.HomePage
{
    internal class HomeViewModel : ViewModel<HomeViewModel>
    {
        private ICommand _openSettingCommand;

        public HomeViewModel(Frame frame)
        {
            _openSettingCommand = new DelegateCommand(()=>{ new SettingWindow().ShowDialog(); });

            Navigation.Init(frame);
            Navigation.NavigationServise.OpenMainPage();
            SetThemes();
        }
        private void SetThemes()
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            var dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("/Properties/Themes/" + Properties.Settings.Default.Themes.ToString() + ".xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        public ICommand OpenSettingCommand => _openSettingCommand;

        public ICommand ExitWindow { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        public ICommand RedirectWebCiteCommand { get => new DelegateParameterCommand(RedirectWebCite, CanRegister); }
        private void RedirectWebCite(object parameter)
        {
            Process.Start(new ProcessStartInfo("https://github.com/NazarKov/TestProjectCryptocurrencies") { UseShellExecute = true });
        }
        private bool CanRegister(object parameter) => true;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestProjectCryptocurrencies.Model.Interface;
using TestProjectCryptocurrencies.View.InfomationPage;

namespace TestProjectCryptocurrencies.Model
{
    internal class NavigationServise : INavigationServise
    {
        private Frame _frame;
        public NavigationServise(Frame frame)
        {
            _frame = frame;
        }

        public void OpenCoinInformationPage()
        {
            _frame.Navigate( new CoinInformationPage());
        }

        public void OpenMainPage()
        {
            _frame.Navigate(new MainPage());
        }
    }
}

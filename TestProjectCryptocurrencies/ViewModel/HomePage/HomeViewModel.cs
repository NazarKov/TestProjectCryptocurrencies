using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TestProjectCryptocurrencies.Model;
using TestProjectCryptocurrencies.Model.HomePage;
using TestProjectCryptocurrencies.View.InfomationPage;

namespace TestProjectCryptocurrencies.ViewModel.HomePage
{
    internal class HomeViewModel : ViewModel<HomeViewModel>
    {
        public HomeViewModel(Frame frame) 
        {

            Navigation.Init(frame);
            Navigation.NavigationServise.OpenMainPage();
           
        }
    }
}

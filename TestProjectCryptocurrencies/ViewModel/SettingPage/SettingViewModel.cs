using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TestProjectCryptocurrencies.View.SettingPage;

namespace TestProjectCryptocurrencies.ViewModel.SettingPage
{
    internal class SettingViewModel : ViewModel<SettingViewModel>
    {
        private ICommand _openGenarelSettingCommand;


        public SettingViewModel()
        {
            

            _openGenarelSettingCommand = new DelegateCommand(() => { Pages = new SettingGeneralPage(); });

        }

        public ICommand OpenGeneralSettingCommand => _openGenarelSettingCommand;

        private Page _pages;
        public Page Pages
        {
            get { return _pages; }
            set { _pages = value; OnPropertyChanged("Pages"); }
        }
    }
}

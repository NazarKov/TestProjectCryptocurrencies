using ShopProject.Model.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TestProjectCryptocurrencies.Model;
using TestProjectCryptocurrencies.Model.InformationPage;

namespace TestProjectCryptocurrencies.ViewModel.InformationPage
{
    internal class MainViewModel : ViewModel<MainViewModel>
    {
        private MainModel _model;

        private ICommand _updateCountShowDataGrid;
        private ICommand _searchCommand;

        public MainViewModel() 
        {

            _model = new MainModel();
            _listCoints = new List<Coin>();
           
            _coutShowDataGrid = new List<string>();
            _selectedItemComboCox = string.Empty;
            _searchItem = string.Empty;

            _updateCountShowDataGrid = new DelegateCommand(UpdateCount);
            _searchCommand = new DelegateCommand(Search);

            CountShowDataGrid.Add("10");
            CountShowDataGrid.Add("15");
            CountShowDataGrid.Add("20");
            CountShowDataGrid.Add("30");
            CountShowDataGrid.Add("50");
            CountShowDataGrid.Add("100");

            new Thread(new ThreadStart(GetResponse)).Start();
        }

        private List<Coin> _listCoints;
        public List<Coin> ListCoints
        {
            get { return _listCoints; }
            set { _listCoints = value; OnPropertyChanged("ListCoints"); }
        }

        private List<string> _coutShowDataGrid;
        public List<string> CountShowDataGrid
        {
            get { return _coutShowDataGrid; }
            set { _coutShowDataGrid = value; OnPropertyChanged("CountShowDataGrid"); }
        }

        private string _selectedItemComboCox;
        public string SelectedItemComboCox
        {
            get { return _selectedItemComboCox; }
            set { _selectedItemComboCox = value; OnPropertyChanged("SelectedItemComboCox"); }
        }

        private string _searchItem;
        public string SearchItem
        {
            get { return _searchItem; }
            set { _searchItem = value; OnPropertyChanged("SearchItem"); }
        }


        private void GetResponse()
        {
            ListCoints = _model.GetList(10);
        }

        public ICommand UpdateCountShowDataGrid => _updateCountShowDataGrid;
        private void UpdateCount()
        {
            ListCoints = _model.GetList(Convert.ToInt32(SelectedItemComboCox));
        }

        public ICommand SearchCommand => _searchCommand;

        private void Search()
        {
            ListCoints = _model.GetItem(_searchItem);
        }

        public ICommand RedirectWebCiteCommand { get => new DelegateParameterCommand(RedirectWebCite, CanRegister); }
        private void RedirectWebCite(object parameter)
        {
            Process.Start(new ProcessStartInfo(((Coin)parameter).explorer) { UseShellExecute = true });
        }
        public ICommand OpenCoinInformanionComman { get => new DelegateParameterCommand(OpenCoinInformanion, CanRegister); }
        private void OpenCoinInformanion(object parameter)
        {
            StaticResourse.Coin = ((Coin)parameter);
            StaticResourse.CoinList = ListCoints;
            Navigation.NavigationServise.OpenCoinInformationPage();
        }
        private bool CanRegister(object parameter) => true;
    }

}

using LiveCharts;
using LiveCharts.Wpf;
using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;
using TestProjectCryptocurrencies.Model;
using TestProjectCryptocurrencies.Model.InformationPage;
using TestProjectCryptocurrencies.View.InfomationPage;
using LineSeries = LiveCharts.Wpf.LineSeries;

namespace TestProjectCryptocurrencies.ViewModel.InformationPage
{
    internal class CoinInformationViewModel : ViewModel<CoinInformationViewModel>
    {
        private CoinInformationModel _model;
        
        private IFormatProvider _formatter;

        private ICommand _returnInMainPage;
        private ICommand _showOneDaysInScheduleCommand;
        private ICommand _showOSevenDaysInScheduleCommand;
        private ICommand _showOneMounthInScheduleCommand;
        private ICommand _showThreeMounthInScheduleCommand;
        private ICommand _showOneYearInScheduleCommand;
        private ICommand _returnCalulatorResultCommand;

        public CoinInformationViewModel()
        {
            _model = new CoinInformationModel();

            _formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            _coinPrices = new List<DataSchedule>();
            _priceCoin = string.Empty;
            _nameCoin = string.Empty;
            _dates = new List<string>();
            _coinMarkers = new List<CoinMarker>();
            _calculatorValue = string.Empty;
            _calculatorValue = "1";

            _returnInMainPage = new DelegateCommand(() => { Navigation.NavigationServise.OpenMainPage(); });
            _showOneDaysInScheduleCommand = new DelegateCommand(() => { SetSchedule(TypeInterval.OneDay); });
            _showOSevenDaysInScheduleCommand = new DelegateCommand(() => { SetSchedule(TypeInterval.SevenDays);  });
            _showOneMounthInScheduleCommand = new DelegateCommand(() => { SetSchedule(TypeInterval.OneMounth); });
            _showThreeMounthInScheduleCommand = new DelegateCommand(() => { SetSchedule(TypeInterval.ThreeMounth); });
            _showOneYearInScheduleCommand = new DelegateCommand(() => { SetSchedule(TypeInterval.OneYear);  });
            _returnCalulatorResultCommand = new DelegateCommand(ReturnCalulatorResult);

            Height = Application.Current.MainWindow.Height;
            Width = Application.Current.MainWindow.Width;

            

            DataSeries = new SeriesCollection();
            DataSeries.Add(new LineSeries() { PointGeometrySize = 0});
            SetSchedule(TypeInterval.OneYear);
            SetField();
            ReturnCalulatorResult();
        }
        private void SetSchedule(TypeInterval type)
        {
            CoinPrices = new List<DataSchedule>();
            var items = _model.GetDate(type);
            if (items != null)
            {
                foreach (CoinHistory item in items.Data.ToList())
                {
                    double result = double.Parse(item.priceUsd, _formatter);
                    CoinPrices.Add(new DataSchedule() { Date = (DateTime)item.Date, Price = result });

                }
            }

            DataSeries[0].Values = (new LineSeries (){ Values = new ChartValues<double>(CoinPrices.Select(d => d.Price)) }).Values;
            Dates = CoinPrices.Select(d => d.Date.ToString("dd.MM.yyyy")).ToList();

        }  
        private void SetField()
        {
            NameCoin = StaticResourse.Coin.name;
            PriceCoin = "$ " +double.Parse(StaticResourse.Coin.priceUsd,_formatter);

            var result = _model.GetMarkets().ToList();
            if(result != null)
            {
                CoinMarkers.AddRange(result);
            }
        }

        private List<CoinMarker> _coinMarkers;
        public List<CoinMarker> CoinMarkers
        {
            get { return _coinMarkers; }
            set { _coinMarkers = value; OnPropertyChanged("CoinMarkers"); }

        }

        private List<string> _dates;
        public List<string> Dates
        {
            get { return _dates; }
            set { _dates = value; OnPropertyChanged("Dates"); }
        }

        private SeriesCollection _dataSeries;
        public SeriesCollection DataSeries
        {
            get { return _dataSeries; }
            set { _dataSeries = value; OnPropertyChanged("DataSeries"); }
        }

        private string _calculatorValue;
        public string CalculatorValues
        {
            get { return _calculatorValue; }
            set { _calculatorValue = value; OnPropertyChanged("CalculatorName"); }
        }
        private string _calculatorResult;
        public string CalculatorResult
        {
            get { return _calculatorResult; }
            set { _calculatorResult = value; OnPropertyChanged("CalculatorResult"); }
        }

        private string _priceCoin;
        public string PriceCoin
        {
            get { return _priceCoin; }
            set { _priceCoin = value; }
        }

        private string _nameCoin;
        public string NameCoin
        {
            get { return _nameCoin; }
            set { _nameCoin = value; }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value;OnPropertyChanged("Width"); }
        }
        private double _height;
        public double Height
        {
            get { return _height; }
            set { _height = value;OnPropertyChanged("Height"); }
        }

        private List<DataSchedule> _coinPrices;
        public List<DataSchedule> CoinPrices
        {
            get { return _coinPrices; }
            set { _coinPrices = value; OnPropertyChanged("CoinPrices");}
        }

        public ICommand ReturnInMainPage => _returnInMainPage;
        public ICommand ShowOneDaysInScheduleCommand => _showOneDaysInScheduleCommand;
        public ICommand ShowSevenDaysInScheduleCommand => _showOSevenDaysInScheduleCommand;
        public ICommand ShowOneMounthInScheduleCommand => _showOneMounthInScheduleCommand;
        public ICommand ShowThreeMounthInScheduleCommand => _showThreeMounthInScheduleCommand;
        public ICommand ShowOneYearInScheduleCommand => _showOneYearInScheduleCommand;

        public ICommand RedirectWebCiteCommand { get => new DelegateParameterCommand(RedirectWebCite, CanRegister); }
        private void RedirectWebCite(object parameter)
        {
            Process.Start(new ProcessStartInfo("https://"+((CoinMarker)parameter).exchangeId + ".com") { UseShellExecute = true });


        }
        private bool CanRegister(object parameter) => true;

        public ICommand ReturnCalulatorResultCommand => _returnCalulatorResultCommand;
        private void ReturnCalulatorResult()
        {
            if(CalculatorValues!=string.Empty)
                CalculatorResult = CalculatorValues + " " + StaticResourse.Coin.symbol + " = USD $" + (double.Parse(CalculatorValues.ToString()) *double.Parse(StaticResourse.Coin.priceUsd.ToString(),_formatter));
        }
    }

}

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
        private ICommand _returnConvertedResultCommand;
        private ICommand _returnConvertedValueCommand;

        public CoinInformationViewModel()
        {
            _model = new CoinInformationModel();

            _formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            _dataSeries = new SeriesCollection();
            _coinPrices = new List<DataSchedule>();
            _coinMarkers = new List<CoinMarker>();
            _coinNameList = new List<string>();
            _dates = new List<string>();
            _priceCoin = string.Empty;
            _nameCoin = string.Empty;
            _calculatorValue = string.Empty;
            _selectedValueComboBox = string.Empty;
            _calculatorResult = string.Empty;

            _returnInMainPage = new DelegateCommand(() => { Navigation.NavigationServise.OpenMainPage(); });
            _showOneDaysInScheduleCommand = new DelegateCommand(() => { SetSchedule(TypeInterval.OneDay); });
            _showOSevenDaysInScheduleCommand = new DelegateCommand(() => { SetSchedule(TypeInterval.SevenDays);  });
            _showOneMounthInScheduleCommand = new DelegateCommand(() => { SetSchedule(TypeInterval.OneMounth); });
            _showThreeMounthInScheduleCommand = new DelegateCommand(() => { SetSchedule(TypeInterval.ThreeMounth); });
            _showOneYearInScheduleCommand = new DelegateCommand(() => { SetSchedule(TypeInterval.OneYear);  });
            _returnCalulatorResultCommand = new DelegateCommand(ReturnCalulatorResult);
            _returnConvertedResultCommand = new DelegateCommand(ReturnConvertorResult);
            _returnConvertedValueCommand = new DelegateCommand(ReturnConvertorValue);

            _calculatorValue = "1";
            _converterResult = 0;
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
            CoinNameList = StaticResourse.CoinList.Select(item => item.name).ToList();
            SelectedValueComboBox = StaticResourse.CoinList.Where(item =>item.name!=StaticResourse.Coin.name.ToString()).Select(item => item.name).ToList()[0];
            PriceCoin = "$ " +double.Parse(StaticResourse.Coin.priceUsd,_formatter);

            var result = _model.GetMarkets().ToList();
            if(result != null)
            {
                CoinMarkers.AddRange(result);
            }
        }
       
        private double _converterResult;
        public double ConverterResult
        {
            get { return _converterResult; }
            set { _converterResult = value; OnPropertyChanged("ConverterResult"); }
        }
        
        private double _converterValue;
        public double ConverterValue
        {
            get { return _converterValue; }
            set { _converterValue = value; OnPropertyChanged("ConverterValue"); }
        }
        
        private string _selectedValueComboBox;
        public string SelectedValueComboBox
        {
            get { return _selectedValueComboBox; }
            set { _selectedValueComboBox = value; OnPropertyChanged("SelectedValueComboBox"); }
        }
       
        private List<string> _coinNameList;
        public List<string> CoinNameList
        {
            get { return _coinNameList; }
            set { _coinNameList = value; OnPropertyChanged("CoinNameList"); }
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
        public ICommand ReturnCalulatorResultCommand => _returnCalulatorResultCommand;
        private void ReturnCalulatorResult()
        {
            if(CalculatorValues!=string.Empty)
                CalculatorResult = CalculatorValues + " " + StaticResourse.Coin.symbol + " = USD $" + (double.Parse(CalculatorValues.ToString()) *double.Parse(StaticResourse.Coin.priceUsd.ToString(),_formatter));
        }
        public ICommand ReturnConvertedResultCommand => _returnConvertedResultCommand;
        private void ReturnConvertorResult()
        {
            ConverterResult = (double.Parse(StaticResourse.Coin.priceUsd, _formatter) * ConverterValue) / double.Parse(StaticResourse.CoinList.Where(item => item.name == SelectedValueComboBox.ToString()).ToList()[0].priceUsd,_formatter);
        }
        public ICommand ReturnConvertedValueCommand => _returnConvertedValueCommand;
        private void ReturnConvertorValue()
        {
            ConverterValue = (double.Parse(StaticResourse.CoinList.Where(item => item.name == SelectedValueComboBox.ToString()).ToList()[0].priceUsd, _formatter) * ConverterResult) / double.Parse(StaticResourse.Coin.priceUsd, _formatter);                   
        }

        private bool CanRegister(object parameter) => true;
    }

}

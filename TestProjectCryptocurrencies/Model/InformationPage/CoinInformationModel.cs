using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestProjectCryptocurrencies.ViewModel.InformationPage;

namespace TestProjectCryptocurrencies.Model.InformationPage
{
    public enum TypeInterval
    {
        OneDay = 1,
        SevenDays = 2,
        OneMounth = 3,
        ThreeMounth = 4,
        OneYear = 5
    }
    class CoinInformationModel
    {
        public CoinInformationModel() { }

        public CoinHistoryRoot GetDate(TypeInterval type)
        {
            try
            {
                var restult = HttpService.GetHistoriCoin(StaticResourse.Coin.name.ToLower().ToString(),type);
                if (restult == null)
                    throw new Exception("Null");
                return restult.Result;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }
        public List<CoinMarker> GetMarkets()
        {
            try
            {
                var restult = HttpService.GetCointsMarket(StaticResourse.Coin.name.ToLower().ToString());
                if (restult == null)
                    throw new Exception("Null");
                return restult.Result.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

    }
}

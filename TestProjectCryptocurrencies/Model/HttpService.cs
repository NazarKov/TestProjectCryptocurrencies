using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestProjectCryptocurrencies.Model.InformationPage;
using TestProjectCryptocurrencies.ViewModel.InformationPage;

namespace TestProjectCryptocurrencies.Model
{ 
    internal class HttpService
    {

        private static string? _urlApi;
        private static HttpClient? httpClient;

        public HttpService() 
        {
            httpClient = new HttpClient();
            _urlApi = "https://api.coincap.io/v2/assets";
        }


        public static Task<CointRoot?> GetCoints()
        {
            if (httpClient != null)
            {
                var response = httpClient.GetFromJsonAsync<CointRoot>(_urlApi);
                return response;
            }
            else
            {
                return null;
            }
        }
       
        public static Task<CoinMarkerRoot> GetCointsMarket(string nameCoin)
        {
            if(httpClient != null)
            {
                var result = httpClient.GetFromJsonAsync<CoinMarkerRoot>($"https://api.coincap.io/v2/assets/{nameCoin}/markets");
                return result;
            }
            else
            {
                return null;
            }
        }
        public static Task<CoinHistoryRoot> GetHistoriCoin(string nameCoin ,TypeInterval interval)
        {
            httpClient = new HttpClient();
            if (httpClient != null)
            {
                switch(interval)
                {
                    case TypeInterval.OneDay:
                        {
                            var result = httpClient.GetFromJsonAsync<CoinHistoryRoot>($"https://api.coincap.io/v2/assets/{nameCoin}/history?interval=m1");
                            return result;
                        }
                    case TypeInterval.SevenDays:
                        {
                            var result = httpClient.GetFromJsonAsync<CoinHistoryRoot>($"https://api.coincap.io/v2/assets/{nameCoin}/history?interval=m15");
                            return result;
                        }
                    case TypeInterval.OneMounth:
                        {
                            var result = httpClient.GetFromJsonAsync<CoinHistoryRoot>($"https://api.coincap.io/v2/assets/{nameCoin}/history?interval=m30"); 
                            return result;
                        }
                    case TypeInterval.ThreeMounth:
                        {
                            var result = httpClient.GetFromJsonAsync<CoinHistoryRoot>($"https://api.coincap.io/v2/assets/{nameCoin}/history?interval=h6"); 
                            return result;
                        }
                    case TypeInterval.OneYear:
                        {
                            var result = httpClient.GetFromJsonAsync<CoinHistoryRoot>($"https://api.coincap.io/v2/assets/{nameCoin}/history?interval=d1");
                            return result;
                        }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}

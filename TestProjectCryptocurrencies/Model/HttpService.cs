using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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


        public static Task<Root?> GetResponse()
        {
            if (httpClient != null)
            {
                var response = httpClient.GetFromJsonAsync<Root>(_urlApi);
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}

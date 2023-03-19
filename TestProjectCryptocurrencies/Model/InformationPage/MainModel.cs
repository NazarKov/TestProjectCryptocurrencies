using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestProjectCryptocurrencies.Model.InformationPage
{
    internal class MainModel
    {
        private HttpService _httpService;
        private List<Coin> _cointList;

        public MainModel() 
        {
            _httpService = new HttpService();
            _cointList = new List<Coin>();
        }

        public List<Coin> GetList(int count)
        {
            try
            {
                var item = HttpService.GetCoints();
                if (item == null)
                {
                    throw new Exception("Null");
                }
                else
                {
                    if(item.Result !=null)
                        _cointList = item.Result.data.Take(count).ToList();
                    return _cointList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Coin>();
            }
        }

        public List<Coin> GetItem(string searchItem)
        {
            try
            {
                List<Coin> resultSearh = new List<Coin>();
                foreach(Coin coin in _cointList)
                {
                    if(coin.name.ToLower().Contains(searchItem.ToLower()))
                        resultSearh.Add(coin);
                }
                if(resultSearh.Count== 0)
                {
                    foreach (Coin coin in _cointList)
                    {
                        if (coin.symbol.ToLower().Contains(searchItem.ToLower()))
                            resultSearh.Add(coin);
                    }
                }

                return resultSearh;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Coin>();
            }
        }
    }
}

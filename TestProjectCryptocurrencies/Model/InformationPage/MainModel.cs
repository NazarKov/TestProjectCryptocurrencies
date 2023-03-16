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
        private List<Datum> _datumList;

        public MainModel() 
        {
            _httpService = new HttpService();
            _datumList = new List<Datum>();
        }

        public List<Datum> GetList(int count)
        {
            try
            {
                var item = HttpService.GetResponse();
                if (item == null)
                {
                    throw new Exception("Null");
                }
                else
                {
                    if(item.Result !=null)
                        _datumList = item.Result.data.Take(count).ToList();
                    return _datumList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Datum>();
            }
        }

        public List<Datum> GetItem(string searchItem)
        {
            try
            {
                List<Datum> resultSearh = new List<Datum>();
                foreach(Datum datum in _datumList)
                {
                    if(datum.name.ToLower().Contains(searchItem.ToLower()))
                        resultSearh.Add(datum);
                }
                if(resultSearh.Count== 0)
                {
                    foreach (Datum datum in _datumList)
                    {
                        if (datum.symbol.ToLower().Contains(searchItem.ToLower()))
                            resultSearh.Add(datum);
                    }
                }

                return resultSearh;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Datum>();
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace TestProjectCryptocurrencies.Model
{
    public class CoinHistory
    {
        public string? priceUsd { get; set; }
        public object? time { get; set; }
        public DateTime? Date { get; set; }
    }
    public class CoinHistoryRoot
    {
        public List<CoinHistory> Data { get; set; }
        public long? timestamp { get; set; }
    }
}

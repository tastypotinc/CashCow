
#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion Usings

namespace Stocks10DMA.Entities
{
    public class StockPriceData
    {

        public string StockSymbol { get; set; }

        public string PriceDate { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal HighPrice { get; set; }

        public decimal LowPrice { get; set; }

        public decimal ClosePrice { get; set; }

        public decimal ShareVolume { get; set; }

        public decimal TradeVolume { get; set; }

        public decimal Turnover { get; set; }

        public decimal DMA10 { get; set; }

    }
}

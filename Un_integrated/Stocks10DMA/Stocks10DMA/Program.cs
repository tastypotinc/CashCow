using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Stocks10DMA
{
    class Program
    {
        #region Main
        public static void Main(string[] args)
        {
            Console.WriteLine("Reading WatchList ...");

            Stocks10DMA.Services.DMA10AnalyzerService analyzerService = new Services.DMA10AnalyzerService();
            analyzerService.Analyze();

            Console.WriteLine("... Done Reading WatchList");
            Console.ReadKey();
        }
        #endregion Main
    }
}

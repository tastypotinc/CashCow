using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace StocksSwingPointMarker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Read all active entries in the Watch List
            var watchList = TestGetWatchList();

            // Perform the following Tasks in parallel:
            Parallel.ForEach(watchList, watchListRow => {
                
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("{0}|BSE:{1}|NSE:{2}", watchListRow.Name, watchListRow.BSESymbol, watchListRow.NSESymbol);
                
                // Step 2: Download data from BSE
                var bseDownloadUrl = GetBSEDownloadUrl(watchListRow.BSESymbol);
                var destinationFilePath = string.Format(@"../../Data/Downloads/{0}.csv", watchListRow.BSESymbol);
                var isSuccessfullyDownloaded = TestDownloadStockDataFromBSE(bseDownloadUrl, destinationFilePath);

                if (isSuccessfullyDownloaded == true) {

                    Console.WriteLine("* Done downloading Stock Price data.");
                    
                    // Step 3: Read the downloaded data
                    var excelSheetTable = TestReadExcelSheet(watchListRow.BSESymbol);
                    Console.WriteLine("* Done reading file.");

                    // Step 4: Mark the Swing Point Highs and Swing Point Lows
                    var stockPriceListWithSP = TestMarkSwingPoints(excelSheetTable);
                    Console.WriteLine("* Done marking Swing Points.");

                    // Step 5: Print results to screen
                    // PrintStockPriceDetails(stockPriceListWithSP);
                    var isSuccessfullyWritten = WriteStockPriceDetailsToFile(watchListRow.BSESymbol, stockPriceListWithSP);
                    Console.WriteLine("* Done writing output file.");
                }

                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            });
            
            Console.ReadLine();
        }

        #region GetBSEDownloadUrl

        private static string GetBSEDownloadUrl(string bseSymbol)
        {
            DateTime fromDate = DateTime.Now.AddYears(-1);
            DateTime toDate = DateTime.Now;

            string bseDownloadUrl = @"http://www.bseindia.com/stockinfo/stockprc2_excel.aspx?scripcd={0}&FromDate={1}&ToDate={2}&OldDMY=D";
            string strFromDate = string.Format("{0:MM/dd/yyyy}", fromDate);
            string strToDate = string.Format("{0:MM/dd/yyyy}", toDate);

            bseDownloadUrl = string.Format(bseDownloadUrl, bseSymbol.Trim(), strFromDate.Trim(), strToDate.Trim());

            return bseDownloadUrl;
        }

        #endregion GetBSEDownloadUrl

        private static List<dynamic> TestGetWatchList()
        {
            var input = new GetWatchListInput();
            var output = new GetWatchList().Execute(input);
            return output.WatchList;
        }

        private static bool TestDownloadStockDataFromBSE(string bseDownloadUrl, string destinationFilePath)
        {
            var input = new DownloadStockDataFromBSEInput();
            input.BSEDownloadUrl = bseDownloadUrl;
            input.DestinationFilePath = destinationFilePath;

            var output = new DownloadStockDataFromBSE().Execute(input);

            return output.IsSuccessfullyDownloaded;
        }

        private static DataTable TestReadExcelSheet(string filename)
        {
            // string filename = "500010";
            string fullFilePath = string.Format(@"..\..\Data\Downloads\{0}.csv", filename);

            ReadExcelSheetInput input = new ReadExcelSheetInput();
            input.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Mode=ReadWrite;Extended Properties=""text;HDR=Yes;FMT=Delimited"";";
            input.SelectCommand = @"SELECT * FROM [{0}.csv]";

            input.ConnectionString = string.Format(input.ConnectionString, fullFilePath.Substring(0, fullFilePath.LastIndexOf('\\')));
            input.SelectCommand = string.Format(input.SelectCommand, filename);

            // Console.WriteLine("+++ Connection String:");
            // Console.WriteLine(input.ConnectionString);

            // Console.WriteLine("+++ Select Command:");
            // Console.WriteLine(input.SelectCommand);

            // Console.WriteLine("***************************************************");
            // Console.WriteLine("BSE: {0}", filename);
            // Console.WriteLine("***************************************************");

            var output = new ReadExcelSheetOutput();
            output = new ReadExcelSheet().Execute(input);

            return output.ExcelSheetTable;
        }

        private static List<dynamic> TestMarkSwingPoints(DataTable excelSheetTable)
        {
            MarkSwingPointsInput input = new MarkSwingPointsInput();
            input.StockPriceDataTable = excelSheetTable;

            var output = new MarkSwingPointsOutput();
            output = new MarkSwingPoints().Execute(input);

            return output.StockPriceListWithSP;
        }


        private static bool WriteStockPriceDetailsToFile(string filename, List<dynamic> stockPriceList)
        {
            var outputFilePath = string.Format(@"../../Data/Output/{0}.csv", filename);
            var input = new WriteOutputStockPriceCsvFileInput();
            input.FilePath = outputFilePath;
            input.HeadersList = new List<dynamic>(); // TODO: Send the list of Headers
            input.DetailsList = stockPriceList;
            var output = new WriteOutputStockPriceCsvFile().Execute(input);
            return output.IsSuccessfullyWritten;
        }

        private static void PrintStockPriceDetails(List<dynamic> stockPriceList)
        {
            var headerLine = string.Format("{0}\t\t{1}\t{2}\t{3}\t{4}\t{5, 10}\t{6}", "Date", "Open", "High", "Low", "Close", "Volume", "SwingPoint");
            Console.WriteLine(headerLine);

            foreach (var stockPriceRow in stockPriceList)
            {
                var detailLine = string.Format("{0:yyyyMMdd}\t{1:0.00}\t{2:0.00}\t{3:0.00}\t{4:0.00}\t{5, 10}\t{6}", stockPriceRow.PriceDate.ToString("yyyyMMdd"), stockPriceRow.OpenPrice, stockPriceRow.HighPrice, stockPriceRow.LowPrice, stockPriceRow.ClosePrice, stockPriceRow.ShareVolume, stockPriceRow.SwingPoint);
                Console.WriteLine(detailLine);
            }
        }
    }
}

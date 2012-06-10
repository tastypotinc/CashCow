
#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stocks10DMA.Repositories;
using Stocks10DMA.Entities;
#endregion Usings

namespace Stocks10DMA.Services
{
    public class DMA10AnalyzerService
    {
        #region Data Members
        private DateTime fromDate = DateTime.Now.AddMonths(-3);
        private DateTime toDate = DateTime.Now;
        private string targetFile = @"../../Data/Downloads/{0}.csv";
        #endregion Data Members

        #region Collaborators
        private readonly IWatchListRepository watchListRepository = null;
        private readonly IFileDownloadService fileDownloadService = null;
        private IStockPriceDataRepository stockPriceDataRepository = null;
        #endregion Collaborators

        #region Constructors
        
        public DMA10AnalyzerService()
        {
            this.watchListRepository = new WatchListRepository();
            this.fileDownloadService = new FileDownloadService();
            this.stockPriceDataRepository = new StockPriceDataRepository();
        }

        public DMA10AnalyzerService(IWatchListRepository watchListRepository, IFileDownloadService fileDownloadService, IStockPriceDataRepository stockPriceDataRepository)
        {
            this.watchListRepository = watchListRepository;
            this.fileDownloadService = fileDownloadService;
            this.stockPriceDataRepository = stockPriceDataRepository;
        }

        #endregion Constructors

        #region Analyze
        public void Analyze()
        {
            // Get Active WatchList Entries
            var activeWatchListEntries = from wle in this.watchListRepository.GetAll()
                                         where wle.Active == 1
                                         select wle;

            string downloadFromUrl = string.Empty;
            string downloadTo = string.Empty;
            foreach (WatchListEntry wle in activeWatchListEntries)
            {
                if (!string.IsNullOrEmpty(wle.BSESymbol))
                {
                    downloadFromUrl = this.GetBSEDownloadUrl(wle);
                    downloadTo = string.Format(this.targetFile, wle.BSESymbol.Trim());
                }
                else if (!string.IsNullOrEmpty(wle.NSESymbol))
                {
                    downloadFromUrl = this.GetNSEDownloadUrl(wle);
                    downloadTo = string.Format(this.targetFile, wle.NSESymbol.Trim());
                }

                // Download Price data:
                this.fileDownloadService.DownloadFile(downloadFromUrl, downloadTo);

                // Read data from the downloaded file and calculate the 10 DMA:
                this.stockPriceDataRepository.DataSource = downloadTo;
                this.stockPriceDataRepository.FileName = wle.BSESymbol.Trim();
                var stockPriceData = this.stockPriceDataRepository.GetPriceData();

                // Check if 10DMA Pattern is found in the Stock Price:
                this.Is10DMAPatternPresent(stockPriceData);

            }

            // Foreach WatchListEntry, 
            // a. Download 3 year data
            // b. Analyze data for 10 DMA pattern
            // c. Log the entry with attributes

        }
        #endregion Analyze

        #region GetBSEDownloadUrl
        private string GetBSEDownloadUrl(WatchListEntry wle)
        {
            string bseDownloadUrl = @"http://www.bseindia.com/stockinfo/stockprc2_excel.aspx?scripcd={0}&FromDate={1}&ToDate={2}&OldDMY=D";
            string strFromDate = string.Format("{0:MM/dd/yyyy}", fromDate);
            string strToDate = string.Format("{0:MM/dd/yyyy}", toDate);

            bseDownloadUrl = string.Format(bseDownloadUrl, wle.BSESymbol.Trim(), strFromDate.Trim(), strToDate.Trim());

            return bseDownloadUrl;
        }
        #endregion GetBSEDownloadUrl

        #region GetNSEDownloadUrl
        private string GetNSEDownloadUrl(WatchListEntry wle)
        {
            string nseDownloadUrl = @"http://www.nseindia.com/content/equities/scripvol/datafiles/{0}-TO-{1}{2}EQN.csv";
            string strFromDate = string.Format("{0:dd-MM-yyyy}", fromDate);
            string strToDate = string.Format("{0:dd-MM-yyyy}", toDate);

            nseDownloadUrl = string.Format(nseDownloadUrl, strFromDate.Trim(), strToDate.Trim(), wle.NSESymbol.Trim());

            return nseDownloadUrl;
        }
        #endregion GetNSEDownloadUrl

        #region Is10DMAPatternPresent
        public bool Is10DMAPatternPresent(IQueryable<StockPriceData> stockPriceData)
        {
            stockPriceData = from spd in stockPriceData
                             orderby spd.PriceDate descending
                             select spd;

            string stockName = string.Empty;
            decimal prevDMA = 0m;
            decimal maxDMA = 0m;
            decimal minDMA = 0m;
            int hitCount = 0;
            int missCount = 0;
            string endDate = System.DateTime.MaxValue.ToString();
            string startDate = System.DateTime.MinValue.ToString();

            foreach (StockPriceData entry in stockPriceData)
            {
                if (prevDMA <= 0)
                {
                    prevDMA = entry.DMA10;
                }
                else
                {
                    maxDMA = prevDMA + (prevDMA * (0.2m/100m));
                    minDMA = prevDMA - (prevDMA * (0.2m/100m));
                    if (entry.DMA10 >= minDMA && entry.DMA10 <= maxDMA)
                    {
                        if (hitCount == 0)
                        {
                            endDate = entry.PriceDate;
                            startDate = entry.PriceDate;
                        }
                        else
                        {
                            startDate = entry.PriceDate;
                        }
                        hitCount++;
                        // endDate = entry.PriceDate;
                    }
                    else
                    {
                        missCount++;
                        if (hitCount >= 3)
                        {
                            stockName = entry.StockSymbol;
                        }
                        break;
                        // startDate = entry.PriceDate;
                    }
                }
            }
            if (hitCount >= 3)
            {
                Console.WriteLine("***** Pattern Found! *****");
                Console.WriteLine("Stock: {0} \t\t Hits: {1}", stockName, hitCount);
                Console.WriteLine("From: {0} to {1}", startDate, endDate);
                Console.WriteLine("**************************");
            }
            return true;
        }
        #endregion Is10DMAPatternPresent

    }
}

#region Namespaces

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using ShowSwingPoint.Models;
using ShowSwingPoint.Repositories;

#endregion Namespaces

namespace ShowSwingPoint.Classes
{
    /// <summary>
    /// Main class to show swing point.
    /// </summary>
    public class SwingPointShowService
    {
        #region Collaborators

        private ISwingPointDataRepository _iSwingPointDataRepository = null;

        #endregion Collaborators

        #region Constructors

        public SwingPointShowService()
        {
            this._iSwingPointDataRepository = new SwingPointDataRepository();
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Method to to get list of WatchList entities.
        /// </summary>
        /// <param name="watchListFilePath">Path to the file containing watch list data in CSV format.</param>
        /// <returns>List of WatchList entities.</returns>
        public IQueryable<WatchListModel> GetWatchList(string watchListFilePath)
        {
            IQueryable<WatchListModel> watchList = null;

            var physicalPath = HttpContext.Current.Server.MapPath(watchListFilePath);

            if (File.Exists(physicalPath))
            {
                var dataFile = new FileInfo(physicalPath);

                // Read data from stock file.
                this._iSwingPointDataRepository.DataSource = dataFile.DirectoryName;
                this._iSwingPointDataRepository.FileName = dataFile.Name;
                watchList = this._iSwingPointDataRepository.GetWatchList();
            }

            return watchList;
        }

        /// <summary>
        /// Method to to get list of stock data.
        /// </summary>
        /// <param name="stockDataFilePath">Path to the file containing stock data in CSV format.</param>
        /// <returns>List of stock data.</returns>
        public IQueryable<StockDataModel> GetStockData(string stockDataFilePath)
        {
            IQueryable<StockDataModel> stockData = null;

            var physicalPath = HttpContext.Current.Server.MapPath(stockDataFilePath);

            if (File.Exists(physicalPath))
            {
                var dataFile = new FileInfo(physicalPath);

                // Read data from stock file.
                this._iSwingPointDataRepository.DataSource = dataFile.DirectoryName;
                this._iSwingPointDataRepository.FileName = dataFile.Name;
                stockData = this._iSwingPointDataRepository.GetStockData();
            }

            return stockData;
        }

        #endregion Public Methods
    }
}
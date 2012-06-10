#region Namespaces

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwingPointLocator.Entities;
using SwingPointLocator.Repositories;

#endregion Namespaces

namespace SwingPointLocator.Classes
{
    /// <summary>
    /// Main class for locating swing point.
    /// </summary>
    public class SwingPointLocatorService
    {
        #region Private Data

        // This is the matuarity time for a potential swing point to turn to actual swing point.
        // This should be configurable.
        private const int WAIT_TIME = 6;
        
        #endregion Private Data

        #region Collaborators

        private IStockPriceDataRepository _stockPriceDataRepository = null;

        #endregion Collaborators

        #region Constructors

        public SwingPointLocatorService()
        {
            this._stockPriceDataRepository = new StockPriceDataRepository();
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Method to locate swing points for all stock data present in form of CSV files in the mentioned input folder path.
        /// </summary>
        /// <param name="dataFolderPath">Path to the folder containing various stock data in CSV format.</param>
        /// <param name="swingPointFolderPath">Path to folder containing swing points for stocks.</param>
        /// <returns>Void.</returns>
        public void LocateSwingPoints(string dataFolderPath, string swingPointFolderPath)
        {
            // Iterate through each stock CSV file in data folder. Data in this CSV file should be stored in DB in later enhancements.
            if (Directory.Exists(dataFolderPath))
            {
                foreach (var dataFileName in Directory.GetFiles(dataFolderPath))
                {
                    var dataFile = new FileInfo(dataFileName);

                    // Read data from stock file.
                    this._stockPriceDataRepository.DataSource = dataFolderPath;
                    this._stockPriceDataRepository.FileName = dataFile.Name;
                    var stockPriceData = this._stockPriceDataRepository.GetPriceData();

                    // Get low and high swing points.
                    var lowSwingPoints = LocateLowSwingPoints(stockPriceData);
                    var highSwingPoints = LocateHighSwingPoints(stockPriceData);

                    // Write the above swing point data to a text file.
                    WriteSwingPointDataToFile(lowSwingPoints, highSwingPoints, swingPointFolderPath, dataFile.Name.Substring(0, dataFile.Name.IndexOf(".")));
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Method to calculate high swing points from list of stock data.
        /// </summary>
        /// <param name="stockPriceData">List of stock data in which high swing points has to be located.</param>
        /// <returns>List of stock data that are high swing points.</returns>
        private IQueryable<StockPriceData> LocateHighSwingPoints(IQueryable<StockPriceData> stockPriceData)
        {
            var waitTimeCounter = 1;
            var potentialHighSwingPointCounter = 0;
            var highSwingPoints = new List<StockPriceData>();

            // Iterate through each stock data starting the 2nd element.
            for (var loop = 1; loop < stockPriceData.Count(); loop++)
            {
                // If the current data is new potential high swing point.
                if (stockPriceData.ElementAt(loop).HighPrice > stockPriceData.ElementAt(potentialHighSwingPointCounter).HighPrice)
                {
                    // Reset waitTimeCounter.
                    waitTimeCounter = 1;

                    // Set potentialHighSwingPointCounter to current data index.
                    potentialHighSwingPointCounter = loop;
                }
                else
                {
                    // Increase the wait time counter.
                    waitTimeCounter++;

                    // Check if the potential swing point data has matured in terms of wait time.
                    if (waitTimeCounter == WAIT_TIME)
                    {
                        // Add the potential swing point to the actual swing point list.
                        highSwingPoints.Add(stockPriceData.ElementAt(potentialHighSwingPointCounter));

                        // Reset waitTimeCounter.
                        waitTimeCounter = 1;

                        // Set potentialLowSwingPointCounter to current data index.
                        potentialHighSwingPointCounter = loop;
                    }
                }
            }

            return highSwingPoints.AsQueryable();
        }

        /// <summary>
        /// Method to calculate low swing points from list of stock data.
        /// </summary>
        /// <param name="stockPriceData">List of stock data in which low swing points has to be located.</param>
        /// <returns>List of stock data that are low swing points.</returns>
        private IQueryable<StockPriceData> LocateLowSwingPoints(IQueryable<StockPriceData> stockPriceData)
        {
            var waitTimeCounter = 1;
            var potentialLowSwingPointCounter = 0;
            var lowSwingPoints = new List<StockPriceData>();

            // Iterate through each stock data starting the 2nd element.
            for (var loop = 1; loop < stockPriceData.Count(); loop++ )
            {
                // If the current data is new potential low swing point.
                if(stockPriceData.ElementAt(loop).LowPrice < stockPriceData.ElementAt(potentialLowSwingPointCounter).LowPrice)
                {
                    // Reset waitTimeCounter.
                    waitTimeCounter = 1;

                    // Set potentialLowSwingPointCounter to current data index.
                    potentialLowSwingPointCounter = loop;
                }
                else
                {
                    // Increase the wait time counter.
                    waitTimeCounter++;

                    // Check if the potential swing point data has matured in terms of wait time.
                    if(waitTimeCounter == WAIT_TIME)
                    {
                        // Add the potential swing point to the actual swing point list.
                        lowSwingPoints.Add(stockPriceData.ElementAt(potentialLowSwingPointCounter));
                        
                        // Reset waitTimeCounter.
                        waitTimeCounter = 1;

                        // Set potentialLowSwingPointCounter to current data index.
                        potentialLowSwingPointCounter = loop;
                    }
                }
            }

            return lowSwingPoints.AsQueryable();
        }

        /// <summary>
        /// Method to persist swing point data to a text file. If the text file exist then it is overwritten.
        /// </summary>
        /// <param name="lowSwingPoints">List of low swing points stock data.</param>
        /// <param name="highSwingPoints">List of high swing points stock data.</param>
        /// <param name="swingPointFolderPath">Folder path where swing point data files are to be created.</param>
        /// <param name="swingPointFileName">Name of the swing point text file.</param>
        private void WriteSwingPointDataToFile(IQueryable<StockPriceData> lowSwingPoints,
            IQueryable<StockPriceData> highSwingPoints,
            string swingPointFolderPath,
            string swingPointFileName)
        {
            var swingPointDataToWrite = new StringBuilder();

            // Build low swing point data.
            swingPointDataToWrite.AppendLine("Low Swing Point Dates:");
            swingPointDataToWrite.AppendLine("=====================");

            if (lowSwingPoints != null && lowSwingPoints.Count() > 0)
            {
                foreach (var lowSwingPointData in lowSwingPoints)
                {
                    swingPointDataToWrite.Append(lowSwingPointData.PriceDate + ", ");
                }
            }
            else
            {
                swingPointDataToWrite.AppendLine("No low swing data found.");
            }

            // Build low swing point data.
            swingPointDataToWrite.AppendLine(); swingPointDataToWrite.AppendLine(); swingPointDataToWrite.AppendLine();
            swingPointDataToWrite.AppendLine("High Swing Point Dates:");
            swingPointDataToWrite.AppendLine("=====================");

            if (highSwingPoints != null && highSwingPoints.Count() > 0)
            {
                foreach (var highSwingPointData in highSwingPoints)
                {
                    swingPointDataToWrite.Append(highSwingPointData.PriceDate + ", ");
                }
            }
            else
            {
                swingPointDataToWrite.AppendLine("No high swing data found.");
            }

            // Write the data to text file.
            var filePath = swingPointFolderPath + "/" + swingPointFileName + ".txt";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, swingPointDataToWrite.ToString());
        }

        #endregion Private Methods
    }
}

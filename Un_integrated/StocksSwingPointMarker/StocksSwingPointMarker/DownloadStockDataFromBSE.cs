using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace StocksSwingPointMarker
{
    public class DownloadStockDataFromBSEInput
    {
        #region Data Members

        public string BSEDownloadUrl { get; set; }
        public string DestinationFilePath { get; set; }

        #endregion Data Members

        #region Constructors
        
        public DownloadStockDataFromBSEInput()
        {
            BSEDownloadUrl = null;
            DestinationFilePath = null;
        }

        #endregion Constructors

        #region ValidateInput

        public void ValidateInput()
        {
            if (string.IsNullOrEmpty(BSEDownloadUrl)) {
                throw new ArgumentException("Argument is null or empty", "BSEDownloadUrl");
            }

            if (string.IsNullOrEmpty(DestinationFilePath)) {
                throw new ArgumentException("Argument is null or empty", "DestinationFilePath");
            }
        }

        #endregion ValidateInput
    }

    public class DownloadStockDataFromBSEOutput
    {
        #region Data Members

        public bool IsSuccessfullyDownloaded { get; set; }

        #endregion Data Members

        #region Constructors

        public DownloadStockDataFromBSEOutput()
        {
            IsSuccessfullyDownloaded = false;
        }

        #endregion Constructors
    }

    public class DownloadStockDataFromBSE
    {
        #region Data Members

        private DownloadStockDataFromBSEInput _input = null;
        private DownloadStockDataFromBSEOutput _output = null;

        #endregion Data Members

        #region Execute

        public DownloadStockDataFromBSEOutput Execute(DownloadStockDataFromBSEInput input)
        {
            // Assign and Validate Input
            _input = input;
            _input.ValidateInput();


            // Initialize Output
            _output = new DownloadStockDataFromBSEOutput();

            // Download the file
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(_input.BSEDownloadUrl, _input.DestinationFilePath);
                _output.IsSuccessfullyDownloaded = true;
            }

            return _output;
        }

        #endregion Execute
    }
}

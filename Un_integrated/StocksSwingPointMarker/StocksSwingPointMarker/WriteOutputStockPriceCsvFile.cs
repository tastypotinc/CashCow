using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StocksSwingPointMarker
{
    public class WriteOutputStockPriceCsvFileInput
    {
        #region Data Members

        public string FilePath { get; set; }
        public List<dynamic> HeadersList { get; set; }
        public List<dynamic> DetailsList { get; set; }

        #endregion Data Members

        #region Constructors

        public WriteOutputStockPriceCsvFileInput()
        {
            FilePath = null; 
            HeadersList = DetailsList = null;
        }

        #endregion Constructors

        #region ValidateInput

        public void ValidateInput()
        {
            if(string.IsNullOrEmpty(FilePath))
                throw new ArgumentException("Argument null or empty", "FilePath");

            if(HeadersList == null)
                throw new ArgumentException("Argument null", "HeadersList");

            if(DetailsList == null)
                throw new ArgumentException("Argument null", "DetailsList");
        }

        #endregion ValidateInput
    }

    public class WriteOutputStockPriceCsvFileOutput
    {
        #region Data Members

        public bool IsSuccessfullyWritten { get; set; }
        
        #endregion Data Members

        #region Constructors

        public WriteOutputStockPriceCsvFileOutput()
        {
            IsSuccessfullyWritten = false;
        }

        #endregion Constructors
    }

    public class WriteOutputStockPriceCsvFile
    {
        #region Data Members

        private WriteOutputStockPriceCsvFileInput _input = null;
        private WriteOutputStockPriceCsvFileOutput _output = null;

        #endregion Data Members

        #region Constructors

        public WriteOutputStockPriceCsvFile()
        {
            _output = new WriteOutputStockPriceCsvFileOutput();
        }

        #endregion Constructors

        #region Execute

        public WriteOutputStockPriceCsvFileOutput Execute(WriteOutputStockPriceCsvFileInput input)
        {
            // Assign and Validate Input
            _input = input;
            _input.ValidateInput();

            using (var fs = new FileStream(_input.FilePath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    var headerLine = string.Format("{0},{1},{2},{3},{4},{5, 10},{6}", "Date", "Open", "High", "Low", "Close", "Volume", "SwingPoint");
                    sw.WriteLine(headerLine);

                    foreach (var stockPriceRow in _input.DetailsList)
                    {
                        var detailLine = string.Format("{0:yyyyMMdd},{1:0.00},{2:0.00},{3:0.00},{4:0.00},{5, 10},{6}", stockPriceRow.PriceDate.ToString("yyyyMMdd"), stockPriceRow.OpenPrice, stockPriceRow.HighPrice, stockPriceRow.LowPrice, stockPriceRow.ClosePrice, stockPriceRow.ShareVolume, stockPriceRow.SwingPoint);
                        sw.WriteLine(detailLine);                        
                    }

                    _output.IsSuccessfullyWritten = true;
                }
            }

            return _output;
        }

        #endregion Execute
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace StocksSwingPointMarker
{
    public class MarkSwingPointsInput
    {
        #region Data Members

        public DataTable StockPriceDataTable { get; set; }

        #endregion Data Members

        #region Constructors
        
        public MarkSwingPointsInput()
        {

        }
        
        #endregion Constructors

        #region ValidateInput

        public void ValidateInput()
        {
        }

        #endregion ValidateInput
    }

    public class MarkSwingPointsOutput
    {
        #region Data Members

        public List<dynamic> StockPriceListWithSP { get; set; }

        #endregion Data Members

        #region Constructors
        
        public MarkSwingPointsOutput()
        {
            StockPriceListWithSP = new List<dynamic>();
        }

        #endregion Constructors
    }

    public class MarkSwingPoints
    {
        #region Data Members

        private MarkSwingPointsInput _input = null;
        private MarkSwingPointsOutput _output = null;

        private const int BAR_COUNT = 6;
        private const string SWING_POINT_LOW = "SPL";
        private const string SWING_POINT_HIGH = "SPH";

        #endregion Data Members

        #region Execute

        public MarkSwingPointsOutput Execute(MarkSwingPointsInput input)
        {
            // Assign and Validate Input
            _input = input;
            _input.ValidateInput();

            // Initialize Output
            _output = new MarkSwingPointsOutput();

            // Convert the Stock Price Data Table to a List
            var stockPriceList = _FormatDataTableAsList(_input.StockPriceDataTable);

            // Mark the Swing Point Lows in-line
            stockPriceList = _MarkSPL(stockPriceList);

            // Mark the Swing Point Highs in-line
            stockPriceList = _MarkSPH(stockPriceList);

            _output.StockPriceListWithSP = stockPriceList;
            return _output;
        }

        #endregion Execute

        #region _FormatDataTableAsList

        private List<dynamic> _FormatDataTableAsList(DataTable table)
        {
            var stockPriceList = new List<dynamic>();

            for (int i = 0; i < table.Rows.Count; ++i) {

                dynamic stockPriceRow = new ExpandoObject();

                // stockPriceRow.StockSymbol = this.FileName;
                stockPriceRow.PriceDate = Convert.ToDateTime(table.Rows[i]["Date"]);
                stockPriceRow.OpenPrice = Convert.ToDecimal(table.Rows[i]["Open Price"]);
                stockPriceRow.HighPrice = Convert.ToDecimal(table.Rows[i]["High Price"]);
                stockPriceRow.LowPrice = Convert.ToDecimal(table.Rows[i]["Low Price"]);
                stockPriceRow.ClosePrice = Convert.ToDecimal(table.Rows[i]["Close Price"]);
                stockPriceRow.ShareVolume = Convert.ToDecimal(table.Rows[i]["No#of Shares"]);
                stockPriceRow.TradeVolume = Convert.ToDecimal(table.Rows[i]["No# of Trades"]);
                stockPriceRow.Turnover = Convert.ToDecimal(table.Rows[i]["Total Turnover (Rs#)"]);
                stockPriceRow.SwingPoint = string.Empty;

                stockPriceList.Add(stockPriceRow);
            }
            return stockPriceList;
        }
        
        #endregion _FormatDataTableAsList

        #region _MarkSPL

        private List<dynamic> _MarkSPL(List<dynamic> stockPriceList)
        {
            var successiveLowsCounter = 1;
            dynamic potentialSPL = new ExpandoObject();
            dynamic actualizedSPL = new ExpandoObject();

            potentialSPL = stockPriceList[0];

            foreach (var bar in stockPriceList) {

                if (successiveLowsCounter > BAR_COUNT) {
                    actualizedSPL = potentialSPL;
                    successiveLowsCounter = 1;
                    actualizedSPL.SwingPoint = SWING_POINT_LOW;
                    potentialSPL = bar;
                } else {
                    if (bar.LowPrice < potentialSPL.LowPrice) {
                        potentialSPL = bar;
                        successiveLowsCounter = 1;
                    } else {
                        successiveLowsCounter++;
                    }
                }

            }

            return stockPriceList;
        }

        #endregion _MarkSPL

        #region _MarkSPH

        private List<dynamic> _MarkSPH(List<dynamic> stockPriceList)
        {
            var successiveHighsCounter = 1;
            dynamic potentialSPH = new ExpandoObject();
            dynamic actualizedSPH = new ExpandoObject();

            potentialSPH = stockPriceList[0];

            foreach (var bar in stockPriceList) {

                if (successiveHighsCounter > BAR_COUNT) {
                    actualizedSPH = potentialSPH;
                    successiveHighsCounter = 1;
                    actualizedSPH.SwingPoint = SWING_POINT_HIGH;
                    potentialSPH = bar;
                } else {
                    if (bar.HighPrice > potentialSPH.HighPrice) {
                        potentialSPH = bar;
                        successiveHighsCounter = 1;
                    } else {
                        successiveHighsCounter++;
                    }
                }

            }

            return stockPriceList;
        }

        #endregion _MarkSPH
    }
}

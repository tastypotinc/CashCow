using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace StocksSwingPointMarker
{
    public class GetWatchListInput
    {
        #region Data Members


        #endregion Data Members

        #region Constructors

        public GetWatchListInput()
        {

        }

        #endregion Constructors

        #region ValidateInput

        public void ValidateInput()
        {
        }

        #endregion ValidateInput
    }

    public class GetWatchListOutput
    {
        #region Data Members

        public List<dynamic> WatchList { get; set; }

        #endregion Data Members

        #region Constructors

        public GetWatchListOutput()
        {
            WatchList = new List<dynamic>();
        }

        #endregion Constructors
    }

    public class GetWatchList
    {
        #region Data Members

        private GetWatchListInput _input = null;
        private GetWatchListOutput _output = null;

        #endregion Data Members

        #region Execute

        public GetWatchListOutput Execute(GetWatchListInput input)
        {
            // Assign and Validate Input
            _input = input;
            _input.ValidateInput();

            // Initialize Output
            _output = new GetWatchListOutput();

            string filename = "WatchList";
            string fullFilePath = string.Format(@"..\..\Data\WatchList\{0}.csv", filename);

            var readExcelSheetInput = new ReadExcelSheetInput();
            readExcelSheetInput.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Mode=ReadWrite;Extended Properties=""text;HDR=Yes;FMT=Delimited"";";
            readExcelSheetInput.SelectCommand = @"SELECT * FROM [{0}.csv]";

            readExcelSheetInput.ConnectionString = string.Format(readExcelSheetInput.ConnectionString, fullFilePath.Substring(0, fullFilePath.LastIndexOf('\\')));
            readExcelSheetInput.SelectCommand = string.Format(readExcelSheetInput.SelectCommand, filename);

            var readExcelSheetOutput = new ReadExcelSheet().Execute(readExcelSheetInput);

            // Convert the Stock Price Data Table to a List
            var watchList = _FormatDataTableAsList(readExcelSheetOutput.ExcelSheetTable);

            _output.WatchList = watchList;
            return _output;
        }

        #endregion Execute

        #region _FormatDataTableAsList

        private List<dynamic> _FormatDataTableAsList(DataTable table)
        {
            var watchList = new List<dynamic>();

            for (int i = 0; i < table.Rows.Count; ++i) {

                dynamic watchListRow = new ExpandoObject();

                watchListRow.Id = Convert.ToInt32(table.Rows[i]["Id"]);
                watchListRow.BSESymbol = Convert.ToString(table.Rows[i]["BSESymbol"]);
                watchListRow.NSESymbol = Convert.ToString(table.Rows[i]["NSESymbol"]);
                watchListRow.Name = Convert.ToString(table.Rows[i]["Name"]);
                watchListRow.AltName1 = Convert.ToString(table.Rows[i]["AltName1"]);
                watchListRow.AltName2 = Convert.ToString(table.Rows[i]["AltName2"]);
                watchListRow.TempName = Convert.ToString(table.Rows[i]["TempName"]);
                watchListRow.Active = Convert.ToInt32(table.Rows[i]["Active"]);
                watchListRow.Alert = Convert.ToInt32(table.Rows[i]["Alert"]);
                watchListRow.ModifiedOn = Convert.ToInt32(table.Rows[i]["ModifiedOn"]);
                watchListRow.CreatedOn = Convert.ToInt32(table.Rows[i]["CreatedOn"]);

                watchList.Add(watchListRow);
            }
            
            return watchList;
        }

        #endregion _FormatDataTableAsList

    }
}

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Data;

namespace StocksSwingPointMarker
{
    public class ReadExcelSheetInput
    {
        #region Data Members

        public string ConnectionString { get; set; }
        public string SelectCommand { get; set; }

        #endregion Data Members

        #region Constructors

        public ReadExcelSheetInput()
        {
            ConnectionString = null;
            SelectCommand = null;
        }

        #endregion Constructors

        #region ValidateInput

        public void ValidateInput()
        {
            if (string.IsNullOrEmpty(ConnectionString)) {
                throw new ArgumentException("Invalid Argument", "ConnectionString");
            }

            if (string.IsNullOrEmpty(SelectCommand)) {
                throw new ArgumentException("Invalid Argument Value", "SelectCommand");
            }
        }

        #endregion ValidateInput
    }

    public class ReadExcelSheetOutput
    {
        #region Data Members

        public DataTable ExcelSheetTable { get; set; }

        #endregion Data Members

        #region Constructors

        public ReadExcelSheetOutput()
        {
            ExcelSheetTable = null;
        }

        #endregion Constructors
    }

    public class ReadExcelSheet
    {
        #region Data Members

        private ReadExcelSheetInput _input = null;
        private ReadExcelSheetOutput _output = null;

        #endregion Data Members

        #region Execute

        public ReadExcelSheetOutput Execute(ReadExcelSheetInput input)
        {
            // Assign and Validate Input
            _input = input;
            _input.ValidateInput();

            // Initialize Output
            _output = new ReadExcelSheetOutput();

            // Read the contents of the Excel Sheet as a Data Table
            _output.ExcelSheetTable = _ReadExcelSheetAsTable(_input.ConnectionString, _input.SelectCommand);

            return _output;
        }

        #endregion Execute

        #region _ReadExcelSheetAsTable

        private DataTable _ReadExcelSheetAsTable(string connectionString, string selectCommand)
        {
            DataTable excelSheetTable = null;

            using (var conn = new OleDbConnection(connectionString)) {

                using (var cmd = new OleDbCommand(selectCommand, conn)) {

                    try {
                    
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        var dataAdapter = new OleDbDataAdapter(cmd);
                        var dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);

                        excelSheetTable = dataSet.Tables[0];

                    } catch (Exception e) {
                        
                        Console.WriteLine(e.Message);

                    }
                }
            }

            return excelSheetTable;
        }

        #endregion _ReadExcelSheetAsTable

    }
}

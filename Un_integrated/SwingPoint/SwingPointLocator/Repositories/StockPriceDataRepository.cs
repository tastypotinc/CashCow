#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Data;
using SwingPointLocator.Entities;

#endregion Namespaces

namespace SwingPointLocator.Repositories
{
    public class StockPriceDataRepository : IStockPriceDataRepository
    {

        #region Data Members
        
        private string _ConnectionString { get; set; }
        private string _SelectAllCommand { get; set; }
        private DataSet _StockPriceDataSet { get; set; }
        private DataTable _StockPriceDataTable { get; set; }

        public string DataSource { get; set; }
        public string FileName { get; set; }

        #endregion Data Members

        #region Constructors
        
        public StockPriceDataRepository()
        {
             //this._ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE""";
             this._ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Mode=ReadWrite;Extended Properties=""text;HDR=Yes;FMT=Delimited"";";
            this.DataSource = this.DataSource;

            // ConnectionString = string.Format(this.ConnectionString, this.DataSource);

            // this.SelectAllCommand = string.Format(@"SELECT * FROM [{0}$]", this.FileName);
        }

        #endregion Constructors

        #region GetPriceData
        public IQueryable<StockPriceData> GetPriceData()
        {
            List<StockPriceData> stockPriceDataEntries = new List<StockPriceData>();

            this._ConnectionString = string.Format(this._ConnectionString, this.DataSource);
            using (OleDbConnection conn = new OleDbConnection(this._ConnectionString))
            {
                this._SelectAllCommand = string.Format(@"SELECT * FROM [{0}]", this.FileName);
                using (OleDbCommand cmd = new OleDbCommand(this._SelectAllCommand, conn))
                {
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);
                        this._StockPriceDataSet = new DataSet();
                        dataAdapter.Fill(this._StockPriceDataSet);

                        this._StockPriceDataTable = this._StockPriceDataSet.Tables[0];

                        StockPriceData stockPriceDataEntry = null;
                        for (int i = 0; i < this._StockPriceDataTable.Rows.Count; ++i)
                        {
                            stockPriceDataEntry = new StockPriceData();

                            stockPriceDataEntry.StockSymbol = this.FileName.Substring(0, this.FileName.LastIndexOf("."));
                            stockPriceDataEntry.PriceDate = Convert.ToString(this._StockPriceDataTable.Rows[i]["Date"]);
                            stockPriceDataEntry.OpenPrice = Convert.ToDecimal(this._StockPriceDataTable.Rows[i]["Open Price"]);
                            stockPriceDataEntry.HighPrice = Convert.ToDecimal(this._StockPriceDataTable.Rows[i]["High Price"]);
                            stockPriceDataEntry.LowPrice = Convert.ToDecimal(this._StockPriceDataTable.Rows[i]["Low Price"]);
                            stockPriceDataEntry.ClosePrice = Convert.ToDecimal(this._StockPriceDataTable.Rows[i]["Close Price"]);
                            stockPriceDataEntry.ShareVolume = Convert.ToDecimal(this._StockPriceDataTable.Rows[i]["No#of Shares"]);
                            stockPriceDataEntry.TradeVolume = Convert.ToDecimal(this._StockPriceDataTable.Rows[i]["No# of Trades"]);
                            stockPriceDataEntry.Turnover = Convert.ToDecimal(this._StockPriceDataTable.Rows[i]["Total Turnover (Rs#)"]);

                            stockPriceDataEntries.Add(stockPriceDataEntry);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[Error] GetAll() failed.", this.DataSource);
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return stockPriceDataEntries.AsQueryable();
        }
        #endregion GetPriceData
    }
}

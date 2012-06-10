
#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stocks10DMA.Entities;
using System.Data.OleDb;
using System.Data;
#endregion Usings

namespace Stocks10DMA.Repositories
{
    public class StockPriceDataRepository : IStockPriceDataRepository
    {

        #region Data Members
        
        private string ConnectionString { get; set; }
        private string SelectAllCommand { get; set; }
        private DataSet StockPriceDataSet { get; set; }
        private DataTable StockPriceDataTable { get; set; }

        public string DataSource { get; set; }
        public string FileName { get; set; }

        #endregion Data Members

        #region Constructors
        
        public StockPriceDataRepository()
        {
            // this.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE""";
            // this.ConnectionString = @"Driver={{Microsoft Text Driver (*.txt; *.csv)}};Dbq={0};Extensions=asc,csv,tab,txt;Persist Security Info=False";
            this.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Mode=ReadWrite;Extended Properties=""text;HDR=Yes;FMT=Delimited"";";
            this.DataSource = this.DataSource;

            // ConnectionString = string.Format(this.ConnectionString, this.DataSource);

            // this.SelectAllCommand = string.Format(@"SELECT * FROM [{0}$]", this.FileName);
        }

        public StockPriceDataRepository(string fullFilePath, string fileName)
        {
            this.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE""";
            this.DataSource = fullFilePath;

            ConnectionString = string.Format(this.ConnectionString, this.DataSource);

            this.SelectAllCommand = string.Format(@"SELECT * FROM [{0}$]", fileName);
        }

        #endregion Constructors

        #region GetPriceData
        public IQueryable<StockPriceData> GetPriceData()
        {
            List<StockPriceData> stockPriceDataEntries = new List<StockPriceData>();

            this.DataSource = this.DataSource.Substring(0, this.DataSource.LastIndexOf('/'));
            this.ConnectionString = string.Format(this.ConnectionString, this.DataSource);
            using (OleDbConnection conn = new OleDbConnection(this.ConnectionString))
            {
                this.SelectAllCommand = string.Format(@"SELECT * FROM [{0}.csv]", this.FileName);
                using (OleDbCommand cmd = new OleDbCommand(this.SelectAllCommand, conn))
                {
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);
                        this.StockPriceDataSet = new DataSet();
                        dataAdapter.Fill(this.StockPriceDataSet);

                        this.StockPriceDataTable = this.StockPriceDataSet.Tables[0];
                        this.StockPriceDataTable.Columns.Add("10DMA", typeof(double));

                        StockPriceData stockPriceDataEntry = null;
                        for (int i = 0; i < this.StockPriceDataTable.Rows.Count; ++i)
                        {
                            this.StockPriceDataTable.Rows[i]["10DMA"] = this.Calculate10DMA(i, this.StockPriceDataTable);
                            this.StockPriceDataSet.AcceptChanges();

                            stockPriceDataEntry = new StockPriceData();

                            stockPriceDataEntry.StockSymbol = this.FileName;
                            stockPriceDataEntry.PriceDate = Convert.ToString(this.StockPriceDataTable.Rows[i]["Date"]);
                            stockPriceDataEntry.OpenPrice = Convert.ToDecimal(this.StockPriceDataTable.Rows[i]["Open Price"]);
                            stockPriceDataEntry.HighPrice = Convert.ToDecimal(this.StockPriceDataTable.Rows[i]["High Price"]);
                            stockPriceDataEntry.LowPrice = Convert.ToDecimal(this.StockPriceDataTable.Rows[i]["Low Price"]);
                            stockPriceDataEntry.ClosePrice = Convert.ToDecimal(this.StockPriceDataTable.Rows[i]["Close Price"]);
                            stockPriceDataEntry.ShareVolume = Convert.ToDecimal(this.StockPriceDataTable.Rows[i]["No#of Shares"]);
                            stockPriceDataEntry.TradeVolume = Convert.ToDecimal(this.StockPriceDataTable.Rows[i]["No# of Trades"]);
                            stockPriceDataEntry.Turnover = Convert.ToDecimal(this.StockPriceDataTable.Rows[i]["Total Turnover (Rs#)"]);
                            stockPriceDataEntry.DMA10 = Convert.ToDecimal(this.StockPriceDataTable.Rows[i]["10DMA"]);

                            stockPriceDataEntries.Add(stockPriceDataEntry);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[Error] GetAll() failed.", this.DataSource);
                        Console.WriteLine(e.Message);
                    }

                }
            }

            return stockPriceDataEntries.AsQueryable();
        }
        #endregion GetPriceData

        #region Calculate10DMA
        private decimal Calculate10DMA(int currentIndex, DataTable stockPriceDataTable)
        {
            decimal dma10 = 0m;
            int counted = 0;
            int startIndex = 0;

            startIndex = (currentIndex < 10) ? 0 : currentIndex - 9;

            for (int i = startIndex; i <= currentIndex; ++i)
            {
                dma10 += Convert.ToDecimal(stockPriceDataTable.Rows[i]["Close Price"]);
                counted++;
            }

            return dma10 / counted;
        }
        #endregion Calculate10DMA

    }
}

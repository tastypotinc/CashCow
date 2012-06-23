#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Data;
using ShowSwingPoint.Models;

#endregion Namespaces

namespace ShowSwingPoint.Repositories
{
    public class SwingPointDataRepository : ISwingPointDataRepository
    {

        #region Data Members
        
        private string _ConnectionString { get; set; }
        private string _SelectAllCommand { get; set; }
        private DataSet _DataSet { get; set; }
        private DataTable _DataTable { get; set; }

        public string DataSource { get; set; }
        public string FileName { get; set; }

        #endregion Data Members

        #region Constructors
        
        public SwingPointDataRepository()
        {
            this._ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Mode=ReadWrite;Extended Properties=""text;HDR=Yes;FMT=Delimited"";";
        }

        #endregion Constructors

        #region GetWatchList
        public IQueryable<WatchListModel> GetWatchList()
        {
            var watchList = new List<WatchListModel>();

            this._ConnectionString = string.Format(this._ConnectionString, this.DataSource);
            using (var conn = new OleDbConnection(this._ConnectionString))
            {
                this._SelectAllCommand = string.Format(@"SELECT * FROM [{0}]", this.FileName);
                using (var cmd = new OleDbCommand(this._SelectAllCommand, conn))
                {
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        var dataAdapter = new OleDbDataAdapter(cmd);
                        this._DataSet = new DataSet();
                        dataAdapter.Fill(this._DataSet);

                        this._DataTable = this._DataSet.Tables[0];

                        WatchListModel watchListModelDataEntry = null;
                        for (int i = 0; i < this._DataTable.Rows.Count; ++i)
                        {
                            watchListModelDataEntry = new WatchListModel();

                            watchListModelDataEntry.WatchListID = Convert.ToInt32(_DataTable.Rows[i]["Id"]);
                            watchListModelDataEntry.BseSymbol = Convert.ToString(_DataTable.Rows[i]["BSESymbol"]);
                            watchListModelDataEntry.NseSymbol = Convert.ToString(_DataTable.Rows[i]["NSESymbol"]);
                            watchListModelDataEntry.Name = Convert.ToString(_DataTable.Rows[i]["Name"]);
                            watchListModelDataEntry.AltNameOne = Convert.ToString(_DataTable.Rows[i]["AltName1"]);
                            watchListModelDataEntry.AltNameTwo = Convert.ToString(_DataTable.Rows[i]["AltName2"]);
                            watchListModelDataEntry.TempName = Convert.ToString(_DataTable.Rows[i]["TempName"]);
                            watchListModelDataEntry.IsActive = Convert.ToBoolean(Convert.ToInt32(_DataTable.Rows[i]["Active"]));
                            watchListModelDataEntry.AlertRequired = Convert.ToBoolean(Convert.ToInt32(_DataTable.Rows[i]["Alert"]));
                            watchListModelDataEntry.ModifiedOn = _DataTable.Rows[i]["ModifiedOn"].ToString();
                            watchListModelDataEntry.CreatedOn = _DataTable.Rows[i]["CreatedOn"].ToString();

                            watchList.Add(watchListModelDataEntry);
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

            return watchList.AsQueryable();
        }

        public IQueryable<StockDataModel> GetStockData()
        {
            var stockData = new List<StockDataModel>();

            this._ConnectionString = string.Format(this._ConnectionString, this.DataSource);
            using (var conn = new OleDbConnection(this._ConnectionString))
            {
                this._SelectAllCommand = string.Format(@"SELECT * FROM [{0}]", this.FileName);
                using (var cmd = new OleDbCommand(this._SelectAllCommand, conn))
                {
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        var dataAdapter = new OleDbDataAdapter(cmd);
                        this._DataSet = new DataSet();
                        dataAdapter.Fill(this._DataSet);

                        this._DataTable = this._DataSet.Tables[0];

                        StockDataModel stockDataModelDataEntry = null;
                        for (int i = 0; i < this._DataTable.Rows.Count; ++i)
                        {
                            stockDataModelDataEntry = new StockDataModel();

                            stockDataModelDataEntry.Date = Convert.ToDateTime(_DataTable.Rows[i]["Date"]);
                            stockDataModelDataEntry.Open = Convert.ToDecimal(_DataTable.Rows[i]["Open"]);
                            stockDataModelDataEntry.High = Convert.ToDecimal(_DataTable.Rows[i]["High"]);
                            stockDataModelDataEntry.Low = Convert.ToDecimal(_DataTable.Rows[i]["Low"]);
                            stockDataModelDataEntry.Close = Convert.ToDecimal(_DataTable.Rows[i]["Close"]);
                            stockDataModelDataEntry.Volume = Convert.ToInt32(_DataTable.Rows[i]["Volume"]);
                            stockDataModelDataEntry.SwingPoint = _DataTable.Rows[i]["SwingPoint"].ToString();

                            stockData.Add(stockDataModelDataEntry);
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

            return stockData.AsQueryable();
        }
        #endregion GetWatchList
    }
}

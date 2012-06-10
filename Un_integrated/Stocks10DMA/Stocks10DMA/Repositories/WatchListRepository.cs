
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
    public class WatchListRepository : IWatchListRepository
    {
        #region Data Members
        private string ConnectionString { get; set; }
        private string DataSource { get; set; }
        private string SelectAllCommand { get; set; }
        #endregion Data Members

        #region Constructors
        public WatchListRepository()
        {
            this.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE""";
            this.DataSource = @"../../Data/WatchList/WatchList.xlsx";

            ConnectionString = string.Format(this.ConnectionString, this.DataSource);

            this.SelectAllCommand = @"SELECT * FROM [CapitalMarket$]";
        }
        #endregion Constructors

        #region GetAll
        public IQueryable<WatchListEntry> GetAll()
        {
            List<WatchListEntry> watchListEntries = new List<WatchListEntry>();

            using (OleDbConnection conn = new OleDbConnection(this.ConnectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(this.SelectAllCommand, conn))
                {
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        dataAdapter.Fill(ds);

                        DataTable watchListTable = ds.Tables[0];
                        WatchListEntry watchListEntry = null;
                        for (int i = 0; i < watchListTable.Rows.Count; ++i)
                        {
                            watchListEntry = new WatchListEntry();
                            
                            watchListEntry.Id = Convert.ToInt32(watchListTable.Rows[i]["Id"]);
                            watchListEntry.BSESymbol = Convert.ToString(watchListTable.Rows[i]["BSESymbol"]);
                            watchListEntry.NSESymbol = Convert.ToString(watchListTable.Rows[i]["NSESymbol"]);
                            watchListEntry.Name = Convert.ToString(watchListTable.Rows[i]["Name"]);
                            watchListEntry.AltName1 = Convert.ToString(watchListTable.Rows[i]["AltName1"]);
                            watchListEntry.AltName2 = Convert.ToString(watchListTable.Rows[i]["AltName2"]);
                            watchListEntry.TempName = Convert.ToString(watchListTable.Rows[i]["TempName"]);
                            watchListEntry.Active = Convert.ToInt32(watchListTable.Rows[i]["Active"]);
                            watchListEntry.Alert = Convert.ToInt32(watchListTable.Rows[i]["Alert"]);
                            watchListEntry.ModifiedOn = Convert.ToInt32(watchListTable.Rows[i]["ModifiedOn"]);
                            watchListEntry.CreatedOn = Convert.ToInt32(watchListTable.Rows[i]["CreatedOn"]);

                            watchListEntries.Add(watchListEntry);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[Error] GetAll() failed.", this.DataSource);
                        Console.WriteLine(e.Message);
                    }

                }
            }

            return watchListEntries.AsQueryable();
        }
        #endregion GetAll
    }
}

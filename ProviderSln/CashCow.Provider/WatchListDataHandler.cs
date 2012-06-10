#region Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using CashCow.Entity;
using CashCow.Provider.Utility;
using CashCow.ProviderInterface;

#endregion Namespaces

namespace CashCow.Provider
{
    /// <summary>
    /// WatchList data handler class.
    /// </summary>
    public class WatchListDataHandler : BaseDataHandler, IWatchListDataHandler
    {
        #region Public Methods

        /// <summary>
        /// Data handling method to delete WatchListEntity based on Id.
        /// </summary>
        /// <param name="watchListEntityId">WatchListEntity id to be deleted.</param>
        /// <returns>Id of the WatchListEntity that is deleted.</returns>
        public int DeleteWatchListItem(int watchListEntityId)
        {
            int deletedwatchListEntityId;

            try
            {
                using (DbCommand cmd =
                                Database.GetStoredProcCommand(DataAccess.StoredProcedure.Dbo.WATCH_LIST_ITEM_DELETE))
                {
                    cmd.Connection = Database.CreateConnection();
                    cmd.Connection.Open();

                    Database.AddInParameter(cmd, DataAccess.Params.WATCH_LIST_ID, DbType.Int32,
                                                SqlUtil.ParameterValue(watchListEntityId));

                    var dbRes = cmd.ExecuteNonQuery();

                    // Set the value to 0 if DB operation failed.
                    deletedwatchListEntityId = (dbRes > 0) ? watchListEntityId : 0;
                }
            }
            catch (Exception exception)
            {
                deletedwatchListEntityId = 0;
            }

            return deletedwatchListEntityId;
        }

        /// <summary>
        /// Data handling method to add/update WatchListEntity based on Id.
        /// </summary>
        /// <param name="watchListEntity">WatchListEntity to be saved or updated.</param>
        /// <returns>Id of WatchListEntity inserted/updated.</returns>
        public int SaveWatchListItem(WatchListEntity watchListEntity)
        {
            int watchListIdSaved;

            try
            {
                using (DbCommand cmd =
                                Database.GetStoredProcCommand(DataAccess.StoredProcedure.Dbo.WATCH_LIST_ITEM_SAVE))
                {
                    cmd.Connection = Database.CreateConnection();
                    cmd.Connection.Open();

                    Database.AddInParameter(cmd, DataAccess.Params.BSE_SYMBOL, DbType.String,
                                                SqlUtil.ParameterValue(watchListEntity.BseSymbol));

                    Database.AddInParameter(cmd, DataAccess.Params.NSE_SYMBOL, DbType.String,
                                                SqlUtil.ParameterValue(watchListEntity.NseSymbol));

                    Database.AddInParameter(cmd, DataAccess.Params.NAME, DbType.String,
                                                SqlUtil.ParameterValue(watchListEntity.Name));

                    Database.AddInParameter(cmd, DataAccess.Params.ALT_NAME_ONE, DbType.String,
                                                SqlUtil.ParameterValue(watchListEntity.AltNameOne));

                    Database.AddInParameter(cmd, DataAccess.Params.ALT_NAME_TWO, DbType.String,
                                                SqlUtil.ParameterValue(watchListEntity.AltNameTwo));

                    Database.AddInParameter(cmd, DataAccess.Params.ALT_NAME_THREE, DbType.String,
                                                SqlUtil.ParameterValue(watchListEntity.AltNameThree));

                    Database.AddInParameter(cmd, DataAccess.Params.TEMP_NAME, DbType.String,
                                                SqlUtil.ParameterValue(watchListEntity.TempName));

                    Database.AddInParameter(cmd, DataAccess.Params.IS_ACTIVE, DbType.Boolean,
                                                SqlUtil.ParameterValue(watchListEntity.IsActive));

                    Database.AddInParameter(cmd, DataAccess.Params.ALERT_REQUIRED, DbType.Boolean,
                                                SqlUtil.ParameterValue(watchListEntity.AlertRequired));

                    Database.AddInParameter(cmd, DataAccess.Params.MODIFIED_ON, DbType.DateTime,
                                                SqlUtil.ParameterValue(watchListEntity.ModifiedOn));

                    Database.AddInParameter(cmd, DataAccess.Params.CREATED_ON, DbType.DateTime,
                                                SqlUtil.ParameterValue(watchListEntity.CreatedOn));

                    Database.AddParameter(cmd, DataAccess.Params.WATCH_LIST_ID, DbType.Int32, ParameterDirection.InputOutput, null,
                                          DataRowVersion.Default, watchListEntity.WatchListID);

                    var dbRes = cmd.ExecuteNonQuery();

                    watchListIdSaved = SqlUtil.SetValue(cmd.Parameters["@" + DataAccess.Params.WATCH_LIST_ID], 0);

                    // Set the value to 0 if DB operation failed.
                    watchListIdSaved = (dbRes > 0) ? watchListIdSaved : 0;
                }
            }
            catch (Exception exception)
            {
                watchListIdSaved = 0;
            }

            return watchListIdSaved;
        }

        /// <summary>
        /// Data handling method to search for WatchList entities based on search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria object containing all required criteria info.</param>
        /// <param name="watchListId">Id of particular watchlist entity that has to be fetched. Pass 0 if not applicable.</param>
        /// <returns>List of WatchListEntity.</returns>
        public IList<WatchListEntity> SearchWatchList(GridSearchCriteriaEntity searchCriteria, int watchListId)
        {
            var watchList = new List<WatchListEntity>();

            try
            {
                using (DbCommand cmd =
                                Database.GetStoredProcCommand(DataAccess.StoredProcedure.Dbo.WATCH_LIST_SEARCH))
                {
                    cmd.Connection = Database.CreateConnection();
                    cmd.Connection.Open();

                    Database.AddInParameter(cmd, DataAccess.Params.WATCH_LIST_ID, DbType.Int32,
                                                SqlUtil.ParameterValue(watchListId));
                    
                    Database.AddInParameter(cmd, DataAccess.Params.START_ROW_INDEX, DbType.Int32,
                                                SqlUtil.ParameterValue(searchCriteria.StartRowIndex));

                    Database.AddInParameter(cmd, DataAccess.Params.MAXIMUM_ROWS, DbType.Int32,
                                                SqlUtil.ParameterValue(searchCriteria.MaximumRows));

                    Database.AddInParameter(cmd, DataAccess.Params.SORT_COLUMN, DbType.String,
                                                SqlUtil.ParameterValue(searchCriteria.SortColumn));
                    
                    Database.AddInParameter(cmd, DataAccess.Params.SORT_ASCENDING, DbType.Boolean,
                                                SqlUtil.ParameterValue(searchCriteria.SortAscending));

                    Database.AddInParameter(cmd, DataAccess.Params.SEARCH_CRITERIA, DbType.Xml,
                                                SqlUtil.ParameterValue(searchCriteria.SearchCriteria));

                    Database.AddInParameter(cmd, DataAccess.Params.TEXT_SEARCH_KEY, DbType.String,
                                                SqlUtil.ParameterValue(searchCriteria.TextSearchKey));

                    Database.AddInParameter(cmd, DataAccess.Params.SEARCH_AGAINST, DbType.Boolean,
                                                SqlUtil.ParameterValue(searchCriteria.SearchAgainst));

                    Database.AddInParameter(cmd, DataAccess.Params.SEARCH_WITH_OR, DbType.Boolean,
                                                SqlUtil.ParameterValue(searchCriteria.SearchWithOr));

                    Database.AddParameter(cmd, DataAccess.Params.RECORD_COUNT, DbType.Int32, ParameterDirection.InputOutput, null,
                                          DataRowVersion.Default, searchCriteria.RecordCount);

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var watchListItem = new WatchListEntity();

                            watchListItem.Load(SqlUtil.SetValue(dr[DataAccess.Params.WATCH_LIST_ID], 0),
                                SqlUtil.SetValue(dr[DataAccess.Params.BSE_SYMBOL], string.Empty),
                                SqlUtil.SetValue(dr[DataAccess.Params.NSE_SYMBOL], string.Empty),
                                SqlUtil.SetValue(dr[DataAccess.Params.NAME], string.Empty),
                                SqlUtil.SetValue(dr[DataAccess.Params.ALT_NAME_ONE], string.Empty),
                                SqlUtil.SetValue(dr[DataAccess.Params.ALT_NAME_TWO], string.Empty),
                                SqlUtil.SetValue(dr[DataAccess.Params.ALT_NAME_THREE], string.Empty),
                                SqlUtil.SetValue(dr[DataAccess.Params.TEMP_NAME], string.Empty),
                                SqlUtil.SetValue(dr[DataAccess.Params.IS_ACTIVE], true),
                                SqlUtil.SetValue(dr[DataAccess.Params.ALERT_REQUIRED], true),
                                SqlUtil.SetValue(dr[DataAccess.Params.CREATED_ON], DateTime.MinValue),
                                SqlUtil.SetValue(dr[DataAccess.Params.MODIFIED_ON], DateTime.MinValue));

                            watchList.Add(watchListItem);
                        }
                    }

                    searchCriteria.RecordCount = SqlUtil.SetValue(cmd.Parameters["@" + DataAccess.Params.RECORD_COUNT], 0);
                }
            }
            catch (Exception exception)
            {
                watchList = null;
            }

            return watchList;
        }

        #endregion Public Methods
    }
}

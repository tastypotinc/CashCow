#region Namespaces

using System.Collections.Generic;
using CashCow.BusinessInterface;
using CashCow.Entity;
using CashCow.Provider;
using CashCow.ProviderInterface;

#endregion Namespaces

namespace CashCow.Business
{
    /// <summary>
    /// WatchList business class.
    /// </summary>
    public class WatchListBusiness : IWatchListBusiness
    {
        #region Public Methods

        /// <summary>
        /// Business method to delete WatchListEntity based on Id.
        /// </summary>
        /// <param name="watchListEntityId">WatchListEntity id to be deleted.</param>
        /// <returns>Id of the WatchListEntity that is deleted.</returns>
        public int DeleteWatchListItem(int watchListEntityId)
        {
            IWatchListDataHandler watchListData = new WatchListDataHandler();

            return watchListData.DeleteWatchListItem(watchListEntityId);
        }

        /// <summary>
        /// Business method to add/update WatchListEntity based on Id.
        /// </summary>
        /// <param name="watchListEntity">WatchListEntity to be saved or updated.</param>
        /// <returns>Id of WatchListEntity inserted/updated.</returns>
        public int SaveWatchListItem(WatchListEntity watchListEntity)
        {
            IWatchListDataHandler watchListData = new WatchListDataHandler();

            return watchListData.SaveWatchListItem(watchListEntity);
        }

        /// <summary>
        /// Business method to search for WatchList entities based on search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria object containing all required criteria info.</param>
        /// <param name="watchListId">Id of particular watchlist entity that has to be fetched. Pass 0 if not applicable.</param>
        /// <returns>List of WatchListEntity.</returns>
        public IList<WatchListEntity> SearchWatchList(GridSearchCriteriaEntity searchCriteria, int watchListId)
        {
            IWatchListDataHandler watchListData = new WatchListDataHandler();

            return watchListData.SearchWatchList(searchCriteria, watchListId);
        }

        #endregion Public Methods
    }
}

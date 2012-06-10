#region Namespaces

using System.Collections.Generic;
using CashCow.Entity;

#endregion Namespaces

namespace CashCow.ProviderInterface
{
    /// <summary>
    /// Interface that defines implementing methods for WatchList data handling.
    /// </summary>
    public interface IWatchListDataHandler
    {
        /// <summary>
        /// Data handling method to delete WatchListEntity based on Id.
        /// </summary>
        /// <param name="watchListEntityId">WatchListEntity id to be deleted.</param>
        /// <returns>Id of the WatchListEntity that is deleted.</returns>
        int DeleteWatchListItem(int watchListEntityId);

        /// <summary>
        /// Data handling method to add/update WatchListEntity based on Id.
        /// </summary>
        /// <param name="watchListEntity">WatchListEntity to be saved or updated.</param>
        /// <returns>Id of WatchListEntity inserted/updated.</returns>
        int SaveWatchListItem(WatchListEntity watchListEntity);

        /// <summary>
        /// Data handling method to search for WatchList entities based on search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria object containing all required criteria info.</param>
        /// <param name="watchListId">Id of particular watchlist entity that has to be fetched. Pass 0 if not applicable.</param>
        /// <returns>List of WatchListEntity.</returns>
        IList<WatchListEntity> SearchWatchList(GridSearchCriteriaEntity searchCriteria, int watchListId);
    }
}

#region Namespaces

using System;
using CashCow.Entity;
using CashCow.Web.MvcHelpers;
using Helpers;

#endregion Namespaces

namespace CashCow.Web.Models.WatchList
{
    /// <summary>
    /// The view modal class for WatchList.
    /// </summary>
    public class WatchListModel
    {
        #region Private Data

        private bool _alertRequired = true;
        private bool _isActive = true;

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// WatchList item alert flag.
        /// </summary>
        [CcDisplayName("Alert Required")]
        public bool AlertRequired
        {
            get
            {
                return this._alertRequired;
            }

            set
            {
                this._alertRequired = value;
            }
        }

        /// <summary>
        /// WatchList item first alternate name.
        /// </summary>
        [CcDisplayName("Alt Name One")]
        public string AltNameOne { get; set; }

        /// <summary>
        /// WatchList item third alternate name.
        /// </summary>
        [CcDisplayName("Alt Name Three")]
        public string AltNameThree { get; set; }

        /// <summary>
        /// WatchList item second alternate name.
        /// </summary>
        [CcDisplayName("Alt Name Two")]
        public string AltNameTwo { get; set; }

        /// <summary>
        /// WatchList item Bse symbol.
        /// </summary>
        [CcDisplayName("BSE Symbol")]
        public string BseSymbol { get; set; }

        /// <summary>
        /// WatchList item creation date.
        /// </summary>
        [CcDisplayName("Created On")]
        public string CreatedOn { get; set; }

        /// <summary>
        /// WatchList item status flag.
        /// </summary>
        [CcDisplayName("Is Active")]
        public bool IsActive
        {
            get
            {
                return this._isActive;
            }

            set
            {
                this._isActive = value;
            }
        }
        
        /// <summary>
        /// WatchList item modification date.
        /// </summary>
        [CcDisplayName("Modified On")]
        public string ModifiedOn { get; set; }

        /// <summary>
        /// WatchList item name.
        /// </summary>
        [CcDisplayName("Name")]
        public string Name { get; set; }

        /// <summary>
        /// WatchList item Nse symbol.
        /// </summary>
        [CcDisplayName("NSE Symbol")]
        public string NseSymbol { get; set; }

        /// <summary>
        /// WatchList item temporary name.
        /// </summary>
        [CcDisplayName("Temp Name")]
        public string TempName { get; set; }

        /// <summary>
        /// WatchList item unique Id.
        /// </summary>
        public int WatchListID { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Converts a WatchListModel to WatchListEntity.
        /// </summary>
        /// <param name="watchListModel">WatchListModel to be converted to WatchListEntity.</param>
        /// <returns>WatchListEntity corresponding to WatchListModel.</returns>
        public static WatchListEntity ConvertModelToWatchListEntity(WatchListModel watchListModel)
        {
            return new WatchListEntity
            {
                AlertRequired = watchListModel.AlertRequired,
                AltNameOne = watchListModel.AltNameOne,
                AltNameThree = watchListModel.AltNameThree,
                AltNameTwo = watchListModel.AltNameTwo,
                BseSymbol = watchListModel.BseSymbol,
                CreatedOn = !string.IsNullOrEmpty(watchListModel.CreatedOn) ?
                    DataFormatter.GetDateTimeInUtcFormat(Convert.ToDateTime(watchListModel.CreatedOn)) : null,
                IsActive = watchListModel.IsActive,
                ModifiedOn = !string.IsNullOrEmpty(watchListModel.ModifiedOn) ?
                    DataFormatter.GetDateTimeInUtcFormat(Convert.ToDateTime(watchListModel.ModifiedOn)) : null,
                Name = watchListModel.Name,
                NseSymbol = watchListModel.NseSymbol,
                TempName = watchListModel.TempName,
                WatchListID = watchListModel.WatchListID
            };
        }

        /// <summary>
        /// Converts a WatchListEntity to WatchListModel.
        /// </summary>
        /// <param name="watchListEntity">WatchListEntity to be converted to WatchListModel.</param>
        /// <returns>WatchListModel corresponding to WatchListEntity.</returns>
        public static WatchListModel ConvertWatchListEntityToModel(WatchListEntity watchListEntity)
        {
            return new WatchListModel
                       {
                           AlertRequired = watchListEntity.AlertRequired,
                           AltNameOne = watchListEntity.AltNameOne,
                           AltNameThree = watchListEntity.AltNameThree,
                           AltNameTwo = watchListEntity.AltNameTwo,
                           BseSymbol = watchListEntity.BseSymbol,
                           CreatedOn = (watchListEntity.CreatedOn != null)
                                           ? DataFormatter.FormatDateToString(DataFormatter.GetDateTimeInLocalFormat(watchListEntity.CreatedOn))
                                           : null,
                           IsActive = watchListEntity.IsActive,
                           ModifiedOn = (watchListEntity.ModifiedOn != null)
                                            ? DataFormatter.FormatDateToString(DataFormatter.GetDateTimeInLocalFormat(watchListEntity.ModifiedOn))
                                            : null,
                           Name = watchListEntity.Name,
                           NseSymbol = watchListEntity.NseSymbol,
                           TempName = watchListEntity.TempName,
                           WatchListID = watchListEntity.WatchListID
                       };
        }

        #endregion Public Methods
    }
}
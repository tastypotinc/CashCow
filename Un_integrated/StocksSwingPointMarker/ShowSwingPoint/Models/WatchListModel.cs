#region Namespaces

using System;

#endregion Namespaces

namespace ShowSwingPoint.Models
{
    /// <summary>
    /// The view modal class for WatchList.
    /// </summary>
    public class WatchListModel
    {
        #region Public Properties

        /// <summary>
        /// WatchList item alert flag.
        /// </summary>
        public bool AlertRequired { get; set; }

        /// <summary>
        /// WatchList item first alternate name.
        /// </summary>
        public string AltNameOne { get; set; }

        /// <summary>
        /// WatchList item third alternate name.
        /// </summary>
        public string AltNameThree { get; set; }

        /// <summary>
        /// WatchList item second alternate name.
        /// </summary>
        public string AltNameTwo { get; set; }

        /// <summary>
        /// WatchList item Bse symbol.
        /// </summary>
        public string BseSymbol { get; set; }

        /// <summary>
        /// WatchList item creation date.
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// WatchList item status flag.
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// WatchList item modification date.
        /// </summary>
        public string ModifiedOn { get; set; }

        /// <summary>
        /// WatchList item name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// WatchList item Nse symbol.
        /// </summary>
        public string NseSymbol { get; set; }

        /// <summary>
        /// WatchList item temporary name.
        /// </summary>
        public string TempName { get; set; }

        /// <summary>
        /// WatchList item unique Id.
        /// </summary>
        public int WatchListID { get; set; }

        #endregion Public Properties
    }
}
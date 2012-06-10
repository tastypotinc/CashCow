#region Namespaces

using CashCow.Web.MvcHelpers;

#endregion Namespaces

namespace CashCow.Web.Models.WatchList
{
    /// <summary>
    /// The view modal class for WatchList.
    /// </summary>
    public class WatchListSearchModel
    {
        #region Public Properties

        /// <summary>
        /// WatchList item alert flag.
        /// </summary>
        [CcDisplayName("Alert Required")]
        public bool? AlertRequired { get; set; }

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
        public bool? IsActive { get; set; }
        
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
        /// Flag to indicate if search has to performed for or against the search criteria specified.
        /// </summary>
        [CcDisplayName("Search against specified criteria")]
        public bool SearchAgainst { get; set; }

        /// <summary>
        /// The search condition flag. Indicates how different search criteria has to be connected.
        /// </summary>
        [CcDisplayName("Search condition")]
        public bool SearchWithAnd { get; set; }

        /// <summary>
        /// WatchList item temporary name.
        /// </summary>
        [CcDisplayName("Temp Name")]
        public string TempName { get; set; }

        #endregion Public Properties
    }
}
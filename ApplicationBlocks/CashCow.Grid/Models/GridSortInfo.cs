#region Namespaces

using Helpers;

#endregion Namespaces

namespace CashCow.Grid.Models
{
    /// <summary>
    /// Class representing grid sort info.
    /// </summary>
    public class GridSortInfo
    {
        #region Private Data

        private SortDirection _sortOrder = SortDirection.Ascending;

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// Flag to indicate if sort order is Ascending. Read-only.
        /// </summary>
        public bool SortAscending
        {
            get
            {
                return (this._sortOrder == SortDirection.Ascending) ? true : false;
            }
        }

        /// <summary>
        /// Field name to sort.
        /// </summary>
        public string SortOn { get; set; }

        /// <summary>
        /// Sort order.
        /// Default value = SortDirection.Ascending.
        /// </summary>
        public SortDirection SortOrder
        {
            get
            {
                return this._sortOrder;
            }

            set
            {
                this._sortOrder = value;
            }
        }

        #endregion Public Properties
    }
}
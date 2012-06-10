#region Namespaces

using Helpers;

#endregion Namespaces

namespace CashCow.Entity
{
    /// <summary>
    /// Class representing search criteria for grids.
    /// </summary>
    public class GridSearchCriteriaEntity
    {
        #region Private Data

        private int _pageSize = ConfigHelper.DefaultGridPageSize;
        private SearchCondition _searchCondition = SearchCondition.Or;
        private SortDirection _sortOrder = SortDirection.Ascending;

        #endregion Private Data
        
        #region Public Properties

        /// <summary>
        /// Number of rows that has to be returned.
        /// Default value = ConfigHelper.DefaultGridPageSize.
        /// </summary>
        public int MaximumRows
        {
            get
            {
                return this._pageSize;
            }

            set
            {
                this._pageSize = value;
            }
        }

        /// <summary>
        /// Number of records returned.
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// Flag to indicate if search has to performed for or against the search criteria specified.
        /// </summary>
        public bool SearchAgainst { get; set; }

        /// <summary>
        /// Search criteria containing search field and search value in XML format. Leave it blank or null to get all result.
        /// Format should be: <SearchCriteria><Criteria SearchOn="ColumnName" SearchValue="SearchValue"></Criteria></SearchCriteria>
        /// </summary>
        public string SearchCriteria { get; set; }

        /// <summary>
        /// Flag to indicate if search criterias has to be connected with AND or OR.
        /// Default value = true.
        /// </summary>
        public bool SearchWithOr
        {
            get
            {
                return (this._searchCondition == SearchCondition.Or) ? true : false;
            }

            set
            {
                this._searchCondition = (value) ? SearchCondition.Or : SearchCondition.And;
            }
        }

        /// <summary>
        /// Flag for sort direction.
        /// Default value = true.
        /// </summary>
        public bool SortAscending
        {
            get
            {
                return (this._sortOrder == SortDirection.Ascending) ? true : false;
            }

            set
            {
                this._sortOrder = (value) ? SortDirection.Ascending : SortDirection.Descending;
            }
        }

        /// <summary>
        /// Name of the column that has to be sorted.
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// Index of first row that has to be returned.
        /// </summary>
        public int StartRowIndex { get; set; }

        /// <summary>
        /// The key to be searched on all text columns. This is generally implemented on the result returned after search criteria is implemented.
        /// </summary>
        public string TextSearchKey { get; set; }

        #endregion Public Properties
    }
}


namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Class representing grid level info or grid context.
    /// </summary>
    public class GridContext : Grid
    {
        #region Private Data
        
        private GridPagerModel _gridPager = new GridPagerModel();
        private GridSearchInfo _searchInfo = new GridSearchInfo();
        private GridSortInfo _sortInfo = new GridSortInfo();
        
        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// Grid pager for the grid.
        /// </summary>
        public GridPagerModel GridPager
        {
            get
            {
                return this._gridPager;
            }

            set
            {
                this._gridPager = value;
            }
        }

        /// <summary>
        /// Grid search info.
        /// </summary>
        public GridSearchInfo SearchInfo
        {
            get
            {
                return this._searchInfo;
            }

            set
            {
                this._searchInfo = value;
            }
        }
        
        /// <summary>
        /// Sorting info of the gid.
        /// </summary>
        public GridSortInfo SortInfo
        {
            get
            {
                return this._sortInfo;
            }

            set
            {
                this._sortInfo = value;
            }
        }

        #endregion Public Properties
    }
}
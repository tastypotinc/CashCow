#region Namespaces

using System.Collections.Generic;

#endregion Namespaces

namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Model class for grid column.
    /// This should be used to be passed as parameter for grid model building.
    /// </summary>
    public class GridColumnModel
    {
        #region Private Data

        private GridCommonProperties _bodyCellProperty = new GridCommonProperties();
        private GridHeaderCellModel _headerCell = new GridHeaderCellModel();
        private List<List<GridLinkModel>> _links = new List<List<GridLinkModel>>();

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// Common properties of all body cells of this column.
        /// </summary>
        public GridCommonProperties BodyCellProperty
        {
            get
            {
                return this._bodyCellProperty;
            }

            set
            {
                this._bodyCellProperty = value;
            }
        }
        
        /// <summary>
        /// Header cell for the column.
        /// </summary>
        public GridHeaderCellModel HeaderCell
        {
            get
            {
                return this._headerCell;
            }

            set
            {
                this._headerCell = value;
            }
        }

        /// <summary>
        /// Link(s) that has to be displayed when column type defined on header cell is Link.
        /// It contains list of links for all cells under a column. Since each cell can have more then one links hence, it is list of list.
        /// </summary>
        public List<List<GridLinkModel>> Links
        {
            get
            {
                return this._links;
            }

            set
            {
                this._links = value;
            }
        }

        #endregion Public Properties
    }
}

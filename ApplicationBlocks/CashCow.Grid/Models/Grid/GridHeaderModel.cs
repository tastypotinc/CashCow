#region Namespaces

using System.Collections.Generic;

#endregion Namespaces

namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Model class for grid header.
    /// </summary>
    public class GridHeaderModel
    {
        #region Private Data

        private string _cssClass = "gridHeaderRow";
        private IList<GridHeaderCellModel> _cells = new List<GridHeaderCellModel>();

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// The CSS class for grid header row.
        /// Default value = gridHeaderRow.
        /// </summary>
        public string CssClass
        {
            get
            {
                return this._cssClass;
            }

            set
            {
                this._cssClass = value;
            }
        }
        
        /// <summary>
        /// List of grid header cells.
        /// </summary>
        public IList<GridHeaderCellModel> Cells
        {
            get
            {
                return this._cells;
            }

            set
            {
                this._cells = value;
            }
        }

        #endregion Public Properties
    }
}
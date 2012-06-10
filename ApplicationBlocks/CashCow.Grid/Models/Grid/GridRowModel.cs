#region Namespaces

using System.Collections.Generic;

#endregion Namespaces

namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Model class for grid row.
    /// </summary>
    public class GridRowModel
    {
        #region Private Data

        private IList<GridRowCellModel> _cells = new List<GridRowCellModel>();

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// List of grid row cells.
        /// </summary>
        public IList<GridRowCellModel> Cells
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
#region Namespaces

using System.Collections.Generic;

#endregion Namespaces

namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Model class for grid body.
    /// </summary>
    public class GridBodyModel
    {
        #region Private Data

        private string _cssClass = "gridBodyRow";
        private IList<GridRowModel> _rows = new List<GridRowModel>();

        #endregion Private Data

        #region Public Properties
        
        /// <summary>
        /// The CSS class for grid body rows.
        /// Default value = gridBodyRow.
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
        /// List of grid body rows.
        /// </summary>
        public IList<GridRowModel> Rows
        {
            get
            {
                return this._rows;
            }

            set
            {
                this._rows = value;
            }
        }

        #endregion Public Properties
    }
}

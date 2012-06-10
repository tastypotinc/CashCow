#region Namespaces

using System.Collections.Generic;
using Helpers;

#endregion Namespaces

namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Model class for grid row cell.
    /// </summary>
    public class GridRowCellModel
    {
        #region Private Data

        private GridColumnType _columnType = GridColumnType.Text;
        private string _cssClass = "gridBodyRowCell";
        private IList<GridLinkModel> _links = new List<GridLinkModel>();

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// Type of column under this header cell.
        /// Default value = GridColumnType.Text.
        /// </summary>
        public GridColumnType ColumnType
        {
            get
            {
                return this._columnType;
            }

            set
            {
                this._columnType = value;
            }
        }

        /// <summary>
        /// Type of column under this header cell. Read-only string.
        /// </summary>
        public string ColumnTypeString
        {
            get
            {
                return this._columnType.ToString();
            }
        }

        /// <summary>
        /// The CSS class for grid row cell.
        /// Dedfault value = gridBodyRowCell.
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
        /// Path of the image to be shown in grid row cell.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Flag to indicate if grid column is disabled.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Link(s) that has to be displayed when column type defined on header cell is Link.
        /// </summary>
        public IList<GridLinkModel> Links
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
        
        /// <summary>
        /// Text to be displayed in grid row cell.
        /// </summary>
        public string Text { get; set; }

        #endregion Public Properties
    }
}
#region Namespaces

using Helpers;

#endregion Namespaces

namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Model class for grid header cell.
    /// </summary>
    public class GridHeaderCellModel
    {
        #region Private Data
        
        private bool _allowSorting = true;
        private string _bindingColumnName = string.Empty;
        private GridColumnType _columnType = GridColumnType.Text;
        private string _cssClass = "gridHeaderRowCell";
        private string _sortColumnName = string.Empty;
        private int _width = ConfigHelper.DefaultGridCellWidth;

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// Flag to indicate if sorting is allowed on the column.
        /// This is valid only for text column and for image column provided it is showing boolean data.
        /// Default value = true.
        /// </summary>
        public bool AllowSorting
        {
            get
            {
                return this._allowSorting;
            }

            set
            {
                this._allowSorting = value;
            }
        }

        /// <summary>
        /// Name of the data source column. This is not valid for Link ColumnType.
        /// </summary>
        public string BindingColumnName
        {
            get
            {
                return this._bindingColumnName;
            }

            set
            {
                this._bindingColumnName = value;
            }
        }
        
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
        /// The CSS class for grid header cell.
        /// Default value = gridHeaderRowCell.
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
        /// Flag to indicate if grid column is disabled.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Label to be displayed in grid header cell.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The column name on DB corresponding to grid column on which sort should happen. If not specified then BindingColumnName is returned.
        /// This is valid only if AllowSorting = true.
        /// </summary>
        public string SortColumnName
        {
            get
            {
                return (string.IsNullOrEmpty(this._sortColumnName)) ? this._bindingColumnName : this._sortColumnName;
            }

            set
            {
                this._sortColumnName = value;
            }
        }

        /// <summary>
        /// Width of the column. To be set in the inline style of the header cell.
        /// Default value = ConfigHelper.DefaultGridCellWidth.
        /// </summary>
        public int Width
        {
            get
            {
                return this._width;
            }

            set
            {
                this._width = value;
            }
        }
        
        #endregion Public Properties
    }
}
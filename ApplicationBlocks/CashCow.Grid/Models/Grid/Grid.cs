
namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Base class for grid.
    /// </summary>
    public class Grid
    {
        #region Private Data

        private string _alternateRowCss = "gridAlternateRow";
        private string _cssClass = "gridTable";
        private string _rowCss = "gridRow";

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// The grid alternate row css class. Should be used to just set the row colour different to RowCss colour.
        /// Default value = gridAlternateRow.
        /// </summary>
        public string AlternateRowCss
        {
            get
            {
                return this._alternateRowCss;
            }
            set
            {
                this._alternateRowCss = value;
            }
        }

        /// <summary>
        /// The CSS class for grid table.
        /// Default value = gridTable.
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
        /// The default action name for the grid. The action method to handle all action grid specific action i.e. searching, sorting, paging.
        /// Not post from data in grid.
        /// </summary>
        public string DefaultAction { get; set; }

        /// <summary>
        /// Flag to enable and disable search on grid.
        /// </summary>
        public bool DisableGridSearch { get; set; }
        
        /// <summary>
        /// The grid row css class. Should be used to just set the row colour.
        /// Default value = gridRow.
        /// </summary>
        public string RowCss
        {
            get
            {
                return this._rowCss;
            }

            set
            {
                this._rowCss = value;
            }
        }

        #endregion Public Properties
    }
}
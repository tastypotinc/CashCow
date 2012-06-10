
namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Model class for Grid.
    /// </summary>
    public class GridModel
    {
        #region Public Properties

        /// <summary>
        /// Grid body for the grid.
        /// </summary>
        public GridBodyModel GridBodyModel { get; set; }
        
        /// <summary>
        /// Grid context.
        /// </summary>
        public GridContext GridContext { get; set; }

        /// <summary>
        /// Grid header for the grid.
        /// </summary>
        public GridHeaderModel GridHeader { get; set; }
        
        #endregion Public Properties
    }
}
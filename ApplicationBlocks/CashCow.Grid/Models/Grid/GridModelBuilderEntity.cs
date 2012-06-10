#region Namespaces

using System.Collections.Generic;

#endregion Namespaces

namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Class to be passed as parameter for grid model building.
    /// It contains all required grid configuration info. It does not contain data.
    /// </summary>
    public class GridModelBuilderEntity
    {
        #region Private Data

        private IList<GridColumnModel> _columns = new List<GridColumnModel>();
        private GridCommonProperties _gridBodyRowProperty = new GridCommonProperties();
        private GridContext _gridContext = new GridContext();
        private GridCommonProperties _gridHeaderRowProperty = new GridCommonProperties();

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// List of columns to be constructed.
        /// </summary>
        public IList<GridColumnModel> Columns
        {
            get
            {
                return this._columns;
            }

            set
            {
                this._columns = value;
            }
        }
        
        /// <summary>
        /// Property common to all grid body rows.
        /// </summary>
        public GridCommonProperties GridBodyRowProperty
        {
            get
            {
                return this._gridBodyRowProperty;
            }

            set
            {
                this._gridBodyRowProperty = value;
            }
        }

        /// <summary>
        /// Grid context.
        /// </summary>
        public GridContext GridContext
        {
            get
            {
                return this._gridContext;
            }

            set
            {
                this._gridContext = value;
            }
        }

        /// <summary>
        /// Genaral property of grid header row.
        /// </summary>
        public GridCommonProperties GridHeaderRowProperty
        {
            get
            {
                return this._gridHeaderRowProperty;
            }

            set
            {
                this._gridHeaderRowProperty = value;
            }
        }

        #endregion Public Properties
    }
}

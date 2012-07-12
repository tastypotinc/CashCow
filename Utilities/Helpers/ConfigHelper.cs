#region Namespaces

using System;
using System.Configuration;

#endregion Namespaces

namespace Helpers
{
    /// <summary>
    /// Class to do all configuration file related operation.
    /// </summary>
    public class ConfigHelper
    {
        #region Public Properties

        /// <summary>
        /// DateTime format used across the application. Read-only.
        /// </summary>
        public static string DateTimeFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["DateTimeFormat"];
            }
        }

        /// <summary>
        /// DateTime format used across application for conversion to string. Read-only.
        /// </summary>
        public static string DateTimeFormatForString
        {
            get
            {
                return string.Format("{{0:{0}}}", DateTimeFormat);
            }
        }

        /// <summary>
        /// Default grid action name. Read-only.
        /// </summary>
        public static string DefaultGridAction
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultGridAction"];
            }
        }

        /// <summary>
        /// Default width of the grid to be used across the application. Read-only.
        /// </summary>
        public static int DefaultGridCellWidth
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultGridCellWidth"]);
            }
        }

        /// <summary>
        /// Default number of grid pager elements across the application. Read-only.
        /// </summary>
        public static int DefaultGridPagerElements
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultGridPagerElements"]);
            }
        }

        /// <summary>
        /// Default page size of a grid across the application. Read-only.
        /// </summary>
        public static int DefaultGridPageSize
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultGridPageSize"]);
            }
        }

        /// <summary>
        /// Default pop-up window height to be used across application. Read-only.
        /// </summary>
        public static int DefaultWindowHeight
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultWindowHeidht"]);
            }
        }

        /// <summary>
        /// Default pop-up window width to be used across application. Read-only.
        /// </summary>
        public static int DefaultWindowWidth
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultWindowWidth"]);
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets image path that represents bool true.
        /// </summary>
        /// <param name="imageType">Boolean value for which image path is required.</param>
        /// <returns>Image path representing boolean value passed as parameter.</returns>
        public static string GetBooleanImage(bool imageType)
        {
            return (imageType)
                       ? ConfigurationManager.AppSettings["BoolTrueImagePath"]
                       : ConfigurationManager.AppSettings["BoolFalseImagePath"];
        }
        
        #endregion Public Methods
    }
}

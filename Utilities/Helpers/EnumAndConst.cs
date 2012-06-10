
namespace Helpers
{
    #region Class

    /// <summary>
    /// Class containing constants required for Grid.
    /// </summary>
    public static class ConstClass
    {
        #region Public Constants

        public static int[] PageSizes = { 25, 50, 100, 200 };

        #endregion Public Constants
    }

    #endregion Class

    #region Enumeration

    // Text representation of boolean value.
    public enum BoolYesNo
    {
        /// <summary>
        /// Represents boolean false. Has value 0.
        /// </summary>
        No,

        /// <summary>
        /// Represents boolean true. Has value 1.
        /// </summary>
        Yes
    }

    /// <summary>
    /// Enum describing type of grid action behaviour.
    /// </summary>
    public enum GridActionBehaviour
    {
        /// <summary>
        /// Open given action in new window as a modal window.
        /// </summary>
        Popup,

        /// <summary>
        /// Post to server in the background.
        /// </summary>
        PostSilent,

        /// <summary>
        /// Redirect user to the given Url on current page.
        /// </summary>
        Redirect
    }
    
    /// <summary>
    /// Enum describing types of grid columns.
    /// </summary>
    public enum GridColumnType
    {
        /// <summary>
        /// Column containing check box. Could be used to select row.
        /// </summary>
        CheckBox,

        /// <summary>
        /// Image column.
        /// </summary>
        Image,
        
        /// <summary>
        /// Text link column.
        /// </summary>
        Link,
        
        /// <summary>
        /// Normal text column.
        /// </summary>
        Text
    }

    /// <summary>
    /// Enum representing condition to be applied between search criteria.
    /// </summary>
    public enum SearchCondition
    {
        /// <summary>
        /// The AND condition to be applied between search criterias
        /// </summary>
        And,

        /// <summary>
        /// The OR condition to be applied between search criterias
        /// </summary>
        Or
    }

    /// <summary>
    /// Enum specifying the sorting direction
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Ascending sort direction.
        /// </summary>
        Ascending,

        /// <summary>
        /// Descending sort direction.
        /// </summary>
        Descending
    }    

    #endregion Enumeration
}

namespace CashCow.Provider
{
    /// <summary>
    /// Generic data access helper class.
    /// </summary>
    public static class DataAccess
    {
        /// <summary>
        /// Centralized class containing list of parameters to be used in all DB transaction.
        /// </summary>
        public static class Params
        {
            public const string ALERT_REQUIRED = "AlertRequired";
            public const string ALT_NAME_ONE = "AltNameOne";
            public const string ALT_NAME_THREE = "AltNameThree";
            public const string ALT_NAME_TWO = "AltNameTwo";
            public const string BSE_SYMBOL = "BseSymbol";
            public const string CREATED_ON = "CreatedOn";
            public const string IS_ACTIVE = "IsActive";
            public const string MAXIMUM_ROWS = "MaximumRows";
            public const string MODIFIED_ON = "ModifiedOn";
            public const string NAME = "Name";
            public const string NSE_SYMBOL = "NseSymbol";
            public const string RECORD_COUNT = "RecordCount";
            public const string SEARCH_AGAINST = "SearchAgainst";
            public const string SEARCH_CRITERIA = "SearchCriteria";
            public const string SEARCH_WITH_OR = "SearchWithOr";
            public const string SORT_ASCENDING = "SortAscending";
            public const string SORT_COLUMN = "SortColumn";
            public const string START_ROW_INDEX = "StartRowIndex";
            public const string TEMP_NAME = "TempName";
            public const string TEXT_SEARCH_KEY = "TextSearchKey";
            public const string WATCH_LIST_ID = "WatchListID";
        }

        /// <summary>
        /// Centralized class to list and manage all stored procedures.
        /// </summary>
        public static class StoredProcedure
        {
            /// <summary>
            /// Class containing stored procedures with schema dbo.
            /// </summary>
            public static class Dbo
            {
                public const string WATCH_LIST_ITEM_DELETE = "dbo.WatchListItem_Delete";
                public const string WATCH_LIST_ITEM_SAVE = "dbo.WatchListItem_Save";
                public const string WATCH_LIST_SEARCH = "dbo.WatchList_Search";
            }
        }
    }
}

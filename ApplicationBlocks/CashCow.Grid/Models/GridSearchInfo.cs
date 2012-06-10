#region Namespaces

using System.Collections.Generic;
using Helpers;

#endregion Namespaces

namespace CashCow.Grid.Models
{
    /// <summary>
    /// Class representing grid search info. At present, scope of search on grid is limited to only the result displayed in the grid.
    /// All properties except "TextSearchKey" contains values from "CashCow.Entity.GridSearchCriteria" which is set through the search page.
    /// Hence at present only "TextSearchKey" could be set from client and it will contain value from the grid's search text box.
    /// </summary>
    public class GridSearchInfo
    {
        #region Private Data

        private SearchCondition _searchCondition = SearchCondition.Or;
        private IList<KeyValuePair<string, string>> _searchCriteriaList = new List<KeyValuePair<string, string>>();

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// Flag representing if search has to be done for or against the search criteria.
        /// </summary>
        public bool SearchAgainstCriteria { get; set; }

        /// <summary>
        /// Search criteria containing search field and search value in as name value pair list.
        /// Name has DB column name and value the search string.
        /// </summary>
        public IList<KeyValuePair<string, string>> SearchCriteriaList
        { 
            get
            {
                return this._searchCriteriaList;
            }

            set
            {
                this._searchCriteriaList = value;
            }
        }
        
        /// <summary>
        /// Search condition to be applied between search criterias.
        /// Default value = true.
        /// </summary>
        public bool SearchWithOr
        {
            get
            {
                return (this._searchCondition == SearchCondition.Or) ? true : false;
            }

            set
            {
                this._searchCondition = (value) ? SearchCondition.Or : SearchCondition.And;
            }
        }

        /// <summary>
        /// The key to be searched on all text columns. This is generally implemented on the result returned after search criteria is implemented.
        /// </summary>
        public string TextSearchKey { get; set; }

        #endregion Public Properties
    }
}
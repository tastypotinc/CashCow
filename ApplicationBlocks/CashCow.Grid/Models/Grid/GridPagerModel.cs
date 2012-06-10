#region Namespaces

using System.Collections.Generic;
using Helpers;

#endregion Namespaces

namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Model class for grid pager.
    /// </summary>
    public class GridPagerModel
    {
        #region Private Data

        private string _cssClass = "gridPagerRow";
        private int _currPagerElementSetNumber = 1;
        private int _numberOfPagerElements = ConfigHelper.DefaultGridPagerElements;
        private int _pageSize = ConfigHelper.DefaultGridPageSize;
        private int _totalRecord = ConfigHelper.DefaultGridPageSize;

        #endregion Private Data

        #region Public Properties

        /// <summary>
        /// Collection of page sizes available. Read-only.
        /// </summary>
        public int[] AllPageSizes
        {
            get
            {
                return ConstClass.PageSizes;
            }
        }

        /// <summary>
        /// The CSS class for grid pager row.
        /// Default value = gridPagerRow.
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
        /// Current page's zero based index id.
        /// </summary>
        public int CurrPageId { get; set; }

        /// <summary>
        /// Current page number. Read-only.
        /// </summary>
        public int CurrPageNumber
        {
            get
            {
                return this.CurrPageId + 1;
            }
        }

        /// <summary>
        /// Current pager element set number.
        /// </summary>
        public int CurrPagerElementSetNumber
        {
            get
            {
                return this._currPagerElementSetNumber;
            }

            set
            {
                this._currPagerElementSetNumber = value;
            }
        }

        /// <summary>
        /// Number of pager elements to be shown at a time.
        /// Default value = ConfigHelper.DefaultGridPagerElements.
        /// </summary>
        public int NumberOfPagerElements
        {
            get
            {
                return this._numberOfPagerElements;
            }

            set
            {
                this._numberOfPagerElements = value;
            }
        }

        /// <summary>
        /// Array of pager elements for current pager element set. Read-only.
        /// </summary>
        public int[] PagerElements
        {
            get
            {
                var pagerElements = new List<int>();
                int pagerElementToAdd;

                for (int loop = 1; loop <= this.NumberOfPagerElements; loop++)
                {
                    pagerElementToAdd = (this.CurrPagerElementSetNumber - 1) * this.NumberOfPagerElements + loop;

                    // Add element only if it not more then the total number of pages.
                    if (pagerElementToAdd <= this.TotalPages)
                    {
                        pagerElements.Add(pagerElementToAdd);
                    }
                }

                return pagerElements.ToArray();
            }
        }

        /// <summary>
        /// List of pager element sets. This is list of key value pair. Key has first and last page and key has current element set number.
        /// Read-only.
        /// </summary>
        public IList<KeyValuePair<string, int>> PagerElementSetList
        {
            get
            {
                string key;
                var pagerElementSetList = new List<KeyValuePair<string, int>>();

                for (int loop = 0; loop < this.TotalPagerElementSets; loop++)
                {
                    // The key will have first and last page numbers in the set.
                    key = (loop * this.NumberOfPagerElements + 1) + " ... " + (loop + 1) * this.NumberOfPagerElements;

                    pagerElementSetList.Add(new KeyValuePair<string, int>(key,loop + 1));
                }

                return pagerElementSetList;
            }
        }

        /// <summary>
        /// Current page size of the grid.
        /// Default value = ConfigHelper.DefaultGridPageSize.
        /// </summary>
        public int PageSize
        {
            get
            {
                return this._pageSize;
            }

            set
            {
                this._pageSize = value;
            }
        }

        /// <summary>
        /// Index of first database row index that has to be returned for the current page. Read-only.
        /// </summary>
        public int StartRowIndex
        {
            get
            {
                return (this.PageSize * this.CurrPageId);
            }
        }

        /// <summary>
        /// Total number pager number sets. Read-only.
        /// </summary>
        public int TotalPagerElementSets
        {
            get
            {
                return (this.TotalPages % this.NumberOfPagerElements == 0)
                           ? this.TotalPages / this.NumberOfPagerElements
                           : this.TotalPages / this.NumberOfPagerElements + 1;
            }
        }

        /// <summary>
        /// Total number of pages. Read-only.
        /// </summary>
        public int TotalPages
        {
            get
            {
                return (this._totalRecord % this._pageSize == 0)
                           ? this._totalRecord / this._pageSize
                           : this._totalRecord / this._pageSize + 1;
            }
        }

        /// <summary>
        /// Total number of records.
        /// Default value = ConfigHelper.DefaultGridPageSize.
        /// </summary>
        public int TotalRecord
        {
            get
            {
                return this._totalRecord;
            }

            set
            {
                this._totalRecord = value;
            }
        }

        #endregion Public Properties
    }
}
#region Namespaces

using System.Collections.Generic;
using System.Linq;
using System.Xml;
using CashCow.Entity;
using CashCow.Grid.Models;
using CashCow.Grid.Models.Grid;
using Helpers;

#endregion Namespaces

namespace CashCow.Grid.Helper
{
    /// <summary>
    /// A helper class to build grid model.
    /// </summary>
    public class GridModelBuilder
    {
        #region Private Methods

        /// <summary>
        /// Method to build grid body model using deep copy.
        /// </summary>
        /// <typeparam name="T">Model entity.</typeparam>
        /// <param name="gridModelBuilderEntity">Contains all required config info for grid construction.</param>
        /// <param name="dataSource">List of data to be displayed in the grid.</param>
        /// <returns>GridBodyModel.</returns>
        private GridBodyModel BuildGridBodyModel<T>(GridModelBuilderEntity gridModelBuilderEntity, IEnumerable<T> dataSource) where T : class
        {
            var gridBodyModel = new GridBodyModel();

            // Build body only if dataSource has data.
            if (dataSource != null && dataSource.Count() > 0)
            {
                // Iterate through dataSource to build rows.
                for (int rowNum = 0; rowNum < dataSource.Count(); rowNum++)
                {
                    var gridRowModel = new GridRowModel();

                    // Iterate through list of columns to build cells.
                    if (gridModelBuilderEntity.Columns != null && gridModelBuilderEntity.Columns.Count > 0)
                    {
                        foreach (var column in gridModelBuilderEntity.Columns)
                        {
                            gridRowModel.Cells.Add(this.BuildGridRowCellModel(column, dataSource.ElementAt(rowNum), rowNum));
                        }
                    }

                    // Add the above constructed row to body.
                    gridBodyModel.Rows.Add(gridRowModel);
                }

                // Assign common row properties to body.
                if (!string.IsNullOrEmpty(gridModelBuilderEntity.GridBodyRowProperty.CssClass))
                {
                    gridBodyModel.CssClass = gridModelBuilderEntity.GridBodyRowProperty.CssClass;
                }
            }

            return gridBodyModel;
        }

        /// <summary>
        /// Method to build grid context for the grid using deep copy.
        /// </summary>
        /// <param name="gridModelBuilderEntity">Contains all required config info for grid construction.</param>
        /// <param name="gridDefaultAction">The default action name for the grid.</param>
        /// <returns>GridContext.</returns>
        private GridContext BuildGridContext(GridModelBuilderEntity gridModelBuilderEntity, string gridDefaultAction)
        {
            // Build grid pager objec
            var gridPagerModel = new GridPagerModel();
            gridPagerModel.CssClass = gridModelBuilderEntity.GridContext.GridPager.CssClass;
            gridPagerModel.CurrPageId = gridModelBuilderEntity.GridContext.GridPager.CurrPageId;
            gridPagerModel.PageSize = gridModelBuilderEntity.GridContext.GridPager.PageSize;
            gridPagerModel.TotalRecord = gridModelBuilderEntity.GridContext.GridPager.TotalRecord;
            gridPagerModel.NumberOfPagerElements = gridModelBuilderEntity.GridContext.GridPager.NumberOfPagerElements;

            // Build grid search info object.
            var gridSearchInfo = new GridSearchInfo();
            gridSearchInfo.SearchAgainstCriteria = gridModelBuilderEntity.GridContext.SearchInfo.SearchAgainstCriteria;
            gridSearchInfo.SearchCriteriaList = new List<KeyValuePair<string, string>>(gridModelBuilderEntity.GridContext.SearchInfo.SearchCriteriaList);
            gridSearchInfo.SearchWithOr = gridModelBuilderEntity.GridContext.SearchInfo.SearchWithOr;
            gridSearchInfo.TextSearchKey = gridModelBuilderEntity.GridContext.SearchInfo.TextSearchKey;

            // Build grid sort info object.
            var gridSortInfo = new GridSortInfo();
            gridSortInfo.SortOn = gridModelBuilderEntity.GridContext.SortInfo.SortOn;
            gridSortInfo.SortOrder = gridModelBuilderEntity.GridContext.SortInfo.SortOrder;

            // Build grid context
            var gridContext = new GridContext();
            gridContext.AlternateRowCss = gridModelBuilderEntity.GridContext.AlternateRowCss;
            gridContext.CssClass = gridModelBuilderEntity.GridContext.CssClass;
            gridContext.DefaultAction = gridDefaultAction;
            gridContext.DisableGridSearch = gridModelBuilderEntity.GridContext.DisableGridSearch;
            gridContext.RowCss = gridModelBuilderEntity.GridContext.RowCss;
            gridContext.GridPager = gridPagerModel;
            gridContext.SearchInfo = gridSearchInfo;
            gridContext.SortInfo = gridSortInfo;

            return gridContext;
        }

        /// <summary>
        /// Method to build grid header model for the grid using deep copy.
        /// </summary>
        /// /// <typeparam name="T">Model entity.</typeparam>
        /// <param name="gridModelBuilderEntity">Contains all required config info for grid construction.</param>
        /// <param name="dataEntity">Model entity containing data to be displayed.</param>
        /// <returns>GridHeaderModel.</returns>
        private GridHeaderModel BuildGridHeaderModel<T>(GridModelBuilderEntity gridModelBuilderEntity, T dataEntity)
        {
            var gridHeaderModel = new GridHeaderModel();

            // Build header only if dataSource has data.
            if (dataEntity != null)
            {
                var dataEntityType = dataEntity.GetType();

                // Build list of header cells by iterating through list of columns.
                if (gridModelBuilderEntity.Columns != null && gridModelBuilderEntity.Columns.Count > 0)
                {
                    foreach (var column in gridModelBuilderEntity.Columns)
                    {
                        var gridHeaderCellModel = new GridHeaderCellModel();

                        gridHeaderCellModel.CssClass = column.HeaderCell.CssClass;
                        gridHeaderCellModel.BindingColumnName = column.HeaderCell.BindingColumnName;
                        gridHeaderCellModel.ColumnType = column.HeaderCell.ColumnType;
                        gridHeaderCellModel.IsDisabled = column.HeaderCell.IsDisabled;
                        gridHeaderCellModel.Label = column.HeaderCell.Label;
                        gridHeaderCellModel.Width = column.HeaderCell.Width;

                        // Now set AllowSorting on the column only if the column type is defined as text, image or link.
                        // A text column can represent boolean data as well. For boolean data we have standard text and it is so chosen that
                        // sorting will yield as result as sorting based on the boolean data it represents.
                        // For image type column sorting will be allowed only if it being binded to boolean type datasource. For boolean data we
                        // have standard image and its name is so kept that sorting based on image path will be same as sorting based on boolean
                        // data that it represents. This care should be taken when renaming boolean images.
                        // For link there is no column binding hence allow sorting only if SortColumnName is mentioned.
                        if (column.HeaderCell.ColumnType == GridColumnType.Text ||
                            (column.HeaderCell.ColumnType == GridColumnType.Image &&
                            (dataEntityType.GetProperty(column.HeaderCell.BindingColumnName).PropertyType.Name.Equals("Bool") ||
                            dataEntityType.GetProperty(column.HeaderCell.BindingColumnName).PropertyType.Name.Equals("Boolean"))) ||
                            (column.HeaderCell.ColumnType == GridColumnType.Link && !string.IsNullOrEmpty(column.HeaderCell.SortColumnName)))
                        {
                            gridHeaderCellModel.AllowSorting = column.HeaderCell.AllowSorting;
                            gridHeaderCellModel.SortColumnName = column.HeaderCell.SortColumnName;
                        }
                        else
                        {
                            gridHeaderCellModel.AllowSorting = false;
                        }

                        gridHeaderModel.Cells.Add(gridHeaderCellModel);
                    }
                }

                if (!string.IsNullOrEmpty(gridModelBuilderEntity.GridHeaderRowProperty.CssClass))
                {
                    gridHeaderModel.CssClass = gridModelBuilderEntity.GridHeaderRowProperty.CssClass;
                }
            }

            return gridHeaderModel;
        }

        /// <summary>
        /// Method to build body cell models using reflection.
        /// </summary>
        /// <typeparam name="T">Model entity.</typeparam>
        /// <param name="gridColumnModel">Column model containing header and body cell configuration details.</param>
        /// <param name="dataEntity">Model entity containing data to be displayed.</param>
        /// <param name="rowNum">Zero based index row number for which cell has to be build.</param>
        /// <returns>GridRowCellModel.</returns>
        private GridRowCellModel BuildGridRowCellModel<T>(GridColumnModel gridColumnModel, T dataEntity, int rowNum)
        {
            var gridRowCellModel = new GridRowCellModel();

            // Get dataEntity type. To be used later to extract data to be displayed.
            var dataEntityProperty = dataEntity.GetType().GetProperty(gridColumnModel.HeaderCell.BindingColumnName);

            // Build different cells based on header column type.
            switch (gridColumnModel.HeaderCell.ColumnType)
            {
                case GridColumnType.Image:
                    // Proceed only if this column has any valid datasource column binded to it.
                    if (dataEntityProperty != null)
                    {
                        // If data type be bool then take image path from configuration.
                        if (dataEntityProperty.PropertyType.Name.Equals("Bool") || dataEntityProperty.PropertyType.Name.Equals("Boolean"))
                        {
                            gridRowCellModel.ImagePath = ((bool)dataEntityProperty.GetValue(dataEntity, null))
                                                             ? ConfigHelper.GetBooleanImage(true)
                                                             : ConfigHelper.GetBooleanImage(false);
                        }
                        // Else take image path from data source.
                        else
                        {
                            gridRowCellModel.ImagePath = dataEntityProperty.GetValue(dataEntity, null).ToString();
                        }
                    }
                    break;

                case GridColumnType.Link:
                    // Simply choose the list of links for the cell as per current row number.    
                    if (gridColumnModel.Links != null && gridColumnModel.Links.Count > 0)
                    {
                        gridRowCellModel.Links = new List<GridLinkModel>(gridColumnModel.Links.ElementAt(rowNum));
                    }
                    break;

                case GridColumnType.Text:
                    // Proceed only if this column has any valid datasource column binded to it.
                    if (dataEntityProperty != null)
                    {
                        // If data type be bool then set text value as Yes or No.
                        if (dataEntityProperty.PropertyType.Name.Equals("Bool") || dataEntityProperty.PropertyType.Name.Equals("Boolean"))
                        {
                            gridRowCellModel.Text = ((bool)dataEntityProperty.GetValue(dataEntity, null))
                                                        ? BoolYesNo.Yes.ToString()
                                                        : BoolYesNo.No.ToString();
                        }
                        // Else set text from data source.
                        else
                        {
                            if(dataEntityProperty.GetValue(dataEntity, null) != null)
                            {
                                gridRowCellModel.Text = dataEntityProperty.GetValue(dataEntity, null).ToString();
                            }
                        }
                    }
                    break;

                default:
                    break;
            }

            // Set the column type and disable flag.
            gridRowCellModel.ColumnType = gridColumnModel.HeaderCell.ColumnType;
            gridRowCellModel.IsDisabled = gridColumnModel.HeaderCell.IsDisabled;

            // Set general properties of cell.
            if (!string.IsNullOrEmpty(gridColumnModel.BodyCellProperty.CssClass))
            {
                gridRowCellModel.CssClass = gridColumnModel.BodyCellProperty.CssClass;
            }

            return gridRowCellModel;
        }

        /// <summary>
        /// Method to convert the grid context search criteria list to search criteria for grid in XML format.
        /// </summary>
        /// <param name="searchCriteriaList">Name value pair list of search criteria from grid context.</param>
        /// <returns>Search criteria string in XML format.
        /// Format is <SearchCriteria><Criteria SearchOn="ColumnName" SearchValue="SearchString" /></SearchCriteria></returns>
        private string ConvertContextSearchCriteriaListToGridSearchCriteria(IList<KeyValuePair<string, string>> searchCriteriaList)
        {
            var xmlDoc = new XmlDocument();
            var root = xmlDoc.CreateElement("SearchCriteria");

            foreach (var searchCriteria in searchCriteriaList)
            {
                var xmlElement = xmlDoc.CreateElement("Criteria");
                xmlElement.SetAttribute("SearchOn", searchCriteria.Key);
                xmlElement.SetAttribute("SearchValue", searchCriteria.Value);
                root.AppendChild(xmlElement);
            }

            xmlDoc.AppendChild(root);

            return xmlDoc.InnerXml;
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Method to build model for grid.
        /// </summary>
        /// <typeparam name="T">Entity type whose list has to be displayed.</typeparam>
        /// <param name="gridModelBuilderEntity">Contains all required grid configuration info.</param>
        /// <param name="dataSource">List of entities that has to be displayed.</param>
        /// <param name="gridDefaultAction">The default action name for the grid.</param>
        /// <returns>GridModel containing grid config info and data.</returns>
        public GridModel BuildGridModel<T>(GridModelBuilderEntity gridModelBuilderEntity, IEnumerable<T> dataSource, string gridDefaultAction) where T : class
        {
            var gridModel = new GridModel();

            gridModel.GridContext = this.BuildGridContext(gridModelBuilderEntity, gridDefaultAction);
            gridModel.GridHeader = this.BuildGridHeaderModel(gridModelBuilderEntity, dataSource.FirstOrDefault());
            gridModel.GridBodyModel = this.BuildGridBodyModel(gridModelBuilderEntity, dataSource);

            return gridModel;
        }

        /// <summary>
        /// Method to create GridSearchCriteriaEntity from grid context.
        /// </summary>
        /// <param name="gridContext">Grid context of the grid.</param>
        /// <returns>GridSearchCriteriaEntity</returns>
        public GridSearchCriteriaEntity CreateGridSearchCriteriaEntity(GridContext gridContext)
        {
            return new GridSearchCriteriaEntity
                       {
                           MaximumRows = gridContext.GridPager.PageSize,
                           SearchAgainst = gridContext.SearchInfo.SearchAgainstCriteria,
                           SearchCriteria = this.ConvertContextSearchCriteriaListToGridSearchCriteria(gridContext.SearchInfo.SearchCriteriaList),
                           SearchWithOr = gridContext.SearchInfo.SearchWithOr,
                           SortAscending = gridContext.SortInfo.SortAscending,
                           SortColumn = gridContext.SortInfo.SortOn,
                           StartRowIndex = gridContext.GridPager.StartRowIndex,
                           TextSearchKey = gridContext.SearchInfo.TextSearchKey
                       };
        }

        #endregion Public Methods
    }
}

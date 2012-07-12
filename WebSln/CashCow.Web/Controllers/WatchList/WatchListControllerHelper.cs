#region Namespaces

using System.Collections.Generic;
using System.Linq;
using System.Xml;
using CashCow.Business;
using CashCow.BusinessInterface;
using CashCow.Entity;
using CashCow.Grid.Helper;
using CashCow.Grid.Models.Grid;
using CashCow.Web.Models.WatchList;
using Helpers;

#endregion Namespaces

namespace CashCow.Web.Controllers.WatchList
{
    /// <summary>
    /// Controller helper class for WatchListController.
    /// </summary>
    public partial class WatchListController
    {
        #region Private Methods

        /// <summary>
        /// Method to convert the grid context search criteria list to search criteria for grid in XML format.
        /// </summary>
        /// <param name="searchCriteriaList">Name value pair list of search criteria from grid context.</param>
        /// <returns>Search criteria string in XML format.
        /// Format is <SearchCriteria><Criteria SearchOn="ColumnName" SearchValueOne="SearchStringStart" SearchValueTwo="SearchStringEnd" /></SearchCriteria></returns>
        private string ConvertContextSearchCriteriaListToGridSearchCriteria(IList<KeyValuePair<string, string>> searchCriteriaList)
        {
            var xmlDoc = new XmlDocument();
            var root = xmlDoc.CreateElement("SearchCriteria");
            var tempRoot = xmlDoc.CreateElement("Temp");
            XmlElement xmlElement;

            foreach (var searchCriteria in searchCriteriaList)
            {
                xmlElement = xmlDoc.CreateElement("Criteria");
                xmlElement.SetAttribute("SearchOn", searchCriteria.Key);
                xmlElement.SetAttribute("SearchValueOne", searchCriteria.Value);
                xmlElement.SetAttribute("SearchValueTwo", string.Empty);

                // If Key has particular value, add it to temporary root element.
                // They will be inserted into root element in a different way below.
                if (searchCriteria.Key.Equals("CreatedOnStart") ||
                    searchCriteria.Key.Equals("CreatedOnEnd") ||
                    searchCriteria.Key.Equals("ModifiedOnStart") ||
                    searchCriteria.Key.Equals("ModifiedOnEnd"))
                {
                    tempRoot.AppendChild(xmlElement);
                }
                else
                {
                    root.AppendChild(xmlElement);
                }
            }

            XmlNode nodeOne;
            XmlNode nodeTwo;

            // Create search criteria for created date. Get CreatedOnStart and CreatedOnEnd nodes.
            // Construct a single node, CreatedOn and put both values in it.
            nodeOne = tempRoot.SelectSingleNode("Criteria[@SearchOn='CreatedOnStart']");
            nodeTwo = tempRoot.SelectSingleNode("Criteria[@SearchOn='CreatedOnEnd']");
            if(nodeOne != null && nodeTwo != null)
            {
                xmlElement = xmlDoc.CreateElement("Criteria");
                xmlElement.SetAttribute("SearchOn", "CreatedOn");
                xmlElement.SetAttribute("SearchValueOne", nodeOne.Attributes["SearchValueOne"].Value);
                xmlElement.SetAttribute("SearchValueTwo", nodeTwo.Attributes["SearchValueOne"].Value);
                root.AppendChild(xmlElement);
            }

            // Create search criteria for modified date. Get ModifiedOnStart and ModifiedOnEnd nodes.
            // Construct a single node, ModifiedOn and put both values in it.
            nodeOne = tempRoot.SelectSingleNode("Criteria[@SearchOn='ModifiedOnStart']");
            nodeTwo = tempRoot.SelectSingleNode("Criteria[@SearchOn='ModifiedOnEnd']");
            if (nodeOne != null && nodeTwo != null)
            {
                xmlElement = xmlDoc.CreateElement("Criteria");
                xmlElement.SetAttribute("SearchOn", "ModifiedOn");
                xmlElement.SetAttribute("SearchValueOne", nodeOne.Attributes["SearchValueOne"].Value);
                xmlElement.SetAttribute("SearchValueTwo", nodeTwo.Attributes["SearchValueOne"].Value);
                root.AppendChild(xmlElement);
            }

            xmlDoc.AppendChild(root);

            return xmlDoc.InnerXml;
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

        /// <summary>
        /// Method to create search criteria name value pair list for watch list grid. Name has DB column name and value the search string.
        /// </summary>
        /// <param name="watchListSearchModel">The WatchListSearchModel from view.</param>
        /// <returns>Search criteria name value pair list.</returns>
        private IList<KeyValuePair<string, string>> CreateSearchCriteriaListForWatchListGrid(WatchListSearchModel watchListSearchModel)
        {
            var searchCriteriaListForWatchListGrid = new List<KeyValuePair<string, string>>();

            // Create a criteria name value pair for each model field.
            if (watchListSearchModel.AlertRequired != null)
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("AlertRequired",
                    (bool)watchListSearchModel.AlertRequired ? "1" : "0"));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.AltNameOne))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("AltNameOne", watchListSearchModel.AltNameOne));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.AltNameThree))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("AltNameThree", watchListSearchModel.AltNameThree));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.AltNameTwo))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("AltNameTwo", watchListSearchModel.AltNameTwo));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.BseSymbol))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("BseSymbol", watchListSearchModel.BseSymbol));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.CreatedOnStart))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("CreatedOnStart",
                    DataFormatter.GetDateTimeInUtcFormat(DataFormatter.FormatStringToDate(watchListSearchModel.CreatedOnStart)).ToString()));
            }
            else if (!string.IsNullOrEmpty(watchListSearchModel.CreatedOnEnd))
            {
                // If only end date is specified, start date will be begining of the end date i.e. 0 hr of end date.
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("CreatedOnStart",
                    DataFormatter.GetDateTimeInUtcFormat(DataFormatter.FormatStringToDate(watchListSearchModel.CreatedOnEnd)).ToString()));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.CreatedOnEnd))
            {
                // End date will be end of end date i.e. 2400 hrs of end date or 0 hrs of next day to end date.
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("CreatedOnEnd",
                    DataFormatter.GetDateTimeInUtcFormat(DataFormatter.FormatStringToDate(watchListSearchModel.CreatedOnEnd).Value.AddDays(1)).ToString()));
            }
            else if (!string.IsNullOrEmpty(watchListSearchModel.CreatedOnStart))
            {
                // If only start date is specified, end date will be end of start date i.e. 2400 hrs of start date or 0 hr of next day to start date.
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("CreatedOnEnd",
                    DataFormatter.GetDateTimeInUtcFormat(DataFormatter.FormatStringToDate(watchListSearchModel.CreatedOnStart).Value.AddDays(1)).ToString()));
            }

            if (watchListSearchModel.IsActive != null)
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("IsActive",
                    (bool)watchListSearchModel.IsActive ? "1" : "0"));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.ModifiedOnStart))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("ModifiedOnStart",
                     DataFormatter.GetDateTimeInUtcFormat(DataFormatter.FormatStringToDate(watchListSearchModel.ModifiedOnStart)).ToString()));
            }
            else if (!string.IsNullOrEmpty(watchListSearchModel.ModifiedOnEnd))
            {
                // If only end date is specified, start date will be begining of the end date i.e. 0 hr of end date.
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("ModifiedOnStart",
                     DataFormatter.GetDateTimeInUtcFormat(DataFormatter.FormatStringToDate(watchListSearchModel.ModifiedOnEnd)).ToString()));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.ModifiedOnEnd))
            {
                // End date will be end of end date i.e. 2400 hrs of end date or 0 hrs of next day to end date.
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("ModifiedOnEnd",
                     DataFormatter.GetDateTimeInUtcFormat(DataFormatter.FormatStringToDate(watchListSearchModel.ModifiedOnEnd).Value.AddDays(1)).ToString()));
            }
            else if (!string.IsNullOrEmpty(watchListSearchModel.ModifiedOnStart))
            {
                // If only start date is specified, end date will be end of start date i.e. 2400 hrs of start date or 0 hr of next day to start date.
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("ModifiedOnEnd",
                     DataFormatter.GetDateTimeInUtcFormat(DataFormatter.FormatStringToDate(watchListSearchModel.ModifiedOnStart).Value.AddDays(1)).ToString()));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.Name))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("Name", watchListSearchModel.Name));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.NseSymbol))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("NseSymbol", watchListSearchModel.NseSymbol));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.TempName))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("TempName", watchListSearchModel.TempName));
            }

            return searchCriteriaListForWatchListGrid;
        }

        /// <summary>
        /// Method to return GridModel for watchlist grid.
        /// </summary>
        /// <param name="gridContext">The grid context containing search, sort and paging info.</param>
        /// <returns>The grid model required for grid construction.</returns>
        private GridModel CreateWatchListGridModel(GridContext gridContext)
        {
            var gridModelBuilder = new GridModelBuilder();

            // Create grid search criteria from the grid context and retrieve list of watch list entities from the DB.
            var gridSearchCriteria = this.CreateGridSearchCriteriaEntity(gridContext);

            IWatchListBusiness iWatchListBusiness = new WatchListBusiness();

            var watchListEntities = iWatchListBusiness.SearchWatchList(gridSearchCriteria, 0);
            var watchListModels = watchListEntities.Select(WatchListModel.ConvertWatchListEntityToModel).ToList();

            // Grid context is already available. Just set the number of records in it.
            gridContext.GridPager.TotalRecord = gridSearchCriteria.RecordCount;

            // Create list of columns in the grid.
            var columns = new List<GridColumnModel>
                              {
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               BindingColumnName = "Name",
                                                               Label = "Name"
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               BindingColumnName = "BseSymbol",
                                                               Label = "BSE Symbol"
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               BindingColumnName = "NseSymbol",
                                                               Label = "NSE Symbol"
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               BindingColumnName = "AltNameOne",
                                                               Label = "Alt Name One"
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               BindingColumnName = "AltNameTwo",
                                                               Label = "Alt Name Two"
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               BindingColumnName = "AltNameThree",
                                                               Label = "Alt Name Three"
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               BindingColumnName = "TempName",
                                                               Label = "Temp Name"
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               ColumnType = GridColumnType.Link,
                                                               Label = "Is Active?",
                                                               SortColumnName = "IsActive"
                                                           },
                                          Links = watchListModels.Select(x => new List<GridLinkModel>
                                                                                  {
                                                                                      new GridLinkModel
                                                                                          {
                                                                                              Action = "WatchList/ChangeActiveStatus/" + x.WatchListID,
                                                                                              Behaviour = GridActionBehaviour.PostSilent,
                                                                                              ImagePath = ConfigHelper.GetBooleanImage(x.IsActive)
                                                                                          }
                                                                                  }).ToList()
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               ColumnType = GridColumnType.Link,
                                                               Label = "Is Alert Required?",
                                                               SortColumnName = "AlertRequired"
                                                           },
                                          Links = watchListModels.Select(x => new List<GridLinkModel>
                                                                                  {
                                                                                      new GridLinkModel
                                                                                          {
                                                                                              Action = "WatchList/ChangeAlertStatus/" + x.WatchListID,
                                                                                              Behaviour = GridActionBehaviour.PostSilent,
                                                                                              ImagePath = ConfigHelper.GetBooleanImage(x.AlertRequired)
                                                                                          }
                                                                                  }).ToList()
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               BindingColumnName = "ModifiedOn",
                                                               Label = "Modified On"
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               BindingColumnName = "CreatedOn",
                                                               Label = "Created On"
                                                           }
                                      },
                                      new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               ColumnType = GridColumnType.Link
                                                           },
                                          Links = watchListModels.Select(x => new List<GridLinkModel>
                                                                                  {
                                                                                      new GridLinkModel
                                                                                          {
                                                                                              Action = "WatchList/EditWatchList/" + x.WatchListID,
                                                                                              Behaviour = GridActionBehaviour.Popup,
                                                                                              Text = "Edit"
                                                                                          },
                                                                                      new GridLinkModel
                                                                                          {
                                                                                              Action = "WatchList/DeleteWatchList/" + x.WatchListID,
                                                                                              Behaviour = GridActionBehaviour.PostSilent,
                                                                                              Text = "Delete"
                                                                                          }
                                                                                  }).ToList()
                                      }
                              };

            // Create grid model builder entity.
            var gridModelBuilderEntity = new GridModelBuilderEntity
            {
                Columns = columns,
                GridContext = gridContext
            };

            // Build the grid context to be returned.
            return gridModelBuilder.BuildGridModel(gridModelBuilderEntity, watchListModels, Url.Action(_DefaultGridAction));
        }

        /// <summary>
        /// Method to delete a watch list entity.
        /// </summary>
        /// <param name="watchListId">Id of the watchlist item to be deleted.</param>
        /// <returns>Id of the watch list item that has been deleted.</returns>
        private int DeleteWatchListItem(int watchListId)
        {
            IWatchListBusiness iWatchListBusiness = new WatchListBusiness();

            return iWatchListBusiness.DeleteWatchListItem(watchListId);
        }

        /// <summary>
        /// Method to fetch watch list entity model based on watch list ID.
        /// </summary>
        /// <param name="watchListId">Id of watch list entity to be fetched.</param>
        /// <returns>Watch list model corresponding to the model.</returns>
        private WatchListModel GetWatchListEntityModel(int watchListId)
        {
            IWatchListBusiness iWatchListBusiness = new WatchListBusiness();

            var watchListEntities = iWatchListBusiness.SearchWatchList(new GridSearchCriteriaEntity(), watchListId);
            return WatchListModel.ConvertWatchListEntityToModel(watchListEntities[0]);
        }

        /// <summary>
        /// Method to save watch list entity.
        /// </summary>
        /// <param name="watchListModel">The watch list model to be saved.</param>
        /// <returns>Id of the watch list item that has been saved.</returns>
        private int SaveWatchListItem(WatchListModel watchListModel)
        {
            IWatchListBusiness iWatchListBusiness = new WatchListBusiness();

            return iWatchListBusiness.SaveWatchListItem(WatchListModel.ConvertModelToWatchListEntity(watchListModel));
        }

        #endregion Private Methods
    }
}
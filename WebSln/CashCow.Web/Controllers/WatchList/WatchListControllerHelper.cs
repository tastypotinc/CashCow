#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
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

            if (!string.IsNullOrEmpty(watchListSearchModel.CreatedOn))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("CreatedOn",
                    DataFormatter.GetDateTimeInUtcFormat(Convert.ToDateTime(watchListSearchModel.CreatedOn)).ToString()));
            }

            if (watchListSearchModel.IsActive != null)
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("IsActive",
                    (bool)watchListSearchModel.IsActive ? "1" : "0"));
            }

            if (!string.IsNullOrEmpty(watchListSearchModel.ModifiedOn))
            {
                searchCriteriaListForWatchListGrid.Add(new KeyValuePair<string, string>("ModifiedOn",
                     DataFormatter.GetDateTimeInUtcFormat(Convert.ToDateTime(watchListSearchModel.ModifiedOn)).ToString()));
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
            var gridSearchCriteria = gridModelBuilder.CreateGridSearchCriteriaEntity(gridContext);

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
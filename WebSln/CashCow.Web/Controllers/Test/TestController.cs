using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CashCow.Business;
using CashCow.BusinessInterface;
using CashCow.Entity;
using CashCow.Grid.Helper;
using CashCow.Grid.Models;
using CashCow.Grid.Models.Grid;
using CashCow.Web.Models.WatchList;
using CashCow.Web.MvcHelpers;
using Helpers;
using System.Linq;

namespace CashCow.Web.Controllers.Test
{
    public class TestController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            IWatchListBusiness iWatchListBusiness = new WatchListBusiness();
            var searchCriteria = new GridSearchCriteriaEntity();

            var watchListEntities = iWatchListBusiness.SearchWatchList(searchCriteria, 0);
            var watchListModels = watchListEntities.Select(x => WatchListModel.ConvertWatchListEntityToModel(x)).ToList();

            //watchListModels.Clear();

            var gridModel = this.GetGridModel(watchListModels, searchCriteria.RecordCount);

            ViewData["gridModel"] = gridModel;

            return View();
        }

        private GridModel GetGridModel(IList<WatchListModel> watchListModels, int recordCount)
        {

            var columns = new List<GridColumnModel>
                              {
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               ColumnType = GridColumnType.CheckBox,
                                                               IsDisabled = false
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               ColumnType = GridColumnType.Image,
                                                               BindingColumnName = "AlertRequired",
                                                               Label = "Alert Required?"
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               ColumnType = GridColumnType.Text,
                                                               BindingColumnName = "AlertRequired",
                                                               Label = "Alert Required?"
                                                           }
                                      },
                                  new GridColumnModel
                                      {
                                          HeaderCell = new GridHeaderCellModel
                                                           {
                                                               ColumnType = GridColumnType.Link,
                                                               Label = "Links"
                                                           },
                                          Links = watchListModels.Select(x => new List<GridLinkModel>
                                                                                  {
                                                                                      new GridLinkModel
                                                                                          {
                                                                                              Action = "test/test",
                                                                                              AlertMessage =
                                                                                                  "Are you sure?",
                                                                                              Behaviour =
                                                                                                  GridActionBehaviour.PostSilent,
                                                                                              ImagePath =
                                                                                                  ConfigHelper.
                                                                                                  GetBooleanImage(
                                                                                                      x.AlertRequired)
                                                                                          },
                                                                                      new GridLinkModel
                                                                                          {
                                                                                              Action = "http://google.com",
                                                                                              Behaviour = GridActionBehaviour.PostSilent,
                                                                                              Text = x.Name
                                                                                          }
                                                                                  }).ToList()
                                      }
                              };

            // Set the GridModelBuilderEntity
            var gridModelBuilderEntity = new GridModelBuilderEntity
                                             {
                                                 Columns = columns,
                                                 GridContext = new GridContext
                                                                   {
                                                                       SortInfo = new GridSortInfo
                                                                                      {
                                                                                          SortOn = "AlertRequired",
                                                                                          SortOrder = SortDirection.Descending
                                                                                      },
                                                                       SearchInfo = new GridSearchInfo
                                                                       {
                                                                           TextSearchKey = "Test search"
                                                                       }
                                                                   }
                                             };

            var gridModelBuilder = new GridModelBuilder();
            return gridModelBuilder.BuildGridModel(gridModelBuilderEntity, watchListModels, Url.Action(gridModelBuilderEntity.GridContext.DefaultAction));
        }

        [HttpGet]
        [ActionName("grid-post-action")]
        public JsonResult test([FromJson] GridContext gridContext)
        {
            IWatchListBusiness iWatchListBusiness = new WatchListBusiness();
            var searchCriteria = new GridSearchCriteriaEntity();

            var watchListEntities = iWatchListBusiness.SearchWatchList(searchCriteria, 0);
            var watchListModels = watchListEntities.Select(x => WatchListModel.ConvertWatchListEntityToModel(x)).ToList();

            watchListModels.RemoveAt(3);

            var gridModel = this.GetGridModel(watchListModels, searchCriteria.RecordCount);

            return Json(gridModel, JsonRequestBehavior.AllowGet);
        }
    }
}

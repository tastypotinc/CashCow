#region Namespaces

using System;
using System.Web.Mvc;
using CashCow.Grid.Models;
using CashCow.Grid.Models.Grid;
using CashCow.Web.Models.WatchList;
using CashCow.Web.MvcHelpers;

#endregion Namespaces

namespace CashCow.Web.Controllers.WatchList
{
    /// <summary>
    /// The controller class for WatchList.
    /// </summary>
    public partial class WatchListController : CcBaseController
    {
        #region Public Methods

        /// <summary>
        /// The action method to handle the change of status of watch list item.
        /// </summary>
        /// <param name="id">Id of the watch item whose status has to be changed.</param>
        /// <param name="gridContext">The current grid context.</param>
        /// <returns>Watch list grid model as JsonResult.</returns>
        [HttpPost]
        public JsonResult ChangeActiveStatus(int id, [FromJson]GridContext gridContext)
        {
            // Get model for watch list id.
            var watchListModel = this.GetWatchListEntityModel(id);

            // Simply reverse the status of the entity.
            watchListModel.IsActive = !watchListModel.IsActive;

            // Save the changes and return new grid model as JSON result if the save is successful.
            this.SaveWatchListItem(watchListModel);

            return Json(this.CreateWatchListGridModel(gridContext));
        }

        /// <summary>
        /// Action method to handle the change of alert status of watch list item.
        /// </summary>
        /// <param name="id">Id of the watch item whose alert status has to be changed.</param>
        /// <param name="gridContext">The current grid context.</param>
        /// <returns>Watch list grid model as JsonResult.</returns>
        [HttpPost]
        public JsonResult ChangeAlertStatus(int id, [FromJson]GridContext gridContext)
        {
            // Get model for watch list id.
            var watchListModel = this.GetWatchListEntityModel(id);

            // Simply reverse the status of the entity.
            watchListModel.AlertRequired = !watchListModel.AlertRequired;

            // Save the changes and return new grid model as JSON result if the save is successful.
            this.SaveWatchListItem(watchListModel);

            return Json(this.CreateWatchListGridModel(gridContext));
        }

        /// <summary>
        /// Action method to handle deletion of a watchlist item.
        /// </summary>
        /// <param name="id">Watch list item id to be deleted.</param>
        /// <param name="gridContext">The current grid context.</param>
        /// <returns>Watch list grid model as JsonResult.</returns>
        [HttpPost]
        public JsonResult DeleteWatchList(int id, [FromJson]GridContext gridContext)
        {
            // Delete and return new grid model as JSON result if the delete was successful.
            this.DeleteWatchListItem(id);

            return Json(this.CreateWatchListGridModel(gridContext));
        }

        /// <summary>
        /// The action method to handle the add and edit of watch list item. Handles GET.
        /// </summary>
        /// <param name="id">Id of the watch list item to be edited.</param>
        /// <returns>The watch list edit item view.</returns>
        [HttpGet]
        public ActionResult EditWatchList(int? id)
        {
            var watchListModel = new WatchListModel();

            // Fetch watchlist details if it is in edit mode else simply pass an empty model to the view.
            if (id != null && id.Value > 0)
            {
                watchListModel = this.GetWatchListEntityModel(id.Value);
            }

            return View("Edit", watchListModel);
        }

        /// <summary>
        /// The action method to handle the add and edit of watch list item. Handles POST.
        /// </summary>
        /// <returns>Watch list grid model as JsonResult.</returns>
        [HttpPost]
        public JsonResult EditWatchList(WatchListModel watchListModel)
        {
            // Set the date of creation and modification.
            if(watchListModel.WatchListID > 0)
            {
                watchListModel.ModifiedOn = DateTime.Now.ToString();
            }
            else
            {
                watchListModel.CreatedOn = DateTime.Now.ToString();
            }
            
            // Save the changes and return new grid model as JSON result if the save is successful.
            this.SaveWatchListItem(watchListModel);

            return Json(this.CreateWatchListGridModel(new GridContext {SortInfo = new GridSortInfo {SortOn = "Name"}}));
        }
        
        /// <summary>
        /// The action method to handle all grid specific action i.e. searching, sorting, paging.
        /// </summary>
        /// <param name="gridContext">The current grid context.</param>
        /// <returns>Watch list grid model as JsonResult</returns>
        [HttpPost]
        [ActionName("grid-post-action")]
        public JsonResult GridAction([FromJson] GridContext gridContext)
        {
            var gridModel = this.CreateWatchListGridModel(gridContext);

            return Json(gridModel);
        }

        /// <summary>
        /// Default action method for WatchListController. Handles GET.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var watchListSearchModel = new WatchListSearchModel();

            ViewData["gridModel"] = this.CreateWatchListGridModel(new GridContext{SortInfo = new GridSortInfo{SortOn = "Name"}});

            return View(watchListSearchModel);
        }

        /// <summary>
        /// Default action method for WatchListController. Handles POST.
        /// </summary>
        /// <returns>Watch list grid model as JsonResult.</returns>
        [HttpPost]
        public JsonResult Index(WatchListSearchModel watchListSearchModel)
        {
            // Construct the grid context.
            var gridContext = new GridContext
                                  {
                                      SearchInfo = new GridSearchInfo
                                                       {
                                                           SearchAgainstCriteria = watchListSearchModel.SearchAgainst,
                                                           SearchCriteriaList = this.CreateSearchCriteriaListForWatchListGrid(watchListSearchModel),
                                                           SearchWithOr = !watchListSearchModel.SearchWithAnd
                                                       },
                                      SortInfo = new GridSortInfo {SortOn = "Name"}
                                  };

            return Json(this.CreateWatchListGridModel(gridContext));
        }

        #endregion Public Methods
    }
}

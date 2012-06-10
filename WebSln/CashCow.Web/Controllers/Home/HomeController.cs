#region Namespaces

using System.Web.Mvc;

#endregion Namespaces

namespace CashCow.Web.Controllers.Home
{
    /// <summary>
    /// The controller class for Home.
    /// </summary>
    public class HomeController : CcBaseController
    {
        #region Public Methods

        /// <summary>
        /// Default action method for HomeController. Handles GET.
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return Redirect("WatchList");
        }

        #endregion Public Methods
    }
}

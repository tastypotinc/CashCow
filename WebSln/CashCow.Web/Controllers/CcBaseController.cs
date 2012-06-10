#region Namespaces

using System.Web.Mvc;
using Helpers;

#endregion Namespaces

namespace CashCow.Web.Controllers
{
    /// <summary>
    /// The base controller class.
    /// </summary>
    public class CcBaseController : Controller
    {
        #region Protected Properties

        /// <summary>
        /// Default grid action name. Read-only.
        /// </summary>
        protected string _DefaultGridAction
        {
            get
            {
                return ConfigHelper.DefaultGridAction;
            }
        }
        
        #endregion Protected Properties
    }
}

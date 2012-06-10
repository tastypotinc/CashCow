#region Namespaces

using System;
using Helpers;

#endregion Namespaces

namespace CashCow.Grid.Models.Grid
{
    /// <summary>
    /// Model class for grid link.
    /// </summary>
    public class GridLinkModel
    {
        #region Private Data
        
        private GridActionBehaviour _behaviour = GridActionBehaviour.Redirect;

        #endregion Private Data
        
        #region Public Properties

        /// <summary>
        /// Action method name with required parameter. For normal links mention full URL here.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Alert message to be shown to the user when user click on the link.
        /// </summary>
        public string AlertMessage { get; set; }

        /// <summary>
        /// Behaviour of link.
        /// Default value = GridActionBehaviour.Redirect.
        /// </summary>
        public GridActionBehaviour Behaviour
        {
            get
            {
                return this._behaviour;
            }

            set
            {
                this._behaviour = value;
            }
        }

        /// <summary>
        /// Behaviour of link. Read-only string.
        /// </summary>
        public string BehaviourString
        {
            get
            {
                return this._behaviour.ToString();
            }
        }
        
        /// <summary>
        /// Image path to be shown as link.
        /// </summary>
        public string ImagePath { get; set; }
        
        /// <summary>
        /// Text to be displayed in the link.
        /// </summary>
        public string Text { get; set; }

        #endregion Public Properties
    }
}
#region Namespaces

using Microsoft.Practices.EnterpriseLibrary.Data;

#endregion Namespaces

namespace CashCow.Provider
{
    /// <summary>
    /// The base class for data handler class.
    /// </summary>
    public class BaseDataHandler
    {
        #region Private members

        private Database _database;

        #endregion Private members

        #region Constructors
        
        protected BaseDataHandler()
        {
            this._database = DatabaseFactory.CreateDatabase();
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Provides and instance of Microsoft.Practices.EnterpriseLibrary.Data.Database.
        /// </summary>
        public Database Database
        {
            get { return this._database; }
            set { this._database = value; }
        }

        #endregion Public Properties
    }
}

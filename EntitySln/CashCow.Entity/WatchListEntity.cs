#region Namespaces

using System;

#endregion Namespaces

namespace CashCow.Entity
{
    /// <summary>
    /// Entity class representing DB table dbo.WatchList.
    /// </summary>
    public class WatchListEntity
    {
        #region Public Properties

        /// <summary>
        /// Represents column AlertRequired in the table dbo.WatchList.
        /// </summary>
        public bool AlertRequired { get; set; }

        /// <summary>
        /// Represents column AltNameOne in the table dbo.WatchList.
        /// </summary>
        public string AltNameOne { get; set; }

        /// <summary>
        /// Represents column AltNameThree in the table dbo.WatchList.
        /// </summary>
        public string AltNameThree { get; set; }

        /// <summary>
        /// Represents column AltNameTwo in the table dbo.WatchList.
        /// </summary>
        public string AltNameTwo { get; set; }

        /// <summary>
        /// Represents column BseSymbol in the table dbo.WatchList.
        /// </summary>
        public string BseSymbol { get; set; }

        /// <summary>
        /// Represents column CreatedOn in the table dbo.WatchList.
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Represents column IsActive in the table dbo.WatchList.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Represents column ModifiedOn in the table dbo.WatchList.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// Represents column Name in the table dbo.WatchList.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents column NseSymbol in the table dbo.WatchList.
        /// </summary>
        public string NseSymbol { get; set; }

        /// <summary>
        /// Represents column TempName in the table dbo.WatchList.
        /// </summary>
        public string TempName { get; set; }

        /// <summary>
        /// Represents primary key in the table dbo.WatchList.
        /// </summary>
        public int WatchListID { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Loads value to the properties of WatchList entity.
        /// </summary>
        public void Load(int watchListID, string bseSymbol, string nseSymbol,
            string name, string altNameOne, string altNameTwo, string altNameThree, string tempName,
            bool isActive, bool alertRequired, DateTime? createdOn, DateTime? modifiedOn)
        {
            this.WatchListID = watchListID;
            this.BseSymbol = bseSymbol;
            this.NseSymbol = nseSymbol;
            this.Name = name;
            this.AltNameOne = altNameOne;
            this.AltNameTwo = altNameTwo;
            this.AltNameThree = altNameThree;
            this.TempName = tempName;
            this.IsActive = isActive;
            this.AlertRequired = alertRequired;
            this.CreatedOn = (createdOn == null || createdOn.Value.Equals(DateTime.MinValue)) ? null : createdOn;
            this.ModifiedOn = (modifiedOn == null || modifiedOn.Value.Equals(DateTime.MinValue)) ? null : modifiedOn;
        }

        #endregion Public Methods
    }
}

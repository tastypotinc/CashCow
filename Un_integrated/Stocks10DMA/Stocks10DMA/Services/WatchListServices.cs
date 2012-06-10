
#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stocks10DMA.Repositories;
#endregion Usings

namespace Stocks10DMA.Services
{
    public class WatchListServices
    {
        #region Collaborators
        private readonly IWatchListRepository watchListRepository = null;
        #endregion Collaborators

        #region Constructors
        
        public WatchListServices()
        {
            this.watchListRepository = new WatchListRepository();
        }

        public WatchListServices(IWatchListRepository watchListRepository)
        {
            this.watchListRepository = watchListRepository;
        }

        #endregion Constructors
    }
}

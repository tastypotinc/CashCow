
#region Usings
using System;
using System.Linq;
using Stocks10DMA.Entities;
#endregion Usings

namespace Stocks10DMA.Repositories
{
    public interface IWatchListRepository
    {
        IQueryable<WatchListEntry> GetAll();
    }
}

#region Namespaces

using System.Linq;
using ShowSwingPoint.Models;

#endregion Namespaces

namespace ShowSwingPoint.Repositories
{
    public interface ISwingPointDataRepository
    {
        string DataSource { get; set; }
        string FileName { get; set; }
        IQueryable<WatchListModel> GetWatchList();
        IQueryable<StockDataModel> GetStockData();
    }
}

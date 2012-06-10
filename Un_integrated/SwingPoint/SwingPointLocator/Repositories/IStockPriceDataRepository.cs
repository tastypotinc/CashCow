#region Namespaces

using System.Linq;
using SwingPointLocator.Entities;

#endregion Namespaces

namespace SwingPointLocator.Repositories
{
    public interface IStockPriceDataRepository
    {
        string DataSource { get; set; }
        string FileName { get; set; }
        IQueryable<StockPriceData> GetPriceData();
    }
}

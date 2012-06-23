using System.Collections.Generic;

namespace ShowSwingPoint.Models
{
    public class HomeModel
    {
        public string SelectedStock { get; set; }

        public IList<WatchListModel> WatchList { get; set; }
    }
}
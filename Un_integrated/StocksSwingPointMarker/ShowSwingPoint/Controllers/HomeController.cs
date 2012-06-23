#region Namespaces

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ShowSwingPoint.Classes;
using ShowSwingPoint.Models;

#endregion Namespaces

namespace ShowSwingPoint.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Bse()
        {
            var homeModel = new HomeModel();
            homeModel.WatchList = GetWatchList();
            return View(homeModel);
        }

        [HttpPost]
        public ActionResult Bse(HomeModel homeModel)
        {
            homeModel.WatchList = GetWatchList();
            
            var serializer = new JavaScriptSerializer();
            ViewData["PlotData"] = 
                DataFormatter.SerializeToJson(ConvertStockDataToListOfArrays(GetStockData(homeModel.SelectedStock)));

            return View(homeModel);
        }

        [HttpGet]
        public ActionResult test()
        {
            var watchList = new HomeModel();
            watchList.WatchList = GetWatchList();

            return View(watchList);
        }

        [HttpPost]
        public JsonResult test(HomeModel homeModel)
        {
            var serializer = new JavaScriptSerializer();

            return Json(serializer.Serialize(ConvertStockDataToListOfArrays(GetStockData(homeModel.SelectedStock))));
        }

        private static IList<Array> ConvertStockDataToListOfArrays(IList<StockDataModel> stockDataList)
        {
            var listOfArrays = new List<Array>();
            ArrayList array = null;

            foreach (var stockData in stockDataList)
            {
                array = new ArrayList();
                array.Add(stockData.Date.ToString("MM/dd/yyyy"));
                array.Add(stockData.Open);
                array.Add(stockData.High);
                array.Add(stockData.Low);
                array.Add(stockData.Close);
                array.Add(stockData.Volume);
                if (stockData.SwingPoint.ToLower().Equals("sph"))
                {
                    array.Add("H");
                }
                else if (stockData.SwingPoint.ToLower().Equals("spl"))
                {
                    array.Add("L");
                }
                else
                {
                    array.Add(string.Empty);
                }
                
                listOfArrays.Add(array.ToArray());
            }

            return listOfArrays;
        }

        private static IList<WatchListModel> GetWatchList()
        {
            var service = new SwingPointShowService();
            return service.GetWatchList(@"~/Data/WatchList/WatchList.csv").ToList();
        }

        private static IList<StockDataModel> GetStockData(string stockSymbol)
        {
            var service = new SwingPointShowService();
            return service.GetStockData(string.Format(@"~/Data/Output/{0}.csv", stockSymbol)).ToList();
        }
    }
}

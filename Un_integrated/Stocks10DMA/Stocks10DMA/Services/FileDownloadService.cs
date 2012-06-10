
#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
#endregion Usings

namespace Stocks10DMA.Services
{
    public class FileDownloadService : IFileDownloadService
    {
        #region Data Members

        #endregion Data Members

        #region Constructors
        public FileDownloadService()
        {
        }
        #endregion Constructors

        #region DownloadFile
        public void DownloadFile(string downloadFrom, string downloadTo)
        {
            using (WebClient webClient = new WebClient())
            {
                Console.WriteLine("Attempting to download from {0} ...", downloadFrom);
                webClient.DownloadFile(downloadFrom, downloadTo);
                Console.WriteLine("Finished downloading file to {0}", downloadTo);
            }
        }
        #endregion DownloadFile
    }
}

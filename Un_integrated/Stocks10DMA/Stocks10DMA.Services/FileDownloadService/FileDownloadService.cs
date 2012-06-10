
#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
#endregion Usings

namespace Stocks10DMA.Services.FileDownloadService
{
    public class FileDownloadService
    {
        #region Data Members

        private bool isInitialized = false;
        private bool isValidated = false;

        #endregion Data Members

        #region Parameters

        private string DownloadUrl { get; set; }
        private string DestinationFilePathAndName { get; set; }

        #endregion Parameters

        #region Constructors

        public FileDownloadService()
        {
            this.isInitialized = false;
        }

        public FileDownloadService(string downloadUrl, string destinationFilePathAndName)
        {
            using (WebClient webClient = new WebClient())
            {
                Console.WriteLine("Downloading File from: {0}", downloadUrl);
                webClient.DownloadFile(downloadUrl, destinationFilePathAndName);
                Console.WriteLine("Saving File to: {0}", destinationFilePathAndName);
            }
        }

        #endregion Constructors

        #region Initialize

        public void Initialize(string downloadUrl, string destinationFilePathAndName)
        {
            this.DownloadUrl = downloadUrl;
            this.DestinationFilePathAndName = destinationFilePathAndName;
            this.isInitialized = true;
        }

        #endregion Initialize

        #region ValidateParameters
        
        public void ValidateParameters()
        {
            isValidated = true;
        }
        
        #endregion ValidateParameters
    }
}

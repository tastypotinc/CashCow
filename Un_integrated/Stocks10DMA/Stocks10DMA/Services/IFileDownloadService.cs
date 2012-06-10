
#region Usings
using System;
#endregion Usings

namespace Stocks10DMA.Services
{
    public interface IFileDownloadService
    {
        void DownloadFile(string downloadFrom, string downloadTo);
    }
}

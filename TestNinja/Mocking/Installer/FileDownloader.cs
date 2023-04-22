using System.Net;
using TestNinja.Mocking.Interfaces;

namespace TestNinja.Mocking.Installer
{
    public class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string fileName)
        {
            var client = new WebClient();
            client.DownloadFile(url, fileName);
        }
    }
}
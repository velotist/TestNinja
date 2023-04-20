using System.Net;

namespace TestNinja.Mocking
{
    public class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string fileName)
        {
            var client = new WebClient();
            client.DownloadFile(url,fileName);
        }
    }
}

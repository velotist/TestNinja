using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private readonly string _installerDestinationFileName;
        private readonly IFileDownloader _fileDownloader;

        public InstallerHelper(IFileDownloader fileDownloader, string installerDestinationFileName)
        {
            _fileDownloader = fileDownloader;
            _installerDestinationFileName = installerDestinationFileName;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _fileDownloader.DownloadFile(
                    $"http://example.com/{customerName}/{installerName}",
                    _installerDestinationFileName);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
}
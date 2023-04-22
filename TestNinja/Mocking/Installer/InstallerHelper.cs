using System.Net;
using TestNinja.Mocking.Interfaces;

namespace TestNinja.Mocking.Installer
{
    public class InstallerHelper
    {
        private readonly IFileDownloader _fileDownloader;
        private readonly string _installerDestinationFileName;

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
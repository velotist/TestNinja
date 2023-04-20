using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private readonly string _setupDestinationFile;
        private readonly IFileDownloader _fileDownloader;

        public InstallerHelper(IFileDownloader fileDownloader, string setupDestinationFile)
        {
            _fileDownloader = fileDownloader;
            _setupDestinationFile = setupDestinationFile;
        }


        // Test cases:
        // if download fails because of a WebException, method returns false
        // if file was downloaded, method returns true
        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _fileDownloader.DownloadFile(
                    $"http://example.com/{customerName}/{installerName}",
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
}
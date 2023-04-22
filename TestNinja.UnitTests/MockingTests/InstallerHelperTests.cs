using System.Net;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking.Installer;
using TestNinja.Mocking.Interfaces;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    public class InstallerHelperTests
    {
        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object, _installerDestinationFileName);
        }

        private InstallerHelper _installerHelper;
        private Mock<IFileDownloader> _fileDownloader;
        private readonly string _installerDestinationFileName = null;


        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            const string customerName = "customer";
            const string installerName = "installer";
            _fileDownloader.Setup(downloader => downloader
                    .DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            var result = _installerHelper.DownloadInstaller(customerName, installerName);

            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadSuccessful_ReturnTrue()
        {
            const string customerName = "customer";
            const string installerName = "installer";

            var result = _installerHelper.DownloadInstaller(customerName, installerName);

            Assert.That(result, Is.True);
        }
    }
}
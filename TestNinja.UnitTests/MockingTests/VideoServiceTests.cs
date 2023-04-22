using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking.Interfaces;
using TestNinja.Mocking.VideoService;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    public class VideoServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockFileReader = new Mock<IFileReader>();
            _mockVideoRepository = new Mock<IVideoRepository>();
            _service = new VideoService(_mockFileReader.Object, _mockVideoRepository.Object);
        }

        private VideoService _service;
        private Mock<IFileReader> _mockFileReader;
        private Mock<IVideoRepository> _mockVideoRepository;

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _mockFileReader.Setup(fileReader => fileReader.Read("video.txt")).Returns("");

            var result = _service.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_FewVideosAreUnprocessed_ReturnIdsOfUnprocessedVideos()
        {
            const string expected = "1,2,3";
            IEnumerable<Video> unprocessedVideos = new List<Video>
            {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            };
            _mockVideoRepository.Setup(videoRepository =>
                    videoRepository
                        .GetUnprocessedVideos())
                .Returns(unprocessedVideos);

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnEmptyString()
        {
            const string expected = "";
            _mockVideoRepository.Setup(videoRepository => videoRepository
                    .GetUnprocessedVideos())
                .Returns(new List<Video>());

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}
using System;
using Moq;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        private ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }

        [Test]
        public void Log_WhenCalled_SetLastErrorProperty()
        {
            const string errorMessage = "Some error message";

            _logger.Log(errorMessage);

            Assert.That(_logger.LastError, Is.EqualTo(errorMessage));
        }

        [Test]
        public void Log_ValidError_RaisesErrorLoggedEvent()
        {
            var mockedHandler = new Mock<EventHandler<Guid>>();
            _logger.ErrorLogged += mockedHandler.Object;
            const string error = "Some error message";

            _logger.Log(error);

            mockedHandler
                .Verify(e => e(_logger, It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            Assert.That(() => _logger.Log(error), Throws.ArgumentNullException);
        }
    }
}

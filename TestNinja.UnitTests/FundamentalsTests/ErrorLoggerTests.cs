using System;
using Moq;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.FundamentalsTests
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

        // This test is another way to test if an event has been risen
        [Test]
        public void Log_AnotherValidError_RaisesErrorLoggedEvent()
        {
            var id = Guid.Empty;
            _logger.ErrorLogged += (sender, args) => { id = args; };
            const string error = "Some error message";

            _logger.Log(error);

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
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

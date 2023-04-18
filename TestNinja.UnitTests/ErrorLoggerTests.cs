using System;
using Moq;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetLastErrorProperty()
        {
            var logger = new ErrorLogger();
            const string errorMessage = "Some error message";

            logger.Log(errorMessage);

            Assert.That(logger.LastError, Is.EqualTo(errorMessage));
        }

        [Test]
        public void Log_ValidError_RaisesErrorLoggedEvent()
        {
            var logger = new ErrorLogger();
            var mockedHandler = new Mock<EventHandler<Guid>>();
            logger.ErrorLogged += mockedHandler.Object;
            const string error = "Some error message";

            logger.Log(error);

            mockedHandler
                .Verify(e => e(logger, It.IsAny<Guid>()), Times.Once);
        }

    }
}

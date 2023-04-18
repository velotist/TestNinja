using System;
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
            const string errorMessage = "Error";

            logger.Log(errorMessage);

            Assert.That(logger.LastError, Is.EqualTo(errorMessage));
        }
    }
}

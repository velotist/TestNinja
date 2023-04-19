using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.FundamentalsTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-10)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowsArgumentOutOfRangeException(int speed)
        {
            Assert.That(() =>
                _calculator.CalculateDemeritPoints(speed),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(64, 0)]
        [TestCase(65,0)]
        public void CalculateDemeritPoints_LessThanSpeedLimit_ReturnZero(int speed, int expectedPoints)
        {
            var result = _calculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(expectedPoints));
        }

        [Test]
        [TestCase(66, 0)]
        [TestCase(69, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        [TestCase(76, 2)]
        [TestCase(100, 7)]
        public void CalculateDemeritPoints_GreaterThanSpeedLimit_ReturnPoints(int speed, int expectedPoints)
        {
            var result = _calculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(expectedPoints));
        }
    }
}

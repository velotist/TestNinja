using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        [Test]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            var math = new Math();
            
            var result = math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_IsFirstArgument_ReturnFirstArgument()
        {
            var math = new Math();

            var result = math.Max(2, 1);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_IsSecondArgument_ReturnSecondArgument()
        {
            var math = new Math();

            var result = math.Max(1, 2);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_AreBothArguments_ReturnSameArgument()
        {
            var math = new Math();

            var result = math.Max(2, 2);

            Assert.That(result, Is.EqualTo(2));
        }
    }
}

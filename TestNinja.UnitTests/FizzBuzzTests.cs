using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        [TestCase(3,"Fizz")]
        [TestCase(9, "Fizz")]
        [TestCase(99, "Fizz")]
        public void GetOutput_DivisibleOnlyByThree_ReturnFizz(int number, string expected)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(5, "Buzz")]
        [TestCase(10, "Buzz")]
        [TestCase(100, "Buzz")]
        public void GetOutput_DividableOnlyByFive_ReturnBuzz(int number, string expected)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(15, "FizzBuzz")]
        [TestCase(30, "FizzBuzz")]
        [TestCase(90, "FizzBuzz")]
        public void GetOutput_DivisibleOnlyByThreeAndFive_ReturnFizzBuzz(int number, string expected)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetOutput_OfZero_ReturnFizzBuzz()
        {
            const int number = 0;
            const string expected = "FizzBuzz";

            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(7, "7")]
        [TestCase(11, "11")]
        [TestCase(31, "31")]
        public void GetOutput_NotDivisibleByThreeAndOrFive_ReturnNumber(int number, string expected)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}

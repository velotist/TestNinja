using NUnit.Framework;

namespace TestNinja.UnitTests.FundamentalsTests
{
    [TestFixture]
    public class StackTests
    {
        private Fundamentals.Stack<string> _stack;
        

        [SetUp]
        public void SetUp()
        {
            _stack = new Fundamentals.Stack<string>();
        }

        [Test]
        public void Count_EmptyStack_ReturnsZero()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Push_ValidObject_AddObjectToStack()
        {
            const string stackEntry = "Test";
            const int expectedCount = 1;
            
            _stack.Push(stackEntry);
            
            Assert.That(_stack.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Push_NullObject_ThrowsException()
        {
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Pop_EmptyStack_ThrowsException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackWithObjects_ReturnObjectOnTopOfStack()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var result = _stack.Pop();

            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Pop_StackWithObjects_RemoveObjectOnTopOfStack()
        {
            const int expectedCount = 2;
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            _stack.Pop();

            Assert.That(_stack.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Peek_EmptyStack_ThrowsException()
        {
            Assert.That(()=> _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithObjects_ReturnObjectOnTopOfStack()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var result = _stack.Peek();

            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Peek_StackWithObjects_DoesNotRemoveObjectOnTopOfStack()
        {
            const int expectedCount = 3;
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            _stack.Peek();

            Assert.That(_stack.Count, Is.EqualTo(expectedCount));
        }
    }
}

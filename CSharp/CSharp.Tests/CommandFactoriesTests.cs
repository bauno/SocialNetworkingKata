using CSharp.Core;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class CommandFactoriesTests
    {
        [Test]
        public void PostCommandFactoryCanParsePostCommand()
        {
            var cmdString = "Alice -> I love the weather today";
            var expected = "Type: Post; User: Alice; Post: I love the weather today";
            var sut = new PostCommandFactory();
            Assert.AreEqual(expected, sut.Parse(cmdString).ToString());
        }

        [Test]
        public void ReadCommandFactoryCanParseReadCommand()
        {
            var cmdString = "Alice";            
            var expected = "Type: Read; User: Alice";
            var sut = new ReadCommandFactory();
            Assert.AreEqual(expected, sut.Parse(cmdString).ToString());
        }

        [TestCase("Alice")]
        [TestCase("Charlie follows Alice")]
        [TestCase("Charlie wall")]
        public void PostCommandFactoryReturnsNullIfCannotParseCommand(string cmdString)
        {

           var sut = new PostCommandFactory();
           Assert.IsNull(sut.Parse(cmdString));
        }
        
        [TestCase("Alice -> I love the weather today")]
        [TestCase("Charlie follows Alice")]
        [TestCase("Charlie wall")]
        public void ReadCommandFactoryReturnsNullIfCannotParseCommand(string cmdString)
        {

            var sut = new ReadCommandFactory();
            Assert.IsNull(sut.Parse(cmdString));
        }


    }
}
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

    }
}
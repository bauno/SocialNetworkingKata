using System.CodeDom;
using CSharp.Core;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class CommandParserTests
    {
        [Test]
        public void CanParsePostCommand()
        {
            var sut = new CommandParser();
            var cmdString = "Alice -> I love the weather today";
            var cmd = sut.Parse(cmdString);
            var expected = "Type: Post; User: Alice; Post: I love the weather today";
            Assert.AreEqual(expected, cmd.ToString());
        }

        [Test]
        public void CannotParseInvalidCommand()
        {
            var cmdString = "pippo";
            var sut = new CommandParser();
            Assert.Throws<InvalidCommandException>(() => sut.Parse(cmdString));
        }
    }
}
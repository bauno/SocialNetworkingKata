using System.CodeDom;
using CSharp.Core;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class CommandParserTests
    {
        private StringCommandParser _sut;

        [SetUp]
        public void Init()
        {
            _sut = new StringCommandParser();
        }
        
        [Test]
        public void CanParsePostCommand()
        {
            
            var cmdString = "Alice -> I love the weather today";
            var cmd = _sut.Parse(cmdString);
            var expected = "Type: Post; User: Alice; Post: I love the weather today";
            Assert.AreEqual(expected, cmd.ToString());
        }

        [Test]
        public void CanParseReadCommand()
        {
            var cmdString = "Alice";
            var cmd = _sut.Parse(cmdString);
            var expected = "Type: Read; User: Alice";
            Assert.AreEqual(expected, cmd.ToString());
        }

        [Test]
        public void CannotParseInvalidCommand()
        {
            var cmdString = "pippo pappo puppo";
            var sut = new StringCommandParser();
            Assert.Throws<InvalidCommandException>(() => sut.Parse(cmdString));
        }
    }
}
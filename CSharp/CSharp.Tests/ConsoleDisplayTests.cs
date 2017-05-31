using System;
using System.CodeDom;
using CSharp.Core;
using Moq;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class ConsoleDisplayTests
    {
        private Mock<PostTsFormatter> _formatter;
        private Mock<ITextConsole> _console;

        [SetUp]
        public void Init()
        {
            _formatter = new Mock<PostTsFormatter>();
            _console = new Mock<ITextConsole>();
        }

        [Test]
        public void ConstructorTests()
        {
            Assert.Throws<ArgumentNullException>(() => new ConsoleDisplay(null, _console.Object));
            Assert.Throws<ArgumentNullException>(() => new ConsoleDisplay(_formatter.Object, null));
        }
        
        [Test]
        public void CanPrintAWall()
        {
            var now = DateTime.Now;
            TimeService.TestNow = now;
            
            var post1Ts = now.AddSeconds(15);
            var post2Ts = now.AddSeconds(300);

            _formatter.Setup(f => f.NiceTs(now, post1Ts))
                .Returns("pippo");
            
            _formatter.Setup(f => f.NiceTs(now, post2Ts))
                .Returns("pluto");
            
            
            var postView1 = new PostView {Content = "Content1", TimeStamp = post1Ts};
            var postView2 = new PostView {Content = "Content2", TimeStamp = post2Ts};
            
            var wall = new WallView {User = "Alice", Posts = new[]{ postView1, postView2}};
            var sut = new ConsoleDisplay(_formatter.Object, _console.Object);
            
            sut.Show(wall);

            _console.Verify(c => c.PrintLine("Alice"));
            _console.Verify(c => c.PrintLine("Content1 (pippo)"));
            _console.Verify(c => c.PrintLine("Content2 (pluto)"));
        }
    }
}
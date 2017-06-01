using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using CSharp.Core;
using Moq;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class ConsoleDisplayTests
    {
        private class FakeConsole : ITextConsole
        {
            public List<string> Display { get; }

            public FakeConsole()
            {
                Display = new List<string>();
            }
            
            public void PrintLine(string line)
            {
                Display.Add(line);
            }
        }
        
        
        private Mock<PostTsFormatter> _formatter;
        private FakeConsole _console;
        

        [SetUp]
        public void Init()
        {
            _formatter = new Mock<PostTsFormatter>();
            _console = new FakeConsole();
        }

        [Test]
        public void ConstructorTests()
        {
            Assert.Throws<ArgumentNullException>(() => new ConsoleDisplay(null, _console));
            Assert.Throws<ArgumentNullException>(() => new ConsoleDisplay(_formatter.Object, null));
        }
        
        [Test]
        public void CanPrintASortedWall()
        {
            var now = DateTime.Now;
            TimeService.TestNow = now;
            
            var post1Ts = now.AddSeconds(-300);
            var post2Ts = now.AddSeconds(-15);

            _formatter.Setup(f => f.NiceTs(now, post1Ts))
                .Returns("pippo");
            
            _formatter.Setup(f => f.NiceTs(now, post2Ts))
                .Returns("pluto");
            
            
            var postView1 = new PostView {Content = "Content1", TimeStamp = post1Ts};
            var postView2 = new PostView {Content = "Content2", TimeStamp = post2Ts};
            
            var wall = new WallView {User = "Alice", Posts = new[]{ postView1, postView2}};
            var sut = new ConsoleDisplay(_formatter.Object, _console);
            
            sut.Show(wall);


            Assert.AreEqual("Content2 (pluto)", _console.Display.First());
            Assert.AreEqual("Content1 (pippo)", _console.Display.Last());
            
            
        }
    }
}
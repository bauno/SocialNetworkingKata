using System;
using System.CodeDom;
using CSharp.Core;
using Moq;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class ConsoleSocialNetworkTests
    {
        private Mock<SocialNetwork> _engine;
        private Mock<CommandParser> _parser;
        private Mock<Display> _display;
        private ConsoleSocialNetwork _sut;

        [SetUp]
        public void Init()
        {
            _engine = new Mock<SocialNetwork>();
            _parser = new Mock<CommandParser>();
            _display = new Mock<Display>();
            _sut = new ConsoleSocialNetwork(_parser.Object, _engine.Object, _display.Object);
        }
        
        
        [Test]
        public void CanExecPostCommand()
        {
            var cmdString = "postCommand";
            var cmd = new Mock<Command>();
            
            
            _parser.Setup(p => p.Parse(cmdString))
                .Returns(cmd.Object);

            

            _sut.Enter(cmdString);
            
            cmd.Verify(c => c.SendTo(_engine.Object), Times.Once);
            _display.Verify(d => d.Show(It.IsAny<string>()), Times.Never);


        }

        [Test]
        public void CanExecQueryCommand()
        {
            var cmdString = "query";
            var cmd = new Mock<Command>();
            cmd.Setup(c => c.SendTo(_engine.Object))
                .Returns(new[]{new PostView{Content = "Pippo"}});
            _parser.Setup(p => p.Parse(cmdString))
                .Returns(cmd.Object);
            _sut.Enter(cmdString);
            
            cmd.Verify(c => c.SendTo(_engine.Object), Times.Once);
            _display.Verify(d => d.Show("Pippo"), Times.Once);
            
            
        }

        [Test]
        public void ContructorTests()
        {
            Assert.Throws<ArgumentNullException>(() => new ConsoleSocialNetwork(null, _engine.Object, _display.Object));
            Assert.Throws<ArgumentNullException>(() => new ConsoleSocialNetwork(_parser.Object, null, _display.Object));
            Assert.Throws<ArgumentNullException>(() => new ConsoleSocialNetwork(_parser.Object, _engine.Object, null));
        }
       
    }
}
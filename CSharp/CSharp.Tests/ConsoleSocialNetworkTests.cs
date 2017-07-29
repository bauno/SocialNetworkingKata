using System;
using CSharp.Core;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories.Interfaces;
using CSharp.Core.Services;
using CSharp.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
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
        public void CanExecCommand()
        {
            var cmdString = "postCommand";
            var cmd = new Mock<Command>();
            var display = new Mock<Displayable>();
            
            
            _parser.Setup(p => p.Parse(cmdString))
                .Returns(Maybe<Command>.From(cmd.Object));
            cmd.Setup(c => c.SendTo(_engine.Object))
                .Returns(display.Object);
                                  

            _sut.Enter(cmdString);
            
            cmd.Verify(c => c.SendTo(_engine.Object), Times.Once);
            
            display.Verify(d => d.ShowOn(_display.Object), Times.Once);
            
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
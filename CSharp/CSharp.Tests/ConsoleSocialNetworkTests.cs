using System;
using CSharp.Core;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories.Interfaces;
using CSharp.Core.Services;
using CSharp.Core.Services.Interfaces;
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
            cmd.Setup(c => c.Type)
                .Returns(MessageType.Command);
            
            
            _parser.Setup(p => p.Parse(cmdString))
                .Returns(cmd.Object);
                                  
            
            

            _sut.Enter(cmdString);
            
            cmd.Verify(c => c.SendTo(_engine.Object), Times.Once);
            
        }

        [Test]
        public void CanExecQuery()
        {
            var queryString = "query";
            var query = new Mock<Query>();
            query.Setup(q => q.Type)
                .Returns(MessageType.Query);

            _parser.Setup(p => p.Parse(queryString))
                .Returns(query.Object);
            
            _sut.Enter(queryString);
            
            query.Verify(q => q.Exec(_engine.Object), Times.Once);
            
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
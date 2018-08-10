using System;
using SocialNetwork.Core.Commands.Interfaces;
using SocialNetwork.Core.Factories.Interfaces;
using SocialNetwork.Core.Services;
using SocialNetwork.Core.Services.Interfaces;
using LanguageExt;
using Moq;
using NUnit.Framework;
using static LanguageExt.Prelude;


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
        
        
        [Ignore("Aborts")]
//        [Test]
        public void CanExecCommand()        
        {
            var cmdString = "postCommand";
            var cmd = new Mock<Command>();
            var displayAble = new Mock<Displayable>();
            
            cmd.Setup(c => c.SendTo(_engine.Object))
                .Returns(displayAble.Object);
            
            _parser.Setup(p => p.Parse(cmdString))
                .Returns(Right<string,Command>(cmd.Object));
            
                                            

            var res = _sut.Enter(cmdString);
            Assert.IsTrue(res.IsNone);
            
            cmd.Verify(c => c.SendTo(_engine.Object), Times.Once);
            
            displayAble.Verify(d => d.ShowOn(_display.Object), Times.Once);
            
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
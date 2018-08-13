using System;
using SocialNetwork.Core.Commands.Interfaces;
using SocialNetwork.Core.Factories;
using SocialNetwork.Core.Factories.Interfaces;
using LanguageExt;
using Moq;
using NUnit.Framework;
using static LanguageExt.Prelude;

namespace CSharp.Tests
{
    [TestFixture]
    public class CommandParserTests
    {
        [Test]
        public void ConstructorTests()
        {
            Assert.Throws<ArgumentNullException>(() => new StringCommandParser(null));
        }

        [Test]
        public void WillCallFactoriesToParseCommand()
        {
            var cmdString = "pippo";
            var cmd = new Mock<Command>();            
            var fac1 = new Mock<CommandFactory>();
            fac1.Setup(f1 => f1.Parse(cmdString))
                .Returns(Option<Command>.None);
            
            var fac2 = new Mock<CommandFactory>();
            fac2.Setup(f2 => f2.Parse(cmdString))
                .Returns(Option<Command>.Some(cmd.Object));
            
            var sut = new StringCommandParser(new[]{fac2.Object, fac2.Object});

            var res = sut.Parse(cmdString);
        }

        [Test]
        public void WillThrowInvalidCommandIfAllFactoriesReturnNull()
        {
            var cmdString = "pippo";             
            var fac1 = new Mock<CommandFactory>();
            fac1.Setup(f1 => f1.Parse(cmdString))
                .Returns(Option<Command>.None);
            
            var fac2 = new Mock<CommandFactory>();
            fac2.Setup(f2 => f2.Parse(cmdString))
                .Returns(Option<Command>.None);
            
            var sut = new StringCommandParser(new[]{fac1.Object, fac2.Object});

            sut.Parse(cmdString)
                .Match(c => Assert.Fail(), 
                    err => Assert.AreEqual("Cannot parse command: 'pippo'", err));
            
            fac1.Verify(f1 => f1.Parse(cmdString), Times.Once);
            fac1.Verify(f2 => f2.Parse(cmdString), Times.Once);
        }                 
    }
}
using System;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories;
using CSharp.Core.Factories.Interfaces;
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
            cmd.Setup(c => c.Type)
                .Returns(CommandType.Other);
            var fac1 = new Mock<CommandFactory>();
            fac1.Setup(f1 => f1.Parse(cmdString))
                .Returns(None);
            
            var fac2 = new Mock<CommandFactory>();
            fac2.Setup(f2 => f2.Parse(cmdString))
                .Returns(cmd.Object);
            
            var sut = new StringCommandParser(new[]{fac2.Object, fac2.Object});

            var res = sut.Parse(cmdString);
        }

        [Test]
        public void WillThrowInvalidCommandIfAllFactoriesReturnNull()
        {
            var cmdString = "pippo";             
            var fac1 = new Mock<CommandFactory>();
            fac1.Setup(f1 => f1.Parse(cmdString))
                .Returns<Command>(null);
            
            var fac2 = new Mock<CommandFactory>();
            fac2.Setup(f2 => f2.Parse(cmdString))
                .Returns<Command>(null);
            
            var sut = new StringCommandParser(new[]{fac1.Object, fac2.Object});

            sut.Parse(cmdString)
                .Match(c => Assert.Fail(), 
                    err => Assert.AreEqual("Cannot parse command: 'pippo'", err));
            
            fac1.Verify(f1 => f1.Parse(cmdString), Times.Once);
            fac1.Verify(f2 => f2.Parse(cmdString), Times.Once);
        }                 
    }
}